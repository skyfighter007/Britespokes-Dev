using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class DepartureMap : EntityTypeConfiguration<Departure>
  {
    public DepartureMap()
    {
      // Primary Key
      HasKey(t => t.ProductId);

      Property(t => t.Code)
        .HasMaxLength(50)
        .IsRequired();

      Property(t => t.DepartureDate)
        .IsRequired();

      Property(t => t.ReturnDate)
        .IsRequired();

      HasRequired(t => t.Product)
        .WithOptional(p => p.Departure);
    }
  }
}