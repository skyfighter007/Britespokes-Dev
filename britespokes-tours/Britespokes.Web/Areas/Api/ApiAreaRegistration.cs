using System.Web.Http;
using System.Web.Mvc;

namespace Britespokes.Web.Areas.Api
{
  public class ApiAreaRegistration : AreaRegistration
  {
    public override string AreaName
    {
      get
      {
        return "Api";
      }
    }

    public override void RegisterArea(AreaRegistrationContext context)
    {
      context.Routes.MapHttpRoute(
          "Api_default",
          "api/{controller}/{id}",
          new { id = RouteParameter.Optional }
      );
    }
  }
}