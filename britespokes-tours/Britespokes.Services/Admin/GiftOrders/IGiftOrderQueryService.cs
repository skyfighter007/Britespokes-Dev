using System;
using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Orders;

namespace Britespokes.Services.Admin.GiftOrders
{
    public interface IGiftOrderQueryService
  {
    GiftOrder FindGiftOrder(int orderId);
    IQueryable<GiftOrder> Orders();
    IQueryable<GiftOrder> CompletedOrders();
    IQueryable<GiftOrder> OrdersByStatus(string orderStatus);
    IQueryable<GiftOrder> OrdersByStatus(string orderStatus, DateTime? startDate, DateTime? endDate);
    IEnumerable<StatusCount> CountsByStatus();

    GiftOrderSummary GiftOrderSummary(int orderId);
    GiftOrderSummary GiftOrderSummary(GiftOrder giftorder);

    GiftOrderSummary giftordersummary(int orderId, string orderstatus);

    List<GiftOrder> GiftOrderByUnusedstatus(string orderStatus);
    List<GiftOrder> GiftOrderByUsedstatus(string orderStatus);
    List<GiftOrder> GiftOrderByAlltatus(string orderStatus);
    //IQueryable<GiftOrder> AllGiftOrders();
  }
}