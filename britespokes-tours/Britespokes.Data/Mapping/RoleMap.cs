using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class RoleMap : EntityTypeConfiguration<Role>
  {
    public RoleMap()
    {
      // Primary Key
      HasKey(r => r.Id);

      Property(r => r.Name)
        .IsRequired();
    }
  }
}
