using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class ProductVariantMap : EntityTypeConfiguration<ProductVariant>
  {
    public ProductVariantMap()
    {
      // Primary Key
      HasKey(p => p.Id);

      HasMany(v => v.Images)
        .WithRequired(a => a.ProductVariant);
    }
  }
}
