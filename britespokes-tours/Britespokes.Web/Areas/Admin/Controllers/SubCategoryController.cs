using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.SubCategories;
using Britespokes.Web.Areas.Admin.Models.SubCategories;
using Britespokes.Web.Areas.Admin.Models.Tours;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
namespace Britespokes.Web.Areas.Admin.Controllers
{
    public class SubCategoryController : AdminBaseController
    {
        //
        // GET: /Admin/SubCategory/
        private readonly ISubCategoryQueryService _subcategoryQueryService;
        private readonly ISubCategoryCommandService _subcategoryCommandService;

        public SubCategoryController(ISubCategoryQueryService subcategoryQueryService, ISubCategoryCommandService subcategoryCommandService)
        {
            _subcategoryQueryService = subcategoryQueryService;
            _subcategoryCommandService = subcategoryCommandService;

        }
        public ActionResult Index()
        {

            var subcategories = _subcategoryQueryService.SubCategories();
            return View(new SubCategoriesIndex
            {
                Count = subcategories.Count(),
                SubCategories = subcategories
            });
        }

        public ActionResult Create()
        {
            var subCategoryEdit = new SubCategoryEdit
            {
                CategoryList = _subcategoryQueryService.Categories(),
                TourList = _subcategoryQueryService.Tours(),
                TourFromPost = new System.Collections.Generic.List<string>()
            };
            return View(subCategoryEdit);


        }

        [HttpPost]
        public ActionResult Create(SubCategoryEdit subCategoryEdit)
        {
            if (_subcategoryQueryService.IsCodeUnique(subCategoryEdit.Code))
                ModelState.AddModelError("Code", "Tour code must be unique");
            subCategoryEdit.SelectedTours = new System.Collections.Generic.List<int>();
            foreach (string s in subCategoryEdit.TourFromPost)
                subCategoryEdit.SelectedTours.Add(Convert.ToInt32(s));
            if (ModelState.IsValid)
            {
                var tourUpdate = Mapper.Map<SubCategoryUpdate>(subCategoryEdit);
                _subcategoryCommandService.Add(tourUpdate);
                return RedirectToRoute("admin-Sub-categories");
            }
            if (subCategoryEdit.TourFromPost == null)
                subCategoryEdit.TourFromPost = new System.Collections.Generic.List<string>();

            subCategoryEdit.CategoryList = _subcategoryQueryService.Categories();
            subCategoryEdit.TourList = _subcategoryQueryService.Tours();
            return View(subCategoryEdit);
        }


        public ActionResult Edit(int subcategoryid)
        {
            var subcategory = _subcategoryQueryService.FindSubCategory(subcategoryid);
            var subcategoryedit = Mapper.Map<SubCategoryEdit>(subcategory);
            subcategoryedit.CategoryId = subcategory.CategoriesId;
            subcategoryedit.TourList = _subcategoryQueryService.Tours();
            subcategoryedit.CategoryList = _subcategoryQueryService.Categories();
            subcategoryedit.TourFromPost = new System.Collections.Generic.List<string>();

            if (subcategory.SEOTools != null && subcategory.SEOTools.Count > 0)
            {
                SEOTool SEOTool = subcategory.SEOTools.First();
                subcategoryedit.SEOTitle = SEOTool.SEOTitle;
                subcategoryedit.FocusKeyword = SEOTool.FocusKeyword;
                subcategoryedit.MetaDescription = SEOTool.MetaDescription;
            }



            foreach (Tour tou in subcategory.Tours)
                subcategoryedit.TourFromPost.Add(tou.Id.ToString());

            return View(subcategoryedit);
        }

        [HttpPost]

        public ActionResult Edit(SubCategoryEdit subcategoryedit)
        {
            if (_subcategoryQueryService.IsCodeUnique(subcategoryedit.Code, subcategoryedit.Id))
                ModelState.AddModelError("Code", "Tour code must be unique");

            if (!ModelState.IsValid)
            {

                subcategoryedit.CategoryList = _subcategoryQueryService.Categories();
                subcategoryedit.TourList = _subcategoryQueryService.Tours();
                subcategoryedit.TourFromPost = new System.Collections.Generic.List<string>();
                return View(subcategoryedit);
            }



            subcategoryedit.SelectedTours = new System.Collections.Generic.List<int>();
            foreach (string s in subcategoryedit.TourFromPost)
                subcategoryedit.SelectedTours.Add(Convert.ToInt32(s));

            var subcategoryUpdate = Mapper.Map<SubCategoryUpdate>(subcategoryedit);
            _subcategoryCommandService.Update(subcategoryUpdate);
            TempData["Info"] = "Sub Category updated";
            return RedirectToRoute("admin-sub-category-edit", new { subcategoryedit.Id });
        }

        [HttpPost]
        public ActionResult Delete(int subcategoryid)
        {
            try
            {
                _subcategoryCommandService.Delete(subcategoryid);
            }
            catch (Exception dbex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(dbex);
                TempData["Error"] = "This category could not be deleted.";
                return RedirectToRoute("admin-sub-category-edit", new { subcategoryid });
            }
            return RedirectToRoute("admin-Sub-categories");
        }
    }
}
