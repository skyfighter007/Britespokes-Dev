using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class ProductMap : EntityTypeConfiguration<Product>
  {
    public ProductMap()
    {
      // Primary Key
      HasKey(p => p.Id);

      Property(p => p.Name)
        .IsRequired();

      HasMany(v => v.Organizations)
        .WithMany(o => o.Products)
        .Map(m =>
        {
          m.MapLeftKey("ProductId");
          m.MapRightKey("OrganizationId");
          m.ToTable("OrganizationProducts");
        });

      HasMany(p => p.ProductVariants)
        .WithRequired(v => v.Product);
    }
  }
}
