using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class TravelerMap : EntityTypeConfiguration<Traveler>
  {
    public TravelerMap()
    {
      // Primary Key
      HasKey(t => t.Id);

      Property(t => t.ConfirmationNumber).IsRequired();

      HasMany(t => t.Notes)
        .WithOptional(n => n.Traveler);
    }
  }
}
