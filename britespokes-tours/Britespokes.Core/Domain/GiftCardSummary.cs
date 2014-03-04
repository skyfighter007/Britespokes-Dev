using System;
using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
    public class GiftCardSummary : Entity
    {
        public GiftCardSummary()
    {
      CreatedAt = DateTime.UtcNow;
    }

        public decimal UsedAmount { get; set; }

        public DateTime CreatedAt { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int GiftOrderDetailId { get; set; }
        public virtual GiftOrderDetail GiftOrderDetail { get; set; }

    }
}