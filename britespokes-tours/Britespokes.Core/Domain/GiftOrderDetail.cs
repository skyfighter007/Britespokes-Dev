using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Britespokes.Core.Domain
{
    public class GiftOrderDetail : Entity
    {

        public decimal Amount { get; set; }

        public string RecipientEmail { get; set; }

        public string YourName { get; set; }

        public string Message { get; set; }

        public int GiftOrderId { get; set; }
        public virtual GiftOrder GiftOrder { get; set; }

        public virtual List<GiftCardSummary> GiftCardSummary { get; set; }

        public string RecipientGiftCode { get; set; }

    }
}
