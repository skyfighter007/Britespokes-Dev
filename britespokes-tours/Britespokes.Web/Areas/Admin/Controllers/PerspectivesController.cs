using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Perspectives;
using Britespokes.Services.Admin.Tours;
using Britespokes.Web.Areas.Admin.Models.Perspectives;
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
    public class PerspectivesController : BritespokesController
    {
        private readonly IPerspectiveQueryService _perspectiveQueryService;
        private readonly IPerspectiveCommandService _perspectiveCommandService;
        private readonly ITourQueryService _tourQueryService;
        // GET: /Admin/Perspectives/
        public PerspectivesController(IPerspectiveQueryService perspectiveQueryService, IPerspectiveCommandService perspectiveCommandService,ITourQueryService tourQueryService)
    {
        _perspectiveQueryService = perspectiveQueryService;
        _perspectiveCommandService = perspectiveCommandService;
        _tourQueryService = tourQueryService;

    }
        public ActionResult Index()
        {
            var perspectives = _perspectiveQueryService.All();
            return View(new PerspectivesIndex
            {
                Count = perspectives.Count(),
                Perspectives = perspectives
            });
        }
        public ActionResult Edit(int perspectiveid)
        {
            var perspective = _perspectiveQueryService.FindPerspectivePost(perspectiveid);
            var perspectiveEdit = Mapper.Map<PerspectiveEdit>(perspective);
           /* 
            * Commented -> Category replaced by tours
            * perspectiveEdit.CategoryList = _perspectiveQueryService.Categories();
            perspectiveEdit.CategoriesFromPost = new System.Collections.Generic.List<string>();
            */
            //foreach (Category cat in perspective.Categories)
            //    perspectiveEdit.CategoriesFromPost.Add(cat.Id.ToString());


            if (perspective.SEOTools != null && perspective.SEOTools.Count > 0)
            {
                SEOTool SEOTool = perspective.SEOTools.First();
                perspectiveEdit.SEOTitle = SEOTool.SEOTitle;
                perspectiveEdit.FocusKeyword = SEOTool.FocusKeyword;
                perspectiveEdit.MetaDescription = SEOTool.MetaDescription;
            }

            perspectiveEdit.TourList = _tourQueryService.Tours();

            return View(perspectiveEdit);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PerspectiveEdit perspectiveEdit)
        {
            if (!ModelState.IsValid)
            {
                /*Commented -> Category replaced by tours
                perspectiveEdit.CategoryList = _perspectiveQueryService.Categories(); ;
                perspectiveEdit.CategoriesFromPost = new System.Collections.Generic.List<string>();
                 */
                perspectiveEdit.TourList = _tourQueryService.Tours();

                return View(perspectiveEdit);
            }
             /*Commented -> Category replaced by tours
            perspectiveEdit.SelectedCategories = new System.Collections.Generic.List<int>();
            foreach (string s in perspectiveEdit.CategoriesFromPost)
                perspectiveEdit.SelectedCategories.Add(Convert.ToInt32(s));
              */
            var perspectiveUpdate = Mapper.Map<PerspectivePostUpdate>(perspectiveEdit);
            _perspectiveCommandService.Update(perspectiveUpdate);
            TempData["Info"] = "Perspective post updated";
            return RedirectToRoute("admin-perspective-edit", new { perspectiveEdit.Id });
        }

        
        public ActionResult Create()
        {
            var perspectiveEdit = new PerspectiveEdit
            {
                  /*Commented -> Category replaced by tours
                    CategoryList = _perspectiveQueryService.Categories(),
                    CategoriesFromPost = new System.Collections.Generic.List<string>()
                   */
                TourList = _tourQueryService.Tours()
            };
            return View(perspectiveEdit);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PerspectiveEdit perspectiveEdit)
        {
            if (ModelState.IsValid)
            {
                /*Commented -> Category replaced by tours
                perspectiveEdit.SelectedCategories = new System.Collections.Generic.List<int>();
                foreach (string s in perspectiveEdit.CategoriesFromPost)
                    perspectiveEdit.SelectedCategories.Add(Convert.ToInt32(s));
                 */
                var perspectivePostUpdate = Mapper.Map<PerspectivePostUpdate>(perspectiveEdit);
                perspectivePostUpdate.UserId = UserContext.UserId;

                _perspectiveCommandService.Add(perspectivePostUpdate);
                return RedirectToRoute("admin-perspectives");
            }
            perspectiveEdit.TourList = _tourQueryService.Tours();
            /*Commented -> Category replaced by tours
            if (perspectiveEdit.CategoriesFromPost == null)
                perspectiveEdit.CategoriesFromPost = new System.Collections.Generic.List<string>();
              perspectiveEdit.CategoryList = _perspectiveQueryService.Categories();
             */
           
            return View(perspectiveEdit);
        }
        [HttpPost]
        public ActionResult Delete(int perspectiveid)
        {
            try
            {
                _perspectiveCommandService.Delete(perspectiveid);
            }
            catch (DbUpdateException dbex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(dbex);
                TempData["Error"] = "This Perspectives post could not be deleted.";
                return RedirectToRoute("admin-tour-edit", new { perspectiveid });
            }
            return RedirectToRoute("admin-perspectives");
        }
    }
}
