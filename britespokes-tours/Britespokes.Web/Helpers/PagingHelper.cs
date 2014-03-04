using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;

namespace Britespokes.Web.Helpers
{
  public static class PagingHelper
  {
    public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
    {
      var result = new StringBuilder();

      var firstPage = new TagBuilder("li");
      if (pagingInfo.CurrentPage == 1)
        firstPage.AddCssClass("disabled");
      var firstPageLink = new TagBuilder("a");
      firstPageLink.MergeAttribute("href", pageUrl(1));
      firstPageLink.InnerHtml = "&#8249;";
      firstPage.InnerHtml = firstPageLink.ToString();
      result.Append(firstPage);

      for (var i = 1; i <= pagingInfo.TotalPages; i++)
      {
        var li = new TagBuilder("li");

        if (i == pagingInfo.CurrentPage)
          li.AddCssClass("active");

        var link = new TagBuilder("a");
        link.MergeAttribute("href", pageUrl(i));
        link.InnerHtml = i.ToString(CultureInfo.InvariantCulture);

        li.InnerHtml = link.ToString();

        result.Append(li);
      }

      var lastPage = new TagBuilder("li");
      if (pagingInfo.CurrentPage == pagingInfo.TotalPages)
        lastPage.AddCssClass("disabled");
      var lastPageLink = new TagBuilder("a");
      lastPageLink.MergeAttribute("href", pageUrl(pagingInfo.TotalPages));
      lastPageLink.InnerHtml = "&#8250;";
      lastPage.InnerHtml = lastPageLink.ToString();
      result.Append(lastPage);

      return MvcHtmlString.Create(result.ToString());
    }

    public static MvcHtmlString PerspectivePageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
    {
        var result = new StringBuilder();

        //var firstPage = new TagBuilder("li");
        //if (pagingInfo.CurrentPage == 1)
        //    firstPage.AddCssClass("disabled");
        //var firstPageLink = new TagBuilder("a");
        //firstPageLink.MergeAttribute("href", pageUrl(1));
        //firstPageLink.InnerHtml = "&#8249;";
        //firstPage.InnerHtml = firstPageLink.ToString();
        //result.Append(firstPage);

        for (var i = 1; i <= pagingInfo.TotalPages; i++)
        {
            var li = new TagBuilder("li");

            if (i == pagingInfo.CurrentPage)
                li.AddCssClass("active");

            var link = new TagBuilder("a");
            link.MergeAttribute("href", pageUrl(i));
            link.InnerHtml = i.ToString(CultureInfo.InvariantCulture);

            li.InnerHtml = link.ToString();

            result.Append(li);
        }

        //var lastPage = new TagBuilder("li");
        //if (pagingInfo.CurrentPage == pagingInfo.TotalPages)
        //    lastPage.AddCssClass("disabled");
        //var lastPageLink = new TagBuilder("a");
        //lastPageLink.MergeAttribute("href", pageUrl(pagingInfo.TotalPages));
        //lastPageLink.InnerHtml = "&#8250;";
        //lastPage.InnerHtml = lastPageLink.ToString();
        //result.Append(lastPage);

        return MvcHtmlString.Create(result.ToString());
    }
  }
}