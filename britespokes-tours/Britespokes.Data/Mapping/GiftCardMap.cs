using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
    public class GiftCardMap : EntityTypeConfiguration<GiftCard>
    {
        public GiftCardMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            Property(t => t.Name)
              .IsRequired();

            Property(t => t.GiftCode)
              .IsRequired();

            Property(t => t.Permalink)
           .HasMaxLength(255)
           .IsRequired();
        }
    }
}
