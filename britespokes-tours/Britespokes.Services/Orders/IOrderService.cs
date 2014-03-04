using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Orders
{
  public interface IOrderService
  {
    OrderStatus StatusPending();
    OrderStatus StatusProcessing();
    OrderStatus StatusComplete();
    OrderStatus StatusCancelled();

    Order CreateOrder(int userId, List<ShoppingCartItem> shoppingCartItems);
    Order FindOrder(int orderId, int userId);
    Traveler FindTraveler(int travelerId);
    void UpdateTravelers(ConfirmTravelers confirmTravelers);
    Order UpdateBillingDetails(BillingDetails billingDetails);
    BillingDetails BuildBillingDetails(Order order, User user = null);
    BillingOverview BuildBillingOverview(Order order);
    Order CompleteOrder(Order order, string chargeId);
    Order FailOrder(Order order, string message);

    Order LastOrderForUser(User user);
    IEnumerable<State> States();

    void ApplyDiscountCode(Order order, string discountCode);
    bool IsProductVariantEnabled(int productVariantId);

    IQueryable<DiscountCode> DiscountCodesForOrder(Order order);

    bool ValidateGiftCode(string Code, string Email, ref decimal GiftAmount,ref int OrderDetailId);
    void AddGiftCardSummaries(BillingDetails billingdetails);
  }
}
