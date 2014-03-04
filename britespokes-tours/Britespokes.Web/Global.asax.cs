using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using Britespokes.Services.Authentication;
using Britespokes.Web.App_Start.AutoMapper;

namespace Britespokes.Web
{
  // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
  // visit http://go.microsoft.com/?LinkId=9394801

  public class MvcApplication : HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();

      // using web api in an area
      //WebApiConfig.Register(GlobalConfiguration.Configuration);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      AutoMapperConfiguration.Configure();
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
      var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
      if (authCookie != null)
      {
        var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        var serializer = new JavaScriptSerializer();
        Debug.Assert(authTicket != null, "authTicket != null");
        var serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);

        var customPrincipal = new CustomPrincipal(authTicket.Name)
          {
            UserId = serializeModel.UserId,
            OrganizationId = serializeModel.OrganizationId,
            Roles = serializeModel.Roles ?? (new string[0]),
            IsConfirmed = serializeModel.IsConfirmed,
            DisplayName = serializeModel.DisplayName
          };

        HttpContext.Current.User = customPrincipal;
      }
    }
  }
}