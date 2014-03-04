using System.Collections.Generic;
using System.Web.Mvc;

namespace Britespokes.Web.Helpers
{
  internal class BootstrapLabelHelper
  {
    private readonly Dictionary<object, string> _labelClassDictionary;

    internal BootstrapLabelHelper(Dictionary<object, string> labelClassDictionary)
    {
      _labelClassDictionary = labelClassDictionary;
    }

    internal MvcHtmlString BootstrapLabel(HtmlHelper html, string text, object value)
    {
      if (value == null)
        return null;

      var span = new TagBuilder("span");
      span.AddCssClass("label");

      var labelClass = LabelClass(value);
      if (labelClass != null)
        span.AddCssClass(string.Format("label-{0}", labelClass));

      span.InnerHtml = text;

      return MvcHtmlString.Create(span.ToString());
    }

    private string LabelClass(object key)
    {
      return _labelClassDictionary.ContainsKey(key) ? _labelClassDictionary[key] : null;
    }
  }
}