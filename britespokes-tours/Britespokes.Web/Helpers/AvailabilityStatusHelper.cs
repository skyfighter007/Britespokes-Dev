using System.Collections.Generic;
using System.Web.Mvc;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Helpers
{
  public static class AvailabilityStatusHelper
  {
    private static readonly Dictionary<object, string> LabelClassDictionary = new Dictionary<object, string>
      {
        { AvailabilityStatus.Available, "success" },
        { AvailabilityStatus.ComingSoon, "warning" },
        { AvailabilityStatus.SoldOut, "important" }
      };

    public static MvcHtmlString AvailabilityStatusLabel(this HtmlHelper html, AvailabilityStatus availabilityStatusStatus)
    {
      var labelHelper = new BootstrapLabelHelper(LabelClassDictionary);
      return labelHelper.BootstrapLabel(html, availabilityStatusStatus.Name.ToLowerInvariant(), availabilityStatusStatus.Id);
    }
  }
}