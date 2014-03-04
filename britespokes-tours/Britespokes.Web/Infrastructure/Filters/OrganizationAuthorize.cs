using System.Configuration;
using Britespokes.Core.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Britespokes.Core.Domain;
using Britespokes.Web.Infrastructure.Logging;

namespace Britespokes.Web.Infrastructure.Filters
{
  public sealed class OrganizationAuthorize : AuthorizeAttribute
  {
    private readonly string _defaultPublicOrganizationName = "Britespokes";
    private readonly string[] _publicHosts;
    private readonly string[] _publicTlds;
    private readonly IRepository<Organization> _organizationRepo;
    private readonly ILogger _logger;

    public OrganizationAuthorize(IRepository<Organization> organizationRepo, ILogger logger)
    {
      _organizationRepo = organizationRepo;
      _logger = logger;

      var appSettings = ConfigurationManager.AppSettings;
      _publicHosts = GetConfigValues(appSettings [ "publicHosts" ]);
      _publicTlds = GetConfigValues(appSettings [ "publicTlds" ]);
      _defaultPublicOrganizationName = appSettings [ "defaultPublicOrganization" ] ?? "Britespokes";
    }

    public override void OnAuthorization(AuthorizationContext filterContext)
    {
      var organizationName = _defaultPublicOrganizationName;
      var name = organizationName;
      var httpContext = filterContext.HttpContext;
      Expression<Func<Organization, bool>> predicate = (o => o.Name == name);

      var host = GetHostName(httpContext);
      if (!_publicHosts.Contains(host))
      {
        var parts = CountHostParts(host);
        if (parts > 1)
        {
          // if there are no host name parts, it is likely an intranet url (localhost, for example)
          var tld = host.Substring(0, host.IndexOf('.'));
          if (!_publicTlds.Contains(tld))
          {
            organizationName = tld;
            predicate = (o => o.Subdomain == organizationName);
          }
        }
      }

      var organization = _organizationRepo
        .FindBy(predicate)
        .SingleOrDefault();

      if (organization == null)
        filterContext.Result = new HttpNotFoundResult();
      else
      {
        httpContext.Items["Organization"] = organization;
        filterContext.Controller.ViewBag.Organization = organization;
        if (!organization.IsPublic)
          base.OnAuthorization(filterContext);
      }
    }

    private string GetHostName(HttpContextBase httpContext)
    {
      var request = httpContext.Request;
      var host = request.Headers["HOST"];

      if (!string.IsNullOrEmpty(host))
      {
        host = host.ToLower();
        if (host.IndexOf(":", StringComparison.Ordinal) >= 0)
          host = host.Substring(0, host.IndexOf(":", StringComparison.Ordinal));
      }
      else
      {
        _logger.Debug("Empty host in request: {0}", request.Headers);
        //var e = new ApplicationException(string.Format("Empty host in request: {0}", request.Headers));
        //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
      }

      return (host ?? "");
    }

    private static int CountHostParts(string host)
    {
      return host.Count(c => c == '.');
    }

    private static string[] GetConfigValues(string valString)
    {
      var vals = new string[0];
      if (!string.IsNullOrEmpty(valString))
      {
        vals = valString.Split(new[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries);
      }
      return vals;
    }
  }
}