using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using System;
using System.Collections.Generic;
using Britespokes.Services.Users;
using Omu.ValueInjecter;

namespace Britespokes.Services.GiftCards
{
  public class GiftCardOrderService : IGiftCardOrderService
  {
      private readonly IRepository<GiftOrder> _giftCardOrderRepository;
      private readonly IUserService _userService;
      private readonly IRepository<OrderStatus> _orderStatusRepository;
      private readonly IRepository<GiftOrderDetail> _giftOrderDetailRepository;
      private readonly IRepository<Country> _countriesRepo;
      private readonly IRepository<State> _statesRepo;

      public GiftCardOrderService(IRepository<GiftOrder> giftCardOrderRepository, IUserService userService, IRepository<OrderStatus> orderStatusRepository, IRepository<GiftOrderDetail> giftOrderDetailRepository, IRepository<Country> countriesRepo, IRepository<State> statesRepo)
      {
          _giftCardOrderRepository = giftCardOrderRepository;
          _userService = userService;
          _orderStatusRepository = orderStatusRepository;
          _giftOrderDetailRepository = giftOrderDetailRepository;
          _countriesRepo = countriesRepo;
          _statesRepo = statesRepo;
      }

      public OrderStatus StatusPending()
      {
          return FindStatus(OrderStatus.PendingStatusName);
      }

      public OrderStatus StatusProcessing()
      {
          return FindStatus(OrderStatus.ProcessingStatusName);
      }

      public OrderStatus StatusComplete()
      {
          return FindStatus(OrderStatus.CompleteStatusName);
      }

      public OrderStatus StatusCancelled()
      {
          return FindStatus(OrderStatus.CancelledStatusName);
      }

      public OrderStatus StatusFailed()
      {
          return FindStatus(OrderStatus.FailedStatusName);
      }

      private OrderStatus FindStatus(string name)
      {
          return _orderStatusRepository.FindBy(s => s.Name == name).Single();
      }
      public GiftOrder CompleteOrder(GiftOrder order, string chargeId)
      {
          order.CompletedAt = DateTime.UtcNow;
          order.FailedAt = null;
          order.LastFailureMessage = null;
          order.ChargeId = chargeId;
          order.OrderStatus = StatusComplete();
          _giftCardOrderRepository.Update(order);
          return order;
      }

      public GiftOrder FailOrder(GiftOrder order, string message)
      {
          order.LastFailureMessage = message;
          order.FailedAt = DateTime.UtcNow;
          order.OrderStatus = StatusFailed();
          _giftCardOrderRepository.Update(order);
          return order;
      }

      public GiftOrder CreateOrder(GiftOrder giftOrder)
      {
          var user = _userService.Find(giftOrder.UserId);
          var order = new GiftOrder
          {
              User = user,
              OrderStatus = StatusPending(),
              OrderNumber = GenerateOrderNumber(),
              Quantity = giftOrder.Quantity,
              GiftCardId=giftOrder.GiftCardId
          };

          //foreach (var shoppingCartItem in shoppingCartItems)
          //    AddProductVariant(order, shoppingCartItem);

          order.GiftOrderDetail = BuildGiftOrderDetailList(order);
         // OrderCalculator.Calculate(order);
          _giftCardOrderRepository.Add(order);

          return order;
      }

      private static List<GiftOrderDetail> BuildGiftOrderDetailList(GiftOrder order)
      {
          var giftCardCount = order.Quantity;
          var giftCards = new List<GiftOrderDetail>(giftCardCount);
          for (var i = 0; i < giftCardCount; i++)
              giftCards.Add(new GiftOrderDetail { RecipientGiftCode = GenerateOrderNumber() });
          return giftCards;
      }

      public IQueryable<GiftOrder> All()
    {
        return _giftCardOrderRepository.All;
    }

      public GiftOrder Find(int id)
    {
        return _giftCardOrderRepository.Find(id);
    }

      public GiftOrder FindOrder(int orderId, int userId)
      {
          return _giftCardOrderRepository
            .FindBy(o => o.Id == orderId && o.UserId == userId, null)
            .Single();
      }

      public GiftOrderDetail FindGiftOrderDetail(int GiftOrderDetailId)
      {
          return _giftOrderDetailRepository.Find(GiftOrderDetailId);
      }

      public void UpdateGiftOrderDetails(ConfirmGiftCards confirmGiftCards)
      {
          var order = _giftCardOrderRepository.Find(confirmGiftCards.GiftOrderId);
          decimal total = 0;
          foreach (var details in confirmGiftCards.GiftOrderDetail)
          {
              var orderDetail = FindGiftOrderDetail(details.Id);
              orderDetail.InjectFrom(details);
              _giftOrderDetailRepository.Update(orderDetail);

              total = total + details.Amount;
          }
          order.Total = total;
          order.ItemTotal = total;
          _giftCardOrderRepository.Update(order);
      }

      public BillingDetails BuildBillingDetails(GiftOrder order, User user = null)
      {
          var billingOverview = BuildBillingOverview(order);

          var billingDetails = new BillingDetails
          {
              GiftOrderId = order.Id,
              GiftOrderNumber = order.OrderNumber,              
              UserId = order.UserId,
              CountryId = 226, // TODO: hardcoded country for now
              BillingOverview = billingOverview
          };

          if (user != null)
          {
              billingDetails.FirstName = user.FirstName;
              billingDetails.LastName = user.LastName;
              billingDetails.Email = user.Email;

              var lastOrder = LastOrderForUser(user);
              if (lastOrder != null)
              {
                  var billingAddress = lastOrder.BillingAddress;
                  if (billingAddress != null)
                  {
                      billingDetails.Address1 = billingAddress.Address1;
                      billingDetails.Address2 = billingAddress.Address2;
                      billingDetails.City = billingAddress.City;
                      billingDetails.StateOrProvince = billingAddress.StateOrProvince;
                      billingDetails.ZipCode = billingAddress.ZipCode;
                  }
              }
          }

          return billingDetails;
      }

      public GiftOrder LastOrderForUser(User user)
      {
          return _giftCardOrderRepository.FindBy(o => o.UserId == user.Id && o.BillingAddressId != null)
                                 .OrderByDescending(o => o.CreatedAt)
                                 .FirstOrDefault();
      }
      public BillingOverview BuildBillingOverview(GiftOrder order)
      {
          var giftCard = order.GiftCard;


          return new BillingOverview
          {
              GiftCardName = giftCard.Name,             
              NumberOfCards = order.Quantity,
              Total = order.Total,
              ItemTotal = order.ItemTotal,
              Countries = Countries(),
              States = States()
          };
      }
      private IEnumerable<Country> Countries()
      {
          return _countriesRepo.All.ToList();
      }

      public IEnumerable<State> States()
      {
          return _statesRepo.All.ToList();
      }


      private static string GenerateOrderNumber()
      {
          var i = Guid.NewGuid().ToByteArray().Aggregate(1, (current, b) => current * (b + 1));
          return string.Format("{0:x}", i - DateTime.Now.Ticks);
      }


    //public Tour FindByCode(string code)
    //{
    //    return _giftCardRepository.FindBy(t => t.Code == code).SingleOrDefault();
    //}


      public GiftOrder UpdateBillingDetails(BillingDetails billingDetails)
      {
          var order = FindOrder(billingDetails.GiftOrderId, billingDetails.UserId);
          var billingAddress = order.BillingAddress ?? new Address();
          billingAddress.InjectFrom(billingDetails);
          order.BillingAddress = billingAddress;
          order.SpecialInstructions = billingDetails.SpecialInstructions;

         
          _giftCardOrderRepository.Update(order);

          var user = order.User;
          if (string.IsNullOrEmpty(user.FirstName))
              user.FirstName = billingDetails.FirstName;
          if (string.IsNullOrEmpty(user.LastName))
              user.LastName = billingDetails.LastName;
          _userService.UpdateUser(user);

          return order;
      }
  }
}
