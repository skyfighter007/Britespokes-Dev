using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class ExperienceMap : EntityTypeConfiguration<Experience>
  {
    public ExperienceMap()
    {
      HasKey(e => e.Id);
    }
  }
}