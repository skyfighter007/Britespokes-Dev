using System;
using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
  public class ProductVariant : Entity
  {
    public ProductVariant()
    {
      IsEnabled = true;
    }

    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string PluralDisplayName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal PriceForBriter { get; set; }
    public bool IsMaster { get; set; }
    public bool IsEnabled { get; set; }

    public int MaxCapacity { get; set; }
    public int MinCapacity { get; set; }
    public int MaxChildren { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
    public virtual List<ProductAsset> Images { get; set; }

    public virtual List<DiscountCode> DiscountCodes { get; set; }
  }
}
