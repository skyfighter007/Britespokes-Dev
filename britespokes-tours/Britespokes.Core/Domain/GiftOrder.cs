using System;
using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
    public class GiftOrder : Entity
    {
        public GiftOrder()
        {
            CreatedAt = UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        public int GiftCardId { get; set; }
        public virtual GiftCard GiftCard { get; set; }

        public int? BillingAddressId { get; set; }
        public virtual Address BillingAddress { get; set; }
        // for future use
        public int? ShippingAddressId { get; set; }
        public virtual Address ShippingAddress { get; set; }

        public int OrderStatusId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

        public string OrderNumber { get; set; }
        public string SpecialInstructions { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public decimal ItemTotal { get; set; }
        public decimal AdjustmentTotal { get; set; }
        public string ChargeId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
        public DateTime? FailedAt { get; set; }
        public string LastFailureMessage { get; set; }

        public virtual List<GiftOrderDetail> GiftOrderDetail { get; set; }

    }
}
