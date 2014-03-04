using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.DiscountCodes;
using Britespokes.Services.Admin.Products;
using Britespokes.Services.Admin.Tours;
using Britespokes.Web.Areas.Admin.Models.DiscountCodes;
using Ninject.Infrastructure.Language;

namespace Britespokes.Web.Areas.Admin.Controllers
{
  public class DiscountCodesController : AdminBaseController
  {
    private readonly IDiscountCodeService _discountCodeService;
    private readonly ITourQueryService _tourQueryService;
    private readonly IProductQueryService _productQueryService;

    public DiscountCodesController(IDiscountCodeService discountCodeService, ITourQueryService tourQueryService, IProductQueryService productQueryService)
    {
      _discountCodeService = discountCodeService;
      _tourQueryService = tourQueryService;
      _productQueryService = productQueryService;
    }

    public ActionResult Index()
    {
      var discountCodeUsage = _discountCodeService.UsageCounts();
      var discountCodes = _discountCodeService.All();
      var viewModel = new DiscountCodesIndex
      {
        Count = discountCodes.Count(),
        DiscountCodes =
          Mapper.Map<IEnumerable<DiscountCode>, IEnumerable<DiscountCodeIndex>>(discountCodes.ToEnumerable())
      };
      foreach (var discountCode in viewModel.DiscountCodes)
      {
        discountCode.NumberOfTimesUsed = discountCodeUsage.ContainsKey(discountCode.Id)
          ? discountCodeUsage [ discountCode.Id ]
          : 0;
      }
      return View(viewModel);
    }

    public ActionResult Edit(int discountCodeId)
    {
      var discountCode = _discountCodeService.FindDiscountCode(discountCodeId);
      return View(BuildEditModel(discountCode));
    }

    [HttpPost]
    public ActionResult Edit(DiscountCodeEdit discountCodeEdit)
    {
      var discountCode = _discountCodeService.FindDiscountCode(discountCodeEdit.Id);

      if (!ModelState.IsValid) return View(BuildEditModel(discountCodeEdit, discountCode));

      Mapper.Map(discountCodeEdit, discountCode);
      UpdateTargets(discountCode, discountCodeEdit);
      _discountCodeService.Update(discountCode);
      return RedirectToRoute("admin-discount-codes");
    }

    public ActionResult Create()
    {
      return View(BuildEditModel(new DiscountCodeEdit(), new DiscountCode()));
    }

    [HttpPost]
    public ActionResult Create(DiscountCodeEdit discountCodeEdit)
    {
      var discountCode = Mapper.Map<DiscountCode>(discountCodeEdit);
      if (ModelState.IsValid)
      {
        UpdateTargets(discountCode, discountCodeEdit);
        _discountCodeService.Add(discountCode);
        return RedirectToRoute("admin-discount-codes");
      }
      return View(BuildEditModel(discountCodeEdit, discountCode));
    }

    [HttpPost]
    public ActionResult Destroy(int discountCodeId)
    {
      try
      {
        _discountCodeService.Remove(discountCodeId);
      }
      catch (DbUpdateException dbex)
      {
        Elmah.ErrorSignal.FromCurrentContext().Raise(dbex);
        TempData [ "Error" ] =
          "This discount code could not be deleted. It might already be in use in one or more pending or completed orders. Mark the code as inactive instead.";
        return RedirectToRoute("admin-discount-code-edit", new { discountCodeId });
      }
      return RedirectToRoute("admin-discount-codes");
    }

    private void UpdateTargets(DiscountCode discountCode, DiscountCodeEdit discountCodeEdit)
    {
      if (discountCode.Tours == null)
        discountCode.Tours = new List<Tour>();
      discountCode.Tours.Clear();
      if (!discountCode.IsGlobal && discountCodeEdit.TourId != null)
        discountCode.Tours =
          discountCodeEdit.TourId.Select(i => _tourQueryService.FindTour(Convert.ToInt32(i))).ToList();

      if (discountCode.Products == null)
        discountCode.Products = new List<Product>();
      discountCode.Products.Clear();
      if (!discountCode.IsGlobal && discountCodeEdit.ProductId != null)
        discountCode.Products =
          discountCodeEdit.ProductId.Select(i => _productQueryService.FindProduct(Convert.ToInt32(i))).ToList();

      if (discountCode.ProductVariants == null)
        discountCode.ProductVariants = new List<ProductVariant>();
      discountCode.ProductVariants.Clear();
      if (!discountCode.IsGlobal && discountCodeEdit.ProductVariantId != null)
        discountCode.ProductVariants =
          discountCodeEdit.ProductVariantId.Select(i => _productQueryService.FindProductVariant(Convert.ToInt32(i))).ToList();
    }

    private DiscountCodeEdit BuildEditModel(DiscountCode discountCode)
    {
      var edit = Mapper.Map<DiscountCodeEdit>(discountCode);
      return BuildEditModel(edit, discountCode);
    }

    private DiscountCodeEdit BuildEditModel(DiscountCodeEdit discountCodeEdit, DiscountCode discountCode)
    {
      var tours = _tourQueryService.Tours().ToList();
      discountCodeEdit.TourItems =
        tours.Select(
          t =>
            new SelectListItem
            {
              Text = string.Format("{0} - {1}", t.Code, t.Name),
              Value = t.Id.ToString(CultureInfo.InvariantCulture)
            });

      var firstTour = tours.FirstOrDefault();
      if (firstTour != null && firstTour.Departures.Any())
      {
        discountCodeEdit.ProductItems =
          firstTour.Departures.Select(d => d.Product)
            .Select(
              p =>
                new SelectListItem
                {
                  Text = string.Format("{0} - {1}", p.Departure.Code, p.Name),
                  Value = p.Id.ToString(CultureInfo.InvariantCulture)
                });

        var firstProduct = firstTour.Departures.First().Product;
        if (firstProduct != null)
        {
          discountCodeEdit.ProductVariantItems =
            firstProduct.ProductVariants.Select(
              pv => new SelectListItem
              {
                Text = string.Format("{0} - {1}", pv.Product.Departure.Code, pv.DisplayName),
                Value = pv.Id.ToString(CultureInfo.InvariantCulture)
              });
        }
      }

      if (discountCode.Tours != null)
      {
        discountCodeEdit.ToursJson =
          new JavaScriptSerializer().Serialize(
            discountCode.Tours.Select(
              t => new { id = t.Id, name = string.Format("{0} - {1}", t.Code, t.Name), targetType = "TourId" }));
      }

      if (discountCode.Products != null)
      {
        discountCodeEdit.ProductsJson =
          new JavaScriptSerializer().Serialize(
            discountCode.Products.Select(
              p =>
                new { id = p.Id, name = string.Format("{0} - {1}", p.Departure.Code, p.Name), targetType = "ProductId" }));
      }

      if (discountCode.ProductVariants != null)
      {
        discountCodeEdit.ProductVariantsJson =
          new JavaScriptSerializer().Serialize(
            discountCode.ProductVariants.Select(
              v =>
                new
                {
                  id = v.Id,
                  name = string.Format("{0} - {1}", v.Product.Departure.Code, v.DisplayName),
                  targetType = "ProductVariantId"
                }));
      }
      return discountCodeEdit;
    }
  }
}