using System.Web.Mvc;
using Britespokes.Web.Controllers;
using Britespokes.Web.Models.Users;

namespace Britespokes.Web.Helpers
{
  public static class AuthHelpers
  {
    public static bool IsAuthenticated(this HtmlHelper html)
    {
      var userContext = GetUserContext(html.ViewContext);
      if (userContext == null)
        return false;
      return userContext.IsAuthenticated;
    }

    public static bool IsGuest(this HtmlHelper html)
    {
      var userContext = GetUserContext(html.ViewContext);
      if (userContext == null)
        return false;
      return userContext.IsGuest;
    }

    public static bool IsConfirmed(this HtmlHelper html)
    {
      var userContext = GetUserContext(html.ViewContext);
      if (userContext == null)
        return false;
      return userContext.IsConfirmed;
    }

    private static UserContext GetUserContext(ViewContext viewContext)
    {
      var controller = viewContext.Controller as BritespokesController;
      return controller != null ? controller.UserContext : null;
    }
  }
}