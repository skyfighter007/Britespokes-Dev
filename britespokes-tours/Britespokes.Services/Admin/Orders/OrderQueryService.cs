using System;
using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Orders
{
  public class OrderQueryService : IOrderQueryService
  {
    private readonly IRepository<Order> _orderRepo;
    private readonly IRepository<DiscountCode> _discountCodeRepo;

    public OrderQueryService(IRepository<Order> orderRepo, IRepository<DiscountCode> discountCodeRepo)
    {
      _orderRepo = orderRepo;
      _discountCodeRepo = discountCodeRepo;
    }

    public Order FindOrder(int orderId)
    {
      return _orderRepo.Find(orderId);
    }

    public IQueryable<Order> Orders()
    {
      return _orderRepo.All;
    }

    public IQueryable<Order> CompletedOrders()
    {
      return _orderRepo.FindBy(o => o.OrderStatus.Name == OrderStatus.CompleteStatusName);
    }

    public IQueryable<Order> OrdersByStatus(string orderStatus)
    {
      return !OrderStatus.IsValidStatusName(orderStatus)
        ? _orderRepo.All
        : _orderRepo.FindBy(o => o.OrderStatus.Name == orderStatus);
    }

    public IQueryable<Order> OrdersByStatus(string orderStatus, DateTime? startDate, DateTime? endDate)
    {
      var ordersByStatus = OrdersByStatus(orderStatus);
      if (startDate.HasValue)
      {
        var dtCompare = DateTime.SpecifyKind(startDate.Value, DateTimeKind.Utc).AddDays(-1);
        ordersByStatus = ordersByStatus.Where(o => o.UpdatedAt > dtCompare);
      }
      if (endDate.HasValue)
      {
        var dtCompare = DateTime.SpecifyKind(endDate.Value, DateTimeKind.Utc).AddDays(1);
        ordersByStatus = ordersByStatus.Where(o => o.UpdatedAt < dtCompare);
      }
      return ordersByStatus;
    }

    public IEnumerable<StatusCount> CountsByStatus()
    {
      return _orderRepo.All.GroupBy(o => o.OrderStatus.Name).Select(g => new StatusCount { Name = g.Key, Count = g.Count() });
    }

    public OrderSummary OrderSummary(int orderId)
    {
      return OrderSummary(FindOrder(orderId));
    }

    public OrderSummary OrderSummary(Order order)
    {
      var orderSummary = new OrderSummary
      {
        Id = order.Id,
        Order = order,
        OrderVariants = new Dictionary<string, int>()
      };

      var departures = new List<Departure>();
      var tours = new List<Tour>();

      foreach (var orderProductVariant in order.ProductVariants)
      {
        var productVariant = orderProductVariant.ProductVariant;
        if (productVariant != null)
        {
          orderSummary.OrderVariants.Add(productVariant.Name, orderProductVariant.Quantity);
          var product = productVariant.Product;
          if (product != null)
          {
            var departure = product.Departure;
            if (departure != null)
            {
              departures.Add(departure);
              var tour = departure.Tour;
              if (tour != null)
                tours.Add(tour);
            }
          }
        }
      }

      orderSummary.DiscountCodes = (from discountAdjustment in order.Adjustments.OfType<DiscountAdjustment>()
        let codeId = discountAdjustment.DiscountCodeId
        where codeId != null
        where codeId.HasValue
        select codeId.Value
        into discountCodeId
        select _discountCodeRepo.Find(discountCodeId)).ToArray();
      orderSummary.Departures = departures.ToArray();
      orderSummary.Tours = tours.ToArray();
      return orderSummary;
    }
  }
}