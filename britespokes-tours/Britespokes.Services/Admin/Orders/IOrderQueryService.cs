using System;
using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Orders
{
  public interface IOrderQueryService
  {
    Order FindOrder(int orderId);
    IQueryable<Order> Orders();
    IQueryable<Order> CompletedOrders();
    IQueryable<Order> OrdersByStatus(string orderStatus);
    IQueryable<Order> OrdersByStatus(string orderStatus, DateTime? startDate, DateTime? endDate);
    IEnumerable<StatusCount> CountsByStatus();

    OrderSummary OrderSummary(int orderId);
    OrderSummary OrderSummary(Order order);
  }
}