using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class AvailabilityStatusMap : EntityTypeConfiguration<AvailabilityStatus>
  {
    public AvailabilityStatusMap()
    {
      // Primary Key
      HasKey(s => s.Id);

      Property(s => s.Name)
        .HasMaxLength(255)
        .IsRequired();
    }
  }
}