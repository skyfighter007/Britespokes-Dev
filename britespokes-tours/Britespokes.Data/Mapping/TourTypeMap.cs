using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class TourTypeMap : EntityTypeConfiguration<TourType>
  {
    public TourTypeMap()
    {
      // Primary Key
      HasKey(t => t.Id);

      Property(t => t.Name)
        .HasMaxLength(255)
        .IsRequired();

      Property(t => t.DisplayName)
        .HasMaxLength(255)
        .IsRequired();

      HasMany(t => t.Tours).WithRequired(t => t.TourType);
    }
  }
}
