using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Perspectives;
using Britespokes.Services.Admin.Tours;
using Britespokes.Services.Admin.SEOToolStaticPages;
using Britespokes.Web.Areas.Admin.Models.Perspectives;
using Britespokes.Web.Areas.Admin.Models.SEOToolStaticPages;
using Britespokes.Web.Controllers;
using Britespokes.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Britespokes.Web.Areas.Admin.Controllers
{
    public class SEOToolStaticPagesController : BritespokesController
    {
        private readonly ISEOToolStaticPageQueryService _seoToolStaticPageQueryService;
        private readonly ISEOToolStaticPageCommandService _seoToolStaticPageCommandService;

        public SEOToolStaticPagesController(ISEOToolStaticPageQueryService seoToolStaticPageQueryService, ISEOToolStaticPageCommandService seoToolStaticPageCommandService)
        {
            _seoToolStaticPageQueryService = seoToolStaticPageQueryService;
            _seoToolStaticPageCommandService = seoToolStaticPageCommandService;

        }

        public ActionResult Index()
        {
            var seoToolStaticPages = _seoToolStaticPageQueryService.All();
            return View(new SEOToolStaticPagesIndex
            {
                Count = seoToolStaticPages.Count(),
                SEOToolStaticPages = seoToolStaticPages
            });
        }
        public ActionResult Edit(int pageid)
        {
            var seoToolStaticPage = _seoToolStaticPageQueryService.FindSEOToolStaticPage(pageid);
            var seoToolStaticPageEdit = Mapper.Map<SEOToolStaticPageEdit>(seoToolStaticPage);
          
            return View(seoToolStaticPageEdit);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SEOToolStaticPageEdit seoToolStaticPageEdit)
        {
            if (!ModelState.IsValid)
            {
                return View(seoToolStaticPageEdit);
            }

            var seoToolStaticPageUpdate = Mapper.Map<SEOToolStaticPageUpdate>(seoToolStaticPageEdit);
            _seoToolStaticPageCommandService.Update(seoToolStaticPageUpdate);
            TempData["Info"] = "SEO Tool updated";
            return RedirectToRoute("admin-seotool-staic-edit", new { seoToolStaticPageEdit.Id });
        }

        
        public ActionResult Create()
        {
            var seoToolStaticPageEdit = new SEOToolStaticPageEdit
            {
               
            };
            return View(seoToolStaticPageEdit);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SEOToolStaticPageEdit seoToolStaticPageEdit)
        {
            if (ModelState.IsValid)
            {
                var seoToolStaticPageUpdate = Mapper.Map<SEOToolStaticPageUpdate>(seoToolStaticPageEdit);

                _seoToolStaticPageCommandService.Add(seoToolStaticPageUpdate);
                return RedirectToRoute("admin-seotool-staic-pages");
            }

            return View(seoToolStaticPageEdit);
        }
        
    }
}
