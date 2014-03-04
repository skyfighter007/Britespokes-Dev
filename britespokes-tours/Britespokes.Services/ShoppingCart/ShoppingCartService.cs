using System;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using Britespokes.Services.Orders;

namespace Britespokes.Services.ShoppingCart
{
  public class ProductVariantNotEnabledException : Exception
  {
    public ProductVariantNotEnabledException(string message) : base(message) {}
  }

  public class ShoppingCartService : IShoppingCartService
  {
    private readonly IOrderService _orderService;
    private readonly IRepository<ShoppingCartItem> _shoppingCartRepository;

    public ShoppingCartService(IOrderService orderService, IRepository<ShoppingCartItem> shoppingCartRepository)
    {
      _orderService = orderService;
      _shoppingCartRepository = shoppingCartRepository;
    }

    public void ProcessRequest(BookingRequest bookingRequest)
    {
      foreach (var productRequest in bookingRequest.ProductRequests)
      {
        if (productRequest.Quantity > 0)
        {
          if (!_orderService.IsProductVariantEnabled(productRequest.VariantId))
            throw new ProductVariantNotEnabledException("Product is not available");
          var shoppingCartItem = new ShoppingCartItem
            {
              ProductVariantId = productRequest.VariantId,
              Quantity = productRequest.Quantity,
              UserId = bookingRequest.UserId
            };
          _shoppingCartRepository.Add(shoppingCartItem);
        }
      }
    }

    public Order CreatePendingOrder(int userId)
    {
      var order = _orderService.CreateOrder(userId, UserCart(userId).ToList());
      ClearCart(userId);
      return order;
    }

    public IQueryable<ShoppingCartItem> UserCart(int userId)
    {
      return _shoppingCartRepository.FindBy(i => i.UserId == userId, null, "ProductVariant");
    }

    public IQueryable<ShoppingCartItem> UserCart(User user)
    {
      return UserCart(user.Id);
    }

    public void ClearCart(int userId)
    {
      UserCart(userId).ToList().ForEach(item => _shoppingCartRepository.Delete(item));
    }

    public void ClearCart(User user)
    {
      ClearCart(user.Id);
    }
  }
}
