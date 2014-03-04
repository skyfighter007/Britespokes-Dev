using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
    public class GiftCardSummaryMap : EntityTypeConfiguration<GiftCardSummary>
    {
        public GiftCardSummaryMap()
        {
            // Primary Key
            HasKey(t => t.Id);
           
        }
    }
}
