using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.ShoppingCart
{
  public interface IShoppingCartService
  {
    void ProcessRequest(BookingRequest bookingRequest);
    Order CreatePendingOrder(int userId);
    IQueryable<ShoppingCartItem> UserCart(int userId);
    IQueryable<ShoppingCartItem> UserCart(User user);
    void ClearCart(int userId);
    void ClearCart(User user);
  }
}
