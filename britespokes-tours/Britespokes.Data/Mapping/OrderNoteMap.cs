using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class OrderNoteMap : EntityTypeConfiguration<OrderNote>
  {
    public OrderNoteMap()
    {
      // Primary Key
      HasKey(n => n.Id);

      Property(n => n.OrderId).IsRequired();
      Property(n => n.TravelerId).IsOptional();
      Property(n => n.DisplayToCustomer).IsRequired();
    }
  }
}
