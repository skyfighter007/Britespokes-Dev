using System;
using System.Web.Mvc;
using Britespokes.Services.Authentication;
using Britespokes.Services.Users;

namespace Britespokes.Web.Infrastructure.Filters
{
  public class LogUserActivityAttribute : ActionFilterAttribute
  {
    private readonly IUserService _userService;

    public LogUserActivityAttribute(IUserService userService)
    {
      _userService = userService;
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      var customPrincipal = filterContext.HttpContext.User as CustomPrincipal;

      if (customPrincipal != null && customPrincipal.Identity.IsAuthenticated)
      {
        var user = _userService.Find(customPrincipal.UserId);
        if (user != null)
        {
          user.LastActivityAt = DateTime.UtcNow;
          user.LastIpAddress = GetIpAddress();
          _userService.UpdateUser(user);
        }
      }

      base.OnActionExecuting(filterContext);
    }

    private string GetIpAddress()
    {
      System.Web.HttpContext context = System.Web.HttpContext.Current;

      string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

      if (!string.IsNullOrEmpty(ipAddress))
      {
        string[] addresses = ipAddress.Split(',');
        if (addresses.Length != 0)
        {
          return addresses[0];
        }
      }

      return context.Request.ServerVariables["REMOTE_ADDR"];
    }
  }
}