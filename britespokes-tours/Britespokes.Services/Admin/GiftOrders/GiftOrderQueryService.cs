using System;
using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Orders;

namespace Britespokes.Services.Admin.GiftOrders
{
    public class GiftOrderQueryService : IGiftOrderQueryService
    {
        private readonly IRepository<GiftOrder> _giftorderRepo;
        private readonly IRepository<GiftOrderDetail> _giftorderdetailRepo;
        private readonly IRepository<GiftCard> _giftcardRepo;
        private readonly IRepository<DiscountCode> _discountCodeRepo;
        private readonly IRepository<Order> _OrderRepo;
        private readonly IRepository<GiftCardSummary> _giftcardsummary;

        public GiftOrderQueryService(IRepository<Order> OrderRepo, IRepository<GiftOrder> giftorderRepo, IRepository<DiscountCode> discountCodeRepo, IRepository<GiftOrderDetail> giftorderdetailRepo, IRepository<GiftCard> giftcardRepo, IRepository<GiftCardSummary> giftcardsummary)
        {
            _giftorderRepo = giftorderRepo;
            _discountCodeRepo = discountCodeRepo;
            _giftorderdetailRepo = giftorderdetailRepo;
            _giftcardRepo = giftcardRepo;
            _giftcardsummary = giftcardsummary;
            _OrderRepo = OrderRepo;
        }
        public GiftOrder FindGiftOrder(int orderId)
        {
            return _giftorderRepo.Find(orderId);
        }

        public IQueryable<GiftOrder> Orders()
        {
            return _giftorderRepo.All;
        }

        //public IQueryable<GiftOrder> AllGiftOrders()
        //{
        //    //return _giftorderRepo.All;

        //    var usedGiftOrders = GiftOrderByUsedstatus("Used");
        //    var unusedGiftOrders = GiftOrderByUnusedstatus("Unused");
        //    var allGiftOrders = usedGiftOrders.Concat(unusedGiftOrders)
        //                            .ToList();
        //    return allGiftOrders;
        //}
        
        
        public IQueryable<GiftOrder> CompletedOrders()
        {
            return _giftorderRepo.FindBy(o => o.OrderStatus.Name == OrderStatus.CompleteStatusName);
        }
        public IQueryable<GiftOrder> OrdersByStatus(string orderStatus)
        {
            return !OrderStatus.IsValidStatusName(orderStatus)
              ? _giftorderRepo.All
              : _giftorderRepo.FindBy(o => o.OrderStatus.Name == orderStatus);
        }

        public IQueryable<GiftOrder> OrdersByStatus(string orderStatus, DateTime? startDate, DateTime? endDate)
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
            return _giftorderRepo.All.GroupBy(o => o.OrderStatus.Name).Select(g => new StatusCount { Name = g.Key, Count = g.Count() });
        }


        public GiftOrderSummary GiftOrderSummary(int orderId)
        {
            return GiftOrderSummary(FindGiftOrder(orderId));
        }

        public GiftOrderSummary giftordersummary(int orderId, string orderStatus)
        {
            var giftorderSummary = GiftOrderSummary(FindGiftOrder(orderId));
            giftorderSummary.GiftOrder.OrderStatus.Name = orderStatus;
            return giftorderSummary;
        }

        public GiftOrderSummary GiftOrderSummary(GiftOrder giftorder)
        {
            List<GiftOrderDetails> orderdetails = new List<GiftOrderDetails>();


            var resultsUsed = _giftorderdetailRepo.All
             .Join(_giftorderRepo.All, d => d.GiftOrderId, o => o.Id, (d, o) => new { d, o })
                .Join(_giftcardsummary.All, t => t.d.Id, c => c.GiftOrderDetailId, (t, c) => new { t.d, t.o, c })
                .Join(_OrderRepo.All, p => p.c.OrderId, s => s.Id, (p, s) => new { p.c, p.d, p.o, s })
                .Where(a => a.o.OrderStatusId == 3)
                .Where(a => a.o.Id == giftorder.Id)
                .Where(a => a.s.OrderStatusId == 3)
                .Select(a => new GiftOrderDetails
                {
                    Id = a.d.Id,

                    Amount = a.d.Amount,

                    RecipientEmail = a.d.RecipientEmail,

                    YourName = a.d.YourName,

                    Message = a.d.Message,

                    RecipientGiftCode = a.d.RecipientGiftCode,

                    StatusName = "Used"
                }).ToList();

            var resultsUnUsed1 = _giftorderdetailRepo.All
             .Join(_giftorderRepo.All, d => d.GiftOrderId, o => o.Id, (d, o) => new { d, o })
                .Join(_giftcardsummary.All, t => t.d.Id, c => c.GiftOrderDetailId, (t, c) => new { t.d, t.o, c })
                .Join(_OrderRepo.All, p => p.c.OrderId, s => s.Id, (p, s) => new { p.c, p.d, p.o, s })
                .Where(a => a.o.OrderStatusId == 3)
                .Where(a => a.o.Id == giftorder.Id)
                .Where(a => a.s.OrderStatusId != 3)
                .Select(a => new GiftOrderDetails
                {
                    Id = a.d.Id,

                    Amount = a.d.Amount,

                    RecipientEmail = a.d.RecipientEmail,

                    YourName = a.d.YourName,

                    Message = a.d.Message,

                    RecipientGiftCode = a.d.RecipientGiftCode,

                    StatusName = "Unused"
                }).ToList();

            var resultsUnUsed2 = _giftorderdetailRepo.All
             .Join(_giftorderRepo.All, d => d.GiftOrderId, o => o.Id, (d, o) => new { d, o })
                .Where(a => a.o.OrderStatusId == 3)
                .Where(a => a.o.Id == giftorder.Id)
                .Select(a => new GiftOrderDetails
                {
                    Id = a.d.Id,

                    Amount = a.d.Amount,

                    RecipientEmail = a.d.RecipientEmail,

                    YourName = a.d.YourName,

                    Message = a.d.Message,

                    RecipientGiftCode = a.d.RecipientGiftCode,

                    StatusName = "Unused"
                }).ToList();

            foreach (var item in resultsUsed)
            {
                resultsUnUsed2.Remove(resultsUnUsed2.Single(s => s.Id == item.Id));

            }

            foreach (var item in resultsUnUsed1)
            {
                resultsUnUsed2.Remove(resultsUnUsed2.Single(s => s.Id == item.Id));

            }



            orderdetails = orderdetails.Concat(resultsUsed).ToList();
            orderdetails = orderdetails.Concat(resultsUnUsed1).ToList();
            orderdetails = orderdetails.Concat(resultsUnUsed2).ToList();

            var giftorderSummary = new GiftOrderSummary
            {
                Id = giftorder.Id,
                GiftOrder = giftorder,
                GiftOrderDetails = orderdetails


            };
            return giftorderSummary;
        }

        public List<GiftOrder> GiftOrderByUsedstatus(string orderStatus)
        {
            var results = _giftorderRepo.All
             .Join(_giftorderdetailRepo.All, d => d.Id, o => o.GiftOrderId, (d, o) => new { d, o})
                .Join(_giftcardsummary.All, t => t.o.Id, c => c.GiftOrderDetailId, (t, c) => new { t.d, t.o, c })
                .Join(_OrderRepo.All, p => p.c.OrderId, s => s.Id, (p, s) => new {p.c,p.d,p.o,s })
                .Where(a => a.s.OrderStatusId == 3)
                .Where(a => a.d.OrderStatusId == 3);
                

            List<GiftOrder> orderlist = new List<GiftOrder>();
            foreach (var p in results)
            {
                GiftOrder ord = new
               GiftOrder()
                {
                    User = p.s.User,
                    UserId = p.s.UserId,
                    GiftCardId = p.d.GiftCardId,
                    BillingAddressId = p.d.BillingAddressId,
                    ShippingAddressId = p.d.ShippingAddressId,
                    OrderStatusId = p.s.OrderStatusId,
                    OrderNumber = p.s.OrderNumber,
                    SpecialInstructions = p.s.SpecialInstructions,
                    Quantity = p.d.Quantity,
                    Total = p.d.Total,
                    ItemTotal = p.d.ItemTotal,
                    AdjustmentTotal = p.d.AdjustmentTotal,
                    ChargeId = p.d.ChargeId,
                    IsDeleted = p.d.IsDeleted,
                    CreatedAt = p.d.CreatedAt,
                    UpdatedAt = p.d.UpdatedAt,
                    CompletedAt = p.d.CompletedAt,
                    FailedAt = p.d.FailedAt,
                    LastFailureMessage = p.d.LastFailureMessage,
                    OrderStatus = p.s.OrderStatus,
                    Id = p.d.Id
                };
                ord.OrderStatus.Name = "Used";
                orderlist.Add(ord);
            }
            return orderlist;
        }
        public List<GiftOrder> GiftOrderByAlltatus(string orderStatus)
        {
            //return _giftorderRepo.All;
            // List<GiftOrder> _allGiftOrders = new List<GiftOrder>();

            var usedGiftOrders = GiftOrderByUsedstatus("Used");
            var unusedGiftOrders = GiftOrderByUnusedstatus("Unused");
            var allGiftOrders = usedGiftOrders.Concat(unusedGiftOrders)
                                    .ToList();
            return allGiftOrders;
        }
        public List<GiftOrder> GiftOrderByUnusedstatus(string orderStatus)
        {
            var results = _giftorderRepo.All
              .Where(t => t.OrderStatusId != 3);
            List<GiftOrder> lstgftorder = results.ToList();
            foreach (var item in lstgftorder)
            {
                item.OrderStatus.Name = "Unused";
            }
            return lstgftorder;
        }
    }


}
