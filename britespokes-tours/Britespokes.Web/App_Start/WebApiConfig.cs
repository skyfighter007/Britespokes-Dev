using System.Web.Http;

// this is unused - using api in an area
// ReSharper disable CheckNamespace
namespace Britespokes.Web
// ReSharper restore CheckNamespace
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // ReSharper disable RedundantArgumentName
      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );
      // ReSharper restore RedundantArgumentName
    }
  }
}
