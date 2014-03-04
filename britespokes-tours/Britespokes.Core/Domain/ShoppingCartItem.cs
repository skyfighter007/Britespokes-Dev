using System;

namespace Britespokes.Core.Domain
{
  public class ShoppingCartItem : Entity
  {
    public ShoppingCartItem()
    {
      CreatedAt = UpdatedAt = DateTime.UtcNow;
    }

    public int UserId { get; set; }
    public virtual User User { get; set; }

    public int ProductVariantId { get; set; }
    public virtual ProductVariant ProductVariant { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
