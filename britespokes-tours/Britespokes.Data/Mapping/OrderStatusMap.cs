using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class OrderStatusMap : EntityTypeConfiguration<OrderStatus>
  {
    public OrderStatusMap()
    {
      // Primary Key
      HasKey(s => s.Id);

      Property(s => s.Name)
        .HasMaxLength(255)
        .IsRequired();
    }
  }
}