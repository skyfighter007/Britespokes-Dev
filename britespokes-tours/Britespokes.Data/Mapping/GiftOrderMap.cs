using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
    public class GiftOrderMap : EntityTypeConfiguration<GiftOrder>
    {
        public GiftOrderMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            Property(t => t.GiftCardId)
              .IsRequired();

            Property(t => t.UserId)
              .IsRequired();

            HasOptional(u => u.BillingAddress);
            HasOptional(u => u.ShippingAddress);

            HasRequired(o => o.User)
             .WithMany()
             .WillCascadeOnDelete(false);

            HasRequired(o => o.GiftCard)
            .WithMany()
            .WillCascadeOnDelete(false);

            HasRequired(o => o.OrderStatus)
          .WithMany()
          .WillCascadeOnDelete(false);

            HasMany(o => o.GiftOrderDetail)
            .WithRequired(t => t.GiftOrder)
            .WillCascadeOnDelete(false);

        }
    }
}
