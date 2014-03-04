using System.Web.Mvc;

namespace Britespokes.Web.Helpers
{
  public static class NotificationsHelper
  {
    public static MvcHtmlString InfoMessage(this HtmlHelper html, string message = null)
    {
      return Message(html, "Info", message);
    }

    public static MvcHtmlString ErrorMessage(this HtmlHelper html, string message = null)
    {
      return Message(html, "Error", message);
    }

    private static MvcHtmlString Message(HtmlHelper html, string messageType, string message = null)
    {
      var msg = message ?? (html.ViewContext.TempData[messageType] as string);
      if (msg == null)
        return null;

      var p = new TagBuilder("p") { InnerHtml = msg };

      var div = new TagBuilder("div");
      div.AddCssClass(string.Format("alert alert-{0}", messageType.ToLower()));
      div.InnerHtml = p.ToString();

      return MvcHtmlString.Create(div.ToString());
    }
  }
}