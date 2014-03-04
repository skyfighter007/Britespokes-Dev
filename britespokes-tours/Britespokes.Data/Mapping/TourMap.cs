using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class TourMap : EntityTypeConfiguration<Tour>
  {
    public TourMap()
    {
      // Primary Key
      HasKey(t => t.Id);

      Property(t => t.Name)
        .IsRequired();

      Property(t => t.Name)
        .IsRequired();

      Property(t => t.Permalink)
        .HasMaxLength(255)
        .IsRequired();

      Property(t => t.Code)
        .HasMaxLength(50)
        .IsRequired();

      HasMany(t => t.Departures)
        .WithRequired(a => a.Tour)
        .WillCascadeOnDelete(true);

      HasMany(t => t.TourTimelineItems)
        .WithRequired(i => i.Tour)
        .WillCascadeOnDelete(true);

      HasOptional(t => t.TourMeta)
        .WithRequired(m => m.Tour)
        .WillCascadeOnDelete(true);

      HasMany(t => t.Experiences)
        .WithRequired(e => e.Tour);
    }
  }
}
