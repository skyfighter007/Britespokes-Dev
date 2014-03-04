using System.Web.Mvc;

namespace Britespokes.Web.Helpers
{
  public static class CurrencyHelper
  {
    public static string FormatCurrency(this HtmlHelper html, decimal amount)
    {
        return string.Format("{0:C0}", amount);//amount.ToString("C0");
    }
  }
}