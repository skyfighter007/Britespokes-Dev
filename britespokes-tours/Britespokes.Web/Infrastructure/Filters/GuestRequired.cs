using System;
using System.Web.Mvc;
using Britespokes.Services.Authentication;
using Britespokes.Services.Users;
using System.Web.Routing;

namespace Britespokes.Web.Infrastructure.Filters
{
  public class GuestRequiredAttribute : Attribute { }

  public class GuestRequired : IActionFilter
  {
    private readonly IRegistrationService _registrationService;

    public GuestRequired(IRegistrationService registrationService)
    {
      _registrationService = registrationService;
    }
  
    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (ApplyBehavior(filterContext))
      {
        var customPrincipal = filterContext.HttpContext.User as CustomPrincipal;
        if (customPrincipal == null || !customPrincipal.Identity.IsAuthenticated)
        {
          _registrationService.RegisterGuest(filterContext.Controller.ViewBag.Organization);
        }
      }
    }

    private static bool ApplyBehavior(ActionExecutingContext filterContext)
    {
      return (filterContext.ActionDescriptor.GetCustomAttributes(typeof(GuestRequiredAttribute), true).Length > 0);
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
  }
}