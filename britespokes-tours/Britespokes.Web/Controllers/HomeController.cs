using System.Text;
using System.Web.Mvc;
using Britespokes.Web.App_Start;
using Britespokes.Services.Experiences;
using Britespokes.Services.Categories;
using Britespokes.Web.Models.Home;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.SEOToolStaticPages;
using Britespokes.Services.Admin.Organizations;

namespace Britespokes.Web.Controllers
{
    public class HomeController : BritespokesController
    {
        private readonly ICategoryService _categoryService;
        private readonly ISEOToolStaticPageQueryService _seoToolStaticPageQueryService;
        private readonly IOrganizationService _organizationService;
        //
        // GET: /Home/


        public HomeController(ICategoryService categoryService, ISEOToolStaticPageQueryService seoToolStaticPageQueryService, IOrganizationService organizationService)
        {
            _categoryService = categoryService;
            _seoToolStaticPageQueryService = seoToolStaticPageQueryService;
            _organizationService = organizationService;
        }
       

        public ActionResult Index()
        {
            SEOToolStaticPage SEOTool = _seoToolStaticPageQueryService.FindSEOToolStaticPageByPermalink("home");
            Session["LogoUrl"] = _organizationService.FindOrganization(UserContext.Organization.Id).LogoImageURL;
            Session["AltText"] = _organizationService.FindOrganization(UserContext.Organization.Id).LogoImageAltText;
           
            if (SEOTool != null)
            {
               
                ViewBag.FocusKeyword = SEOTool.FocusKeyword;
                ViewBag.MetaDescription = SEOTool.MetaDescription;
                ViewBag.Title = SEOTool.SEOTitle;
            }

            var categories = _categoryService.All();
            return View(new CategoryShow
            {
                Categories = categories.ToList()
            });
        }

        public FileContentResult Robots()
        {
            var contentBuilder = new StringBuilder();
            contentBuilder.AppendLine("User-agent: *");

            var isAzureUrl = Request.Url != null && Request.Url.ToString().ToLowerInvariant().Contains("cloudapp.net");
            if (EnvironmentConfig.IsProduction && !isAzureUrl)
            {
                contentBuilder.AppendLine("Disallow: /elmah.axd");
                contentBuilder.AppendLine("Disallow: /admin");
                contentBuilder.AppendLine("Disallow: /Admin");
            }
            else
            {
                contentBuilder.AppendLine("Disallow: /");
            }

            return File(Encoding.UTF8.GetBytes(contentBuilder.ToString()), "text/plain");
        }
    }
}
