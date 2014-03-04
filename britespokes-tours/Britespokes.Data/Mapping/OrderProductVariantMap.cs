using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class OrderProductVariantMap : EntityTypeConfiguration<OrderProductVariant>
  {
    public OrderProductVariantMap()
    {
      // Primary Key
      HasKey(v => v.Id);

      Property(v => v.OrderId).IsRequired();
      Property(v => v.ProductVariantId).IsRequired();
      Property(v => v.Quantity).IsRequired();
      Property(v => v.UnitPrice).IsRequired();
    }
  }
}
