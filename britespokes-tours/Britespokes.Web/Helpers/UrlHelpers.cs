using System.Web.Mvc;

namespace Britespokes.Web.Helpers
{
  public static class UrlHelpers
  {
    private const string AssetHost = "britespokesstorage.blob.core.windows.net";

    public static string AssetUrl(this UrlHelper url, string relpath)
    {
      //var protocol = url.RequestContext.HttpContext.Request.IsSecureConnection ? "http" : "http";
      var protocol = url.RequestContext.HttpContext.Request.IsSecureConnection ? "https" : "http";
      return string.Format("{0}://{1}/{2}", protocol, AssetHost, relpath);
    }
  }
}