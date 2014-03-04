using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class DiscountCodeMap : EntityTypeConfiguration<DiscountCode>
  {
    public DiscountCodeMap()
    {
      // Primary Key
      HasKey(t => t.Id);

      Property(t => t.Code)
        .IsRequired();

      HasMany(d => d.Tours)
        .WithMany(t => t.DiscountCodes)
        .Map(m =>
        {
          m.MapLeftKey("DiscountCodeId");
          m.MapRightKey("TourId");
          m.ToTable("TourDiscountCodes");
        });

      HasMany(d => d.Products)
        .WithMany(p => p.DiscountCodes)
        .Map(m =>
        {
          m.MapLeftKey("DiscountCodeId");
          m.MapRightKey("ProductId");
          m.ToTable("ProductDiscountCodes");
        });

      HasMany(d => d.ProductVariants)
        .WithMany(p => p.DiscountCodes)
        .Map(m =>
        {
          m.MapLeftKey("DiscountCodeId");
          m.MapRightKey("ProductVariantId");
          m.ToTable("ProductVariantDiscountCodes");
        });
    }
  }
}