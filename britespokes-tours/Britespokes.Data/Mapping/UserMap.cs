using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class UserMap : EntityTypeConfiguration<User>
  {
    public UserMap()
    {
      // Primary Key
      HasKey(t => t.Id);

      Property(t => t.OrganizationId)
        .IsRequired();

      HasOptional(u => u.Address);

      HasMany(u => u.Roles)
        .WithMany(r => r.Users)
        .Map(m =>
        {
          m.MapLeftKey("UserId");
          m.MapRightKey("RoleId");
          m.ToTable("UserRoles");
        });
    }
  }
}