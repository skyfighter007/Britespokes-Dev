using System;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Orders
{
  public class DiscountCodeApplier
  {
    private readonly DiscountCode _discountCode;

    public DiscountCodeApplier(DiscountCode discountCode)
    {
      _discountCode = discountCode;
    }

    public decimal DiscountAmount(decimal amount)
    {
      var discountAmount = decimal.Zero;
      if (!IsValid()) return discountAmount;

      if (_discountCode.UsePercentage)
      {
        discountAmount = (amount*(_discountCode.Percentage/100.0m));
      }
      else
      {
        discountAmount = _discountCode.Amount;
      }

      return discountAmount;
    }

    public decimal DiscountedTotal(decimal total)
    {
      return Math.Max(decimal.Zero, (total - DiscountAmount(total)));
    }

    public DiscountAdjustment DiscountAdjustment(decimal orderTotal)
    {
      return new DiscountAdjustment
      {
        Amount = DiscountAmount(orderTotal) * -1,
        DiscountCode = _discountCode,
        Description = _discountCode.Code,
        IsActive = true
      };
    }

    private bool IsValid()
    {
      return _discountCode.IsActive && !_discountCode.IsExpired();
    }
  }
}