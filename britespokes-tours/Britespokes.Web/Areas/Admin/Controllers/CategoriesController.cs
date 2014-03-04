using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Tours;
using Britespokes.Web.Areas.Admin.Models.Tours;
using Britespokes.Services.Categories;
using Britespokes.Services.Admin.Categories;
using Britespokes.Web.Areas.Admin.Models.Categories;

namespace Britespokes.Web.Areas.Admin.Controllers
{
  public class CategoriesController : AdminBaseController
  {
      private readonly ICategoryService _categoryQueryService;
      private readonly ICategoryCommandService _categoryCommandService;

      public CategoriesController(ICategoryService categoryQueryService, ICategoryCommandService categoryCommandService)
    {
        _categoryQueryService = categoryQueryService;
        _categoryCommandService = categoryCommandService;
    }

    public ActionResult Index()
    {
        var categories = _categoryQueryService.All();
        return View(new CategoriesIndex
      {
          Count = categories.Count(),
          Categories = categories
      });
    }

    public ActionResult Edit(int categoryid)
    {
        var category = _categoryQueryService.FindCategory(categoryid);
        var categoryEdit = Mapper.Map<CategoryEdit>(category);

        if (category.SEOTools != null && category.SEOTools.Count > 0)
        {
            SEOTool SEOTool = category.SEOTools.First();
            categoryEdit.SEOTitle = SEOTool.SEOTitle;
            categoryEdit.FocusKeyword = SEOTool.FocusKeyword;
            categoryEdit.MetaDescription = SEOTool.MetaDescription;
        }


       // categoryEdit.ca = _tourQueryService.Availabilities();
      //  tourEdit.TourTypeList = _tourQueryService.TourTypes();
        return View(categoryEdit);
    }


    [HttpPost]
    [ValidateInput(false)]
    public ActionResult Edit(CategoryEdit categoryEdit)
    {
        if (_categoryQueryService.IsCodeUnique(categoryEdit.Code, categoryEdit.Id))
            ModelState.AddModelError("Code", "Category code must be unique");

        if (!ModelState.IsValid)
        {
            return View(categoryEdit);
        }


        var categoryUpdate = Mapper.Map<CategoryUpdate>(categoryEdit);
        _categoryCommandService.Update(categoryUpdate);
        TempData["Info"] = "Category updated";
        return RedirectToRoute("admin-category-edit", new { categoryEdit.Id });

    }
    public ActionResult Create()
    {
        var CategoryEdit = new CategoryEdit
        {

        };
        return View(CategoryEdit);
    }

    [HttpPost]
    [ValidateInput(false)]
    public ActionResult Create(CategoryEdit categoryEdit)
    {
        if (_categoryQueryService.IsCodeUnique(categoryEdit.Code, categoryEdit.Id))
            ModelState.AddModelError("Code", "Category code must be unique");

        if (!ModelState.IsValid)
        {
            return View(categoryEdit);
        }


        var categoryUpdate = Mapper.Map<CategoryUpdate>(categoryEdit);
        _categoryCommandService.Add(categoryUpdate);
        TempData["Info"] = "Category Added";
        return RedirectToRoute("admin-categories", new { categoryEdit.Id });



    }


    [HttpPost]
    public ActionResult Delete(int categoryid)
    {
        try
        {
            _categoryCommandService.Delete(categoryid);
        }
        catch (DbUpdateException dbex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(dbex);
            TempData["Error"] = "This category could not be deleted.";
            return RedirectToRoute("admin-category-edit", new { categoryid });
        }
        return RedirectToRoute("admin-categories");
    }

    //[HttpPost]
      
    //public ActionResult Edit(TourEdit tourEdit)
    //{
    //  if (_tourQueryService.IsCodeUnique(tourEdit.Code, tourEdit.Id))
    //    ModelState.AddModelError("Code", "Tour code must be unique");

    //  if (!ModelState.IsValid)
    //  {
    //    tourEdit.AvailabilityStatusList = _tourQueryService.Availabilities();
    //    tourEdit.TourTypeList = _tourQueryService.TourTypes();
    //    return View(tourEdit);
    //  }

    //  var tourUpdate = Mapper.Map<TourUpdate>(tourEdit);
    //  _tourCommandService.Update(tourUpdate);
    //  TempData [ "Info" ] = "Tour updated";
    //  return RedirectToRoute("admin-tour-edit", new { tourEdit.Id });
    //}

    //public ActionResult Create()
    //{
    //  var tourEdit = new TourEdit
    //  {
    //    AvailabilityStatusList = _tourQueryService.Availabilities(),
    //    TourTypeList = _tourQueryService.TourTypes()
    //  };
    //  return View(tourEdit);
    //}

    //[HttpPost]
    //public ActionResult Create(TourEdit tourEdit)
    //{
    //  if (_tourQueryService.IsCodeUnique(tourEdit.Code))
    //    ModelState.AddModelError("Code", "Tour code must be unique");

    //  if (ModelState.IsValid)
    //  {
    //    var tourUpdate = Mapper.Map<TourUpdate>(tourEdit);
    //    _tourCommandService.Add(tourUpdate);
    //    return RedirectToRoute("admin-tours");
    //  }
    //  tourEdit.AvailabilityStatusList = _tourQueryService.Availabilities();
    //  tourEdit.TourTypeList = _tourQueryService.TourTypes();
    //  return View(tourEdit);
    //}

    //[HttpPost]
    //public ActionResult Destroy(int tourId)
    //{
    //  try
    //  {
    //    _tourCommandService.Delete(tourId);
    //  }
    //  catch (DbUpdateException dbex)
    //  {
    //    Elmah.ErrorSignal.FromCurrentContext().Raise(dbex);
    //    TempData [ "Error" ] = "This tour could not be deleted.";
    //    return RedirectToRoute("admin-tour-edit", new { tourId });
    //  }
    //  return RedirectToRoute("admin-tours");
    //}

    //public ActionResult Meta(int tourId)
    //{
    //  var tour = _tourQueryService.FindTour(tourId);
    //  if (tour.TourMeta == null)
    //    tour.TourMeta = new TourMeta { TourId = tourId };
    //  var tourMetaEdit = Mapper.Map<TourMetaEdit>(tour.TourMeta);
    //  tourMetaEdit.TourName = tour.Name;
    //  tourMetaEdit.TourId = tour.Id;
    //  tourMetaEdit.Code = tour.Code;
    //  tourMetaEdit.Name = tour.Name;
    //  tourMetaEdit.TourPermalink = tour.Permalink;
    //  SetTourMetaDefaults(tourMetaEdit);
    //  return View(tourMetaEdit);
    //}

    //[HttpPost]
    //[ValidateInput(false)]
    //public ActionResult Meta(TourMetaEdit tourMetaEdit)
    //{
    //  if (!ModelState.IsValid)
    //    return View(tourMetaEdit);

    //  var tourMetaUpdate = Mapper.Map<TourMetaUpdate>(tourMetaEdit);
    //  _tourCommandService.UpdateMeta(tourMetaUpdate);
    //  TempData [ "Info" ] = "Tour details updated";
    //  return RedirectToRoute("admin-tour-meta", new { tourMetaEdit.TourId });
    //}

    //private void SetTourMetaDefaults(TourMetaEdit metaEdit)
    //{
    //  if (string.IsNullOrWhiteSpace(metaEdit.PriceIncludesHeader))
    //    metaEdit.PriceIncludesHeader = "price includes:";
    //  if (string.IsNullOrWhiteSpace(metaEdit.AdditionalInformationHeader))
    //    metaEdit.AdditionalInformationHeader = "hotel accommodations at properties such as:";
    //}
  }
}