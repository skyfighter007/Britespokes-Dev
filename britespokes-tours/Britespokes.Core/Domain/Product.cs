using System;
using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
  public class Product : Entity
  {
    public Product()
    {
      CreatedAt = UpdatedAt = DateTime.UtcNow;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? AvailableOn { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

   

    public virtual List<Organization> Organizations { get; set; }
    public virtual List<ProductVariant> ProductVariants { get; set; }
    public virtual List<DiscountCode> DiscountCodes { get; set; }

    public virtual Departure Departure { get; set; }
  }
}
