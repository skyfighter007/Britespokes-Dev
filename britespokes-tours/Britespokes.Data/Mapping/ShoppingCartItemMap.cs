using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class ShoppingCartItemMap : EntityTypeConfiguration<ShoppingCartItem>
  {
    public ShoppingCartItemMap()
    {
      // Primary Key
      HasKey(p => p.Id);

      HasRequired(v => v.ProductVariant);
    }
  }
}
