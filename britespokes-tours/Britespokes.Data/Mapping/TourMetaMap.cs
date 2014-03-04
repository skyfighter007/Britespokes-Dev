using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class TourMetaMap : EntityTypeConfiguration<TourMeta>
  {
    public TourMetaMap()
    {
      ToTable("TourMeta");

      // Primary Key
      HasKey(t => t.TourId);

      Property(t => t.Description)
        .HasColumnType("ntext")
        .IsMaxLength()
        .IsRequired();

      Property(t => t.PriceIncludes1)
        .HasColumnType("ntext")
        .IsMaxLength()
        .IsOptional();

      Property(t => t.PriceIncludes2)
        .HasColumnType("ntext")
        .IsMaxLength()
        .IsOptional();

      Property(t => t.PriceIncludes3)
     .HasColumnType("ntext")
     .IsMaxLength()
     .IsOptional();

      Property(t => t.PriceIncludes4)
        .HasColumnType("ntext")
        .IsMaxLength()
        .IsOptional();

      Property(t => t.BriterPriceIncludes1)
     .HasColumnType("ntext")
     .IsMaxLength()
     .IsOptional();

      Property(t => t.BriterPriceIncludes2)
        .HasColumnType("ntext")
        .IsMaxLength()
        .IsOptional();

      Property(t => t.BriterPriceIncludes3)
     .HasColumnType("ntext")
     .IsMaxLength()
     .IsOptional();

      Property(t => t.BriterPriceIncludes4)
        .HasColumnType("ntext")
        .IsMaxLength()
        .IsOptional();

      Property(t => t.AdditionalInformation1)
        .HasColumnType("ntext")
        .IsMaxLength()
        .IsOptional();

      Property(t => t.AdditionalInformation2)
        .HasColumnType("ntext")
        .IsMaxLength()
        .IsOptional();

      HasRequired(t => t.Tour)
        .WithOptional(t => t.TourMeta);
    }
  }
}