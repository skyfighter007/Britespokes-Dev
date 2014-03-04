using System;
using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using Britespokes.Services.Users;
using Omu.ValueInjecter;

namespace Britespokes.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<OrderStatus> _orderStatusRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Traveler> _travelerRepository;
        private readonly IRepository<Country> _countriesRepo;
        private readonly IRepository<State> _statesRepo;
        private readonly IRepository<ProductVariant> _productVariantRepo;
        private readonly IUserService _userService;
        private readonly IDiscountCodeQueryService _discountCodeQueryService;


        private readonly IRepository<GiftCard> _giftcardRepository;
        private readonly IRepository<GiftOrder> _giftorderRepository;
        private readonly IRepository<GiftOrderDetail> _giftorderdetailRepository;
        private readonly IRepository<GiftCardSummary> _giftcardsummaryRepository;

        public OrderService(IRepository<GiftCardSummary> giftcardsummaryRepository, IRepository<GiftOrderDetail> giftorderdetailRepository, IRepository<GiftOrder> giftorderRepository, IRepository<GiftCard> giftcardRepository, IRepository<OrderStatus> orderStatusRepository, IRepository<Order> orderRepository, IUserService userService, IRepository<Traveler> travelerRepository, IRepository<Country> countriesRepo, IRepository<State> statesRepo, IRepository<ProductVariant> productVariantRepo, IDiscountCodeQueryService discountCodeQueryService)
        {
            _orderStatusRepository = orderStatusRepository;
            _orderRepository = orderRepository;
            _userService = userService;
            _travelerRepository = travelerRepository;
            _countriesRepo = countriesRepo;
            _statesRepo = statesRepo;
            _productVariantRepo = productVariantRepo;
            _discountCodeQueryService = discountCodeQueryService;

            _giftcardRepository = giftcardRepository;
            _giftorderRepository = giftorderRepository;
            _giftorderdetailRepository = giftorderdetailRepository;
            _giftcardsummaryRepository = giftcardsummaryRepository;
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

        public Order CreateOrder(int userId, List<ShoppingCartItem> shoppingCartItems)
        {
            var user = _userService.Find(userId);
            var order = new Order
              {
                  User = user,
                  OrderStatus = StatusPending(),
                  OrderNumber = GenerateOrderNumber()
              };

            foreach (var shoppingCartItem in shoppingCartItems)
                AddProductVariant(order, shoppingCartItem);

            order.Travelers = BuildTravelerList(order);
            OrderCalculator.Calculate(order);
            _orderRepository.Add(order);

            return order;
        }

        public Order FindOrder(int orderId, int userId)
        {
            return _orderRepository
              .FindBy(o => o.Id == orderId && o.UserId == userId, null, "Travelers,BillingAddress,User,ProductVariants")
              .Single();
        }

        public Traveler FindTraveler(int travelerId)
        {
            return _travelerRepository.Find(travelerId);
        }

        public void UpdateTravelers(ConfirmTravelers confirmTravelers)
        {
            foreach (var travelerDetails in confirmTravelers.Travelers)
            {
                var traveler = FindTraveler(travelerDetails.Id);
                traveler.InjectFrom(travelerDetails);
                _travelerRepository.Update(traveler);
            }
        }

        public Order UpdateBillingDetails(BillingDetails billingDetails)
        {
            var order = FindOrder(billingDetails.OrderId, billingDetails.UserId);
            var billingAddress = order.BillingAddress ?? new Address();
            billingAddress.InjectFrom(billingDetails);
            order.BillingAddress = billingAddress;
            order.SpecialInstructions = billingDetails.SpecialInstructions;

            if (billingDetails.GiftAmount != 0)
            {
                order.AdjustmentTotal = billingDetails.GiftAmount;
                order.Total = order.Total - billingDetails.GiftAmount;
            }
         
            _orderRepository.Update(order);

            var user = order.User;
            if (string.IsNullOrEmpty(user.FirstName))
                user.FirstName = billingDetails.FirstName;
            if (string.IsNullOrEmpty(user.LastName))
                user.LastName = billingDetails.LastName;
            _userService.UpdateUser(user);

            return order;
        }

        public Order CompleteOrder(Order order, string chargeId)
        {
            order.CompletedAt = DateTime.UtcNow;
            order.FailedAt = null;
            order.LastFailureMessage = null;
            order.ChargeId = chargeId;
            order.OrderStatus = StatusComplete();
            _orderRepository.Update(order);
            return order;
        }

        public Order FailOrder(Order order, string message)
        {
            order.LastFailureMessage = message;
            order.FailedAt = DateTime.UtcNow;
            order.OrderStatus = StatusFailed();
            _orderRepository.Update(order);
            return order;
        }

        public BillingOverview BuildBillingOverview(Order order)
        {
            var departure = order.ProductVariants.First().ProductVariant.Product.Departure;
            var rooms =
              order.ProductVariants.Select(
                v =>
                new BillingRoomSummary
                  {
                      Quantity = v.Quantity,
                      DisplayName = v.ProductVariant.DisplayName,
                      PluralDisplayName = v.ProductVariant.PluralDisplayName
                  }).ToList();

            return new BillingOverview
              {
                  TourName = departure.Tour.Name,
                  DepartureDate = departure.DepartureDate,
                  ReturnDate = departure.ReturnDate,
                  DepartureCode = departure.Code,
                  NumberOfRooms = rooms.Sum(summary => summary.Quantity),
                  Total = order.Total,
                  ItemTotal = order.ItemTotal,
                  AdjustmentTotal = order.AdjustmentTotal,
                  Rooms = rooms,
                  Countries = Countries(),
                  States = States()
              };
        }

        public Order LastOrderForUser(User user)
        {
            return _orderRepository.FindBy(o => o.UserId == user.Id && o.BillingAddressId != null)
                                   .OrderByDescending(o => o.CreatedAt)
                                   .FirstOrDefault();
        }

        public BillingDetails BuildBillingDetails(Order order, User user = null)
        {
            var billingOverview = BuildBillingOverview(order);

            var billingDetails = new BillingDetails
              {
                  OrderId = order.Id,
                  OrderNumber = order.OrderNumber,
                  DiscountCodes = DiscountCodesForOrder(order).Select(d => d.LowerCode).ToArray(),
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

        private static List<Traveler> BuildTravelerList(Order order)
        {
            var travelerCount = order.ProductVariants.Sum(v => NumberOfTravelers.Compute(v));
            var travelers = new List<Traveler>(travelerCount);
            for (var i = 0; i < travelerCount; i++)
                travelers.Add(new Traveler { EmailItinerary = true, ConfirmationNumber = GenerateOrderNumber() });
            return travelers;
        }

        private OrderStatus FindStatus(string name)
        {
            return _orderStatusRepository.FindBy(s => s.Name == name).Single();
        }

        private IEnumerable<Country> Countries()
        {
            return _countriesRepo.All.ToList();
        }

        public IEnumerable<State> States()
        {
            return _statesRepo.All.ToList();
        }

        public void ApplyDiscountCode(Order order, string code)
        {
            var discountCode = _discountCodeQueryService.FindByCode(code);
            var applier = new DiscountCodeApplier(discountCode);
            AddAdjustment(order, applier.DiscountAdjustment(order.Total));
            OrderCalculator.Calculate(order);
            _orderRepository.Update(order);
        }

        public bool IsProductVariantEnabled(int productVariantId)
        {
            var variant = _productVariantRepo.Find(productVariantId);
            return variant != null && variant.IsEnabled;
        }

        public IQueryable<DiscountCode> DiscountCodesForOrder(Order order)
        {
            var discountCodes = new List<DiscountCode>();
            var adjustments = order.Adjustments;
            if (adjustments != null && adjustments.Count > 0)
            {
                foreach (var adjustment in adjustments)
                {
                    var discountAdjustment = adjustment as DiscountAdjustment;
                    if (discountAdjustment != null)
                    {
                        if (discountAdjustment.DiscountCodeId.HasValue)
                            discountCodes.Add(_discountCodeQueryService.Find(discountAdjustment.DiscountCodeId.Value));
                    }
                }
            }
            return discountCodes.AsQueryable();
        }

        private void AddAdjustment(Order order, Adjustment adjustment)
        {
            if (order.Adjustments == null)
                order.Adjustments = new List<Adjustment>();
            order.Adjustments.Add(adjustment);
        }

        private static string GenerateOrderNumber()
        {
            var i = Guid.NewGuid().ToByteArray().Aggregate(1, (current, b) => current * (b + 1));
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        private void AddProductVariant(Order order, ShoppingCartItem shoppingCartItem)
        {
            if (order.ProductVariants == null)
                order.ProductVariants = new List<OrderProductVariant>();

            order.ProductVariants.Add(new OrderProductVariant
              {
                  ProductVariant = shoppingCartItem.ProductVariant,
                  Quantity = shoppingCartItem.Quantity,
                  UnitPrice = shoppingCartItem.ProductVariant.Price
              });
        }

        public bool ValidateGiftCode(string Code, string Email, ref decimal GiftAmount,ref int OrderDetailId)
        {
            decimal amount = 0;
            decimal usedamount = 0;
            

            var resultsOrderDetail = _giftorderdetailRepository.All
               .Join(_giftorderRepository.All, d => d.GiftOrderId, o => o.Id, (d, o) => new { d, o })
               .Where(t => t.d.RecipientEmail == Email)
               .Where(t => t.o.OrderStatusId == 3)
               .Where(t => t.d.RecipientGiftCode == Code)
               .Select(a => new
               {
                   Amount = a.d.Amount,
                   OrderDetailId = a.d.Id
               });

            var resultsGiftcardSummaries = _giftcardsummaryRepository.All
                .Join(_orderRepository.All, s => s.OrderId, o => o.Id, (s, o) => new { s, o })
                .Where(t => t.s.GiftOrderDetailId == resultsOrderDetail.FirstOrDefault().OrderDetailId)
                .Where(t => t.o.OrderStatusId == 3)
              .Select(a => new
              {
                  UsedAmount = a.s.UsedAmount
              });

            if (resultsOrderDetail.Count() > 0)
            {
                amount = Convert.ToDecimal(resultsOrderDetail.FirstOrDefault().Amount);
                OrderDetailId = resultsOrderDetail.FirstOrDefault().OrderDetailId;
            }

            if (resultsGiftcardSummaries.Count() > 0)
                usedamount = Convert.ToDecimal(resultsGiftcardSummaries.FirstOrDefault().UsedAmount);

            GiftAmount = amount - usedamount;

            if (amount - usedamount > 0)
                return true;
            else
                return false;

        }
        
        public void AddGiftCardSummaries(BillingDetails billingdetails)
        {
            var giftcardSummary = new GiftCardSummary
            {
                UsedAmount = billingdetails.GiftAmount,
                OrderId = billingdetails.OrderId,
                GiftOrderDetailId=billingdetails.GiftOrderDetailId
            };
            _giftcardsummaryRepository.Add(giftcardSummary);
        }
    }
}
