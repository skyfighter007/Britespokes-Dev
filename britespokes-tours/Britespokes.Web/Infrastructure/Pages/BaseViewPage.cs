using Britespokes.Services.Authentication;
using System.Web.Mvc;
using Britespokes.Web.App_Start;

namespace Britespokes.Web.Infrastructure.Pages
{
  public abstract class BaseViewPage : WebViewPage
  {
    public virtual new CustomPrincipal User
    {
      get { return base.User as CustomPrincipal; }
    }

    public string DeployEnvironment
    {
      get { return EnvironmentConfig.DeployEnvironment; }
    }

    public string ControllerName
    {
      get { return ViewContext.RouteData.Values["Controller"] as string; }
    }

    public string ActionName
    {
      get { return ViewContext.RouteData.Values["Action"] as string; }
    }
  }

  public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
  {
    public virtual new CustomPrincipal User
    {
      get { return base.User as CustomPrincipal; }
    }

    public string DeployEnvironment
    {
      get { return EnvironmentConfig.DeployEnvironment; }
    }

    public string ControllerName
    {
      get { return ViewContext.RouteData.Values["Controller"] as string; }
    }

    public string ActionName
    {
      get { return ViewContext.RouteData.Values["Action"] as string; }
    }
  }
}