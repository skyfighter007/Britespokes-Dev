using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Orders
{
  public class DiscountCodeValidator : IDiscountCodeValidator
  {
    private readonly IDiscountCodeQueryService _discountCodeQueryService;
    private readonly IRepository<ProductVariant> _productVariantRepo;

    public DiscountCodeValidator(IDiscountCodeQueryService discountCodeQueryService, IRepository<ProductVariant> productVariantRepo)
    {
      _discountCodeQueryService = discountCodeQueryService;
      _productVariantRepo = productVariantRepo;
    }

    public DiscountCodeValidationResult Validate(string code, int[] variantIds)
    {
      var discountCode = _discountCodeQueryService.FindByCode(code);

      if (discountCode == null)
        return Invalid("Invalid discount code.");

      if (discountCode.IsGlobal)
        return Valid();

      if (variantIds == null)
        return Invalid("Sorry, this discount code is not available or is expired.");

      if (discountCode.ProductVariants != null && discountCode.ProductVariants.Any())
      {
        return discountCode.ProductVariants.Select(v => v.Id).Intersect(variantIds).Any()
          ? Valid()
          : NotApplicable();
      }

      var orderVariants =
        variantIds.Select(id => _productVariantRepo.Find(id)).Where(v => v != null);

      if (discountCode.Products != null && discountCode.Products.Any())
      {
        var productIds = orderVariants.Select(v => v.ProductId);
        return discountCode.Products.Select(p => p.Id).Intersect(productIds).Any()
          ? Valid()
          : NotApplicable();
      }

      if (discountCode.Tours != null && discountCode.Tours.Any())
      {
        var tourIds = orderVariants.Select(v => v.Product.Departure.TourId);
        return discountCode.Tours.Select(t => t.Id).Intersect(tourIds).Any()
          ? Valid()
          : NotApplicable();
      }

      return Invalid("Invalid discount code.");
    }

    private DiscountCodeValidationResult Valid()
    {
      return new DiscountCodeValidationResult { IsValid = true };
    }

    private DiscountCodeValidationResult Invalid(string msg)
    {
      return new DiscountCodeValidationResult
      {
        IsValid = false,
        ErrorMessage = msg
      };
    }

    private DiscountCodeValidationResult NotApplicable()
    {
      return Invalid("Sorry, this discount code is not applicable to your order.");
    }
  }
}