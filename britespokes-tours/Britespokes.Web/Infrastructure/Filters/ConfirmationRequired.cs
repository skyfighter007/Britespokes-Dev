using System.Web.Mvc;

namespace Britespokes.Web.Infrastructure.Filters
{
  public class ConfirmationRequired : IActionFilter
  {
    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
      //var customPrincipal = filterContext.HttpContext.User as CustomPrincipal;

      //if (customPrincipal == null) return;

      //if (customPrincipal.Identity.IsAuthenticated && !customPrincipal.IsConfirmed && !customPrincipal.IsGuest)
      //{
      //  var resendLink = new UrlHelper(filterContext.RequestContext).RouteUrl("confirmation-resend");
      //  var msg = new StringBuilder();
      //  msg.Append("Please check your email for user confirmation instructions. ");
      //  msg.AppendFormat("<a href=\"{0}\" title=\"Resend confirmation instructions\">Click here</a> ", resendLink);
      //  msg.AppendFormat("to resend your confirmation instructions.");
      //  filterContext.Controller.TempData["Info"] = msg.ToString();
      //}
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
  }
}