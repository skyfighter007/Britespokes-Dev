using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class TourTimelineItemMap : EntityTypeConfiguration<TourTimelineItem>
  {
    public TourTimelineItemMap()
    {
      HasKey(t => t.Id);

      Property(t => t.ImageUrl)
        .IsRequired();

      Property(t => t.Caption)
        .IsRequired();
    }
  }
}