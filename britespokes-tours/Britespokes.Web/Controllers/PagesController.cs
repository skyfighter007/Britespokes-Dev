using Britespokes.Core.Domain;
using Britespokes.Services.Admin.SEOToolStaticPages;
using System.Web.Mvc;

namespace Britespokes.Web.Controllers
{
    public class PagesController : BritespokesController
    {
        private readonly ISEOToolStaticPageQueryService _seoToolStaticPageQueryService;

        public PagesController(ISEOToolStaticPageQueryService seoToolStaticPageQueryService)
        {
            _seoToolStaticPageQueryService = seoToolStaticPageQueryService;
        }

        public ActionResult Show(string permalink)
        {
          SEOToolStaticPage SEOTool =  _seoToolStaticPageQueryService.FindSEOToolStaticPageByPermalink(permalink);

          if (SEOTool != null)
          {
              ViewBag.FocusKeyword = SEOTool.FocusKeyword;
              ViewBag.MetaDescription = SEOTool.MetaDescription;
              ViewBag.Title = SEOTool.SEOTitle;
          }
            return View(permalink);
        }
    }
}
