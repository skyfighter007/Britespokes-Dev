using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.GiftOrders
{
    public class GiftOrderDetails
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public string RecipientEmail { get; set; }

        public string YourName { get; set; }

        public string Message { get; set; }

        public string RecipientGiftCode { get; set; }

        public string StatusName { get; set; }
    }


  public class GiftOrderSummary
  {
    public int Id { get; set; }
    public GiftOrder GiftOrder { get; set; }
    public List<GiftOrderDetails> GiftOrderDetails { get; set; }
    public GiftCard[] GiftCards;
  }

  
}
