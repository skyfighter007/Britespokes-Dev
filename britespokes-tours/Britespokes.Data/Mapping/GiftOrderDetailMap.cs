using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
    public class GiftOrderDetailMap : EntityTypeConfiguration<GiftOrderDetail>
    {
        public GiftOrderDetailMap()
        {
            // Primary Key
            HasKey(t => t.Id);
           
        }
    }
}
