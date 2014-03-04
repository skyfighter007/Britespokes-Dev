using System.Linq;
using Britespokes.Core.Domain;
using System.Collections.Generic;

namespace Britespokes.Services.GiftCards
{
    public interface IGiftCardOrderService
  {
      OrderStatus StatusPending();
      OrderStatus StatusProcessing();
      OrderStatus StatusComplete();
      OrderStatus StatusCancelled();
      GiftOrder UpdateBillingDetails(BillingDetails billingDetails);

      BillingDetails BuildBillingDetails(GiftOrder order, User user = null);
      BillingOverview BuildBillingOverview(GiftOrder order);
      GiftOrder CreateOrder(GiftOrder order);
      GiftOrder FindOrder(int orderId, int userId);
      GiftOrderDetail FindGiftOrderDetail(int GiftOrderDetailId);
      void UpdateGiftOrderDetails(ConfirmGiftCards confirmGiftCards);
      IQueryable<GiftOrder> All();
      GiftOrder Find(int id);

      GiftOrder CompleteOrder(GiftOrder order, string chargeId);
      GiftOrder FailOrder(GiftOrder order, string message);

   
  }
}