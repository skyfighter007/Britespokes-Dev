using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Web.Mvc;

namespace Britespokes.Web.Helpers
{
  public static class PluralizeHelper
  {
    public static string Pluralize(this HtmlHelper html, int count, string singular)
    {
      return count == 1 ? singular : Pluralize(singular);
    }

    private static string Pluralize(string singular)
    {
      return PluralizationService.CreateService(CultureInfo.CurrentUICulture).Pluralize(singular);
    }
  }
}