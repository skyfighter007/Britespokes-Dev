using System;
using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
  public class DiscountCode : Entity
  {
    public DiscountCode()
    {
      CreatedAt = UpdatedAt = DateTime.UtcNow;
    }

    public string Code { get; set; }
    public string LowerCode { get; set; }
    public DateTime? StartsAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public bool IsActive { get; set; }

    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }
    public bool UsePercentage { get; set; }
    public bool IsGlobal { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual List<DiscountAdjustment> DiscountAdjustments { get; set; }

    public virtual List<Tour> Tours { get; set; }
    public virtual List<Product> Products { get; set; }
    public virtual List<ProductVariant> ProductVariants { get; set; }


    public bool IsExpired()
    {
      return IsExpired(DateTime.UtcNow);
    }

    public bool IsExpired(DateTime compare)
    {
      return !(compare >= StartsAt && compare <= ExpiresAt);
    }
  }
}