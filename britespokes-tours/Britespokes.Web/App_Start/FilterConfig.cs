using System.Web.Mvc;

// ReSharper disable CheckNamespace
namespace Britespokes.Web
// ReSharper restore CheckNamespace
{
  public class FilterConfig
  {
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute());
    }
  }
}