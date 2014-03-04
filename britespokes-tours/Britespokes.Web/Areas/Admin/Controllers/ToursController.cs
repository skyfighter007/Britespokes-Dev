using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Tours;
using Britespokes.Web.Areas.Admin.Models.Tours;
using System;

namespace Britespokes.Web.Areas.Admin.Controllers
{
    public class ToursController : AdminBaseController
    {
        private readonly ITourQueryService _tourQueryService;
        private readonly ITourCommandService _tourCommandService;

        public ToursController(ITourQueryService tourQueryService, ITourCommandService tourCommandService)
        {
            _tourQueryService = tourQueryService;
            _tourCommandService = tourCommandService;
        }

        public ActionResult Index()
        {
            var tours = _tourQueryService.Tours();
            return View(new ToursIndex
            {
                Count = tours.Count(),
                Tours = tours
            });
        }

        public ActionResult Edit(int tourId)
        {
            var tour = _tourQueryService.FindTour(tourId);
            var tourEdit = Mapper.Map<TourEdit>(tour);
            tourEdit.AvailabilityStatusList = _tourQueryService.Availabilities();
            tourEdit.CategoryList = _tourQueryService.Categories();
            tourEdit.CategoriesFromPost = new System.Collections.Generic.List<string>();

            if (tour.SEOTools != null && tour.SEOTools.Count > 0)
            {
                SEOTool SEOTool = tour.SEOTools.First();
                tourEdit.SEOTitle = SEOTool.SEOTitle;
                tourEdit.FocusKeyword = SEOTool.FocusKeyword;
                tourEdit.MetaDescription = SEOTool.MetaDescription;
            }


            foreach (Category cat in tour.Categories)
                tourEdit.CategoriesFromPost.Add(cat.Id.ToString());
            tourEdit.TourTypeList = _tourQueryService.TourTypes();
            return View(tourEdit);
        }

        [HttpPost]

        public ActionResult Edit(TourEdit tourEdit)
        {
            if (_tourQueryService.IsCodeUnique(tourEdit.Code, tourEdit.Id))
                ModelState.AddModelError("Code", "Tour code must be unique");

            if (!ModelState.IsValid)
            {
                tourEdit.AvailabilityStatusList = _tourQueryService.Availabilities();
                tourEdit.CategoryList = _tourQueryService.Categories();
                tourEdit.TourTypeList = _tourQueryService.TourTypes();
                tourEdit.CategoriesFromPost = new System.Collections.Generic.List<string>();
                return View(tourEdit);
            }



            tourEdit.SelectedCategories = new System.Collections.Generic.List<int>();
            foreach (string s in tourEdit.CategoriesFromPost)
                tourEdit.SelectedCategories.Add(Convert.ToInt32(s));

            var tourUpdate = Mapper.Map<TourUpdate>(tourEdit);
            _tourCommandService.Update(tourUpdate);
            TempData["Info"] = "Tour updated";
            return RedirectToRoute("admin-tour-edit", new { tourEdit.Id });
        }

        public ActionResult Create()
        {
            var tourEdit = new TourEdit
            {
                AvailabilityStatusList = _tourQueryService.Availabilities(),
                CategoryList = _tourQueryService.Categories(),
                TourTypeList = _tourQueryService.TourTypes(),
                CategoriesFromPost = new System.Collections.Generic.List<string>()
            };
            return View(tourEdit);
        }

        [HttpPost]
        public ActionResult Create(TourEdit tourEdit)
        {
            if (_tourQueryService.IsCodeUnique(tourEdit.Code))
                ModelState.AddModelError("Code", "Tour code must be unique");
            tourEdit.SelectedCategories = new System.Collections.Generic.List<int>();
            foreach (string s in tourEdit.CategoriesFromPost)
                tourEdit.SelectedCategories.Add(Convert.ToInt32(s));
            if (ModelState.IsValid)
            {
                var tourUpdate = Mapper.Map<TourUpdate>(tourEdit);
                _tourCommandService.Add(tourUpdate);
                return RedirectToRoute("admin-tours");
            }
            if (tourEdit.CategoriesFromPost == null)
                tourEdit.CategoriesFromPost = new System.Collections.Generic.List<string>();
            tourEdit.AvailabilityStatusList = _tourQueryService.Availabilities();
            tourEdit.CategoryList = _tourQueryService.Categories();
            tourEdit.TourTypeList = _tourQueryService.TourTypes();
            return View(tourEdit);
        }

        [HttpPost]
        public ActionResult Destroy(int tourId)
        {
            try
            {
                _tourCommandService.Delete(tourId);
            }
            catch (DbUpdateException dbex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(dbex);
                TempData["Error"] = "This tour could not be deleted.";
                return RedirectToRoute("admin-tour-edit", new { tourId });
            }
            return RedirectToRoute("admin-tours");
        }

        public ActionResult Meta(int tourId)
        {
            var tour = _tourQueryService.FindTour(tourId);
            if (tour.TourMeta == null)
                tour.TourMeta = new TourMeta { TourId = tourId };
            var tourMetaEdit = Mapper.Map<TourMetaEdit>(tour.TourMeta);
            tourMetaEdit.TourName = tour.Name;
            tourMetaEdit.TourId = tour.Id;
            tourMetaEdit.Code = tour.Code;
            tourMetaEdit.Name = tour.Name;
            tourMetaEdit.TourPermalink = tour.Permalink;
            SetTourMetaDefaults(tourMetaEdit);
            return View(tourMetaEdit);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Meta(TourMetaEdit tourMetaEdit)
        {
            if (!ModelState.IsValid)
                return View(tourMetaEdit);

            var tourMetaUpdate = Mapper.Map<TourMetaUpdate>(tourMetaEdit);
            _tourCommandService.UpdateMeta(tourMetaUpdate);
            TempData["Info"] = "Tour details updated";
            return RedirectToRoute("admin-tour-meta", new { tourMetaEdit.TourId });
        }

        private void SetTourMetaDefaults(TourMetaEdit metaEdit)
        {
            if (string.IsNullOrWhiteSpace(metaEdit.PriceIncludesHeader))
                metaEdit.PriceIncludesHeader = "price includes:";
            if (string.IsNullOrWhiteSpace(metaEdit.AdditionalInformationHeader))
                metaEdit.AdditionalInformationHeader = "hotel accommodations at properties such as:";
        }

    }
}