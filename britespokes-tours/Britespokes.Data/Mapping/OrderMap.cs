using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class OrderMap : EntityTypeConfiguration<Order>
  {
    public OrderMap()
    {
      // Primary Key
      HasKey(o => o.Id);

      Property(o => o.UserId).IsRequired();
      Property(o => o.OrderNumber)
        .HasMaxLength(255)
        .IsRequired();

      Property(u => u.Total).IsRequired();

      HasOptional(u => u.BillingAddress);
      HasOptional(u => u.ShippingAddress);

      HasMany(o => o.ProductVariants)
        .WithRequired(v => v.Order)
        .WillCascadeOnDelete();

      HasMany(o => o.Travelers)
        .WithRequired(t => t.Order)
        .WillCascadeOnDelete(true);

      HasMany(o => o.Notes)
        .WithRequired(t => t.Order)
        .WillCascadeOnDelete();
    }
  }
}
