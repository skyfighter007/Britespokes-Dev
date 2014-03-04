using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class CountryMap : EntityTypeConfiguration<Country>
  {
    public CountryMap()
    {
      // Primary Key
      HasKey(c => c.Id);

      Property(c => c.Iso)
        .HasMaxLength(2);

      Property(c => c.Name)
        .HasMaxLength(80);

      Property(c => c.PrintableName)
        .HasMaxLength(80);

      Property(c => c.Iso3)
        .HasMaxLength(3);
    }
  }
}
