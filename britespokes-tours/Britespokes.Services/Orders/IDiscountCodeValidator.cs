using Britespokes.Core.Domain;

namespace Britespokes.Services.Orders
{
  public interface IDiscountCodeValidator
  {
    DiscountCodeValidationResult Validate(string code, int[] variantIds);
  }
}
