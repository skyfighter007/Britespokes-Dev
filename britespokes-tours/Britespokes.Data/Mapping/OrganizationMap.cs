using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class OrganizationMap : EntityTypeConfiguration<Organization>
  {
    public OrganizationMap()
    {
      // Primary Key
      HasKey(t => t.Id);

      Property(t => t.Name)
        .HasMaxLength(255)
        .IsRequired();

      HasMany(t => t.Images)
        .WithRequired(a => a.Organization)
        .WillCascadeOnDelete(false);

      HasMany(t => t.EmailDomains)
        .WithRequired(d => d.Organization);
    }
  }
}
