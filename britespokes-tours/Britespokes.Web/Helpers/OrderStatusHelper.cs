using System.Collections.Generic;
using System.Web.Mvc;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Helpers
{
  public static class OrderStatusHelper
  {
    private static readonly Dictionary<object, string> LabelClassDictionary = new Dictionary<object,string>
      {
        { OrderStatus.PendingStatusName, "info" },
        { OrderStatus.ProcessingStatusName, "important" },
        { OrderStatus.FailedStatusName, "important" },
        { OrderStatus.CompleteStatusName, "success" },
        { OrderStatus.CancelledStatusName, "warning" }
      };

    public static MvcHtmlString OrderStatusLabel(this HtmlHelper html, OrderStatus orderStatus)
    {
      var labelHelper = new BootstrapLabelHelper(LabelClassDictionary);
      return labelHelper.BootstrapLabel(html, orderStatus.Name, orderStatus.Name);
    }
  }
}