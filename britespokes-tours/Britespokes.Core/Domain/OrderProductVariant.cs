using System;

namespace Britespokes.Core.Domain
{
  public class OrderProductVariant : Entity
  {
    public OrderProductVariant()
    {
      CreatedAt = UpdatedAt = DateTime.UtcNow;
    }

    public int OrderId { get; set; }
    public virtual Order Order { get; set; }

    public int ProductVariantId { get; set; }
    public virtual ProductVariant ProductVariant { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
