using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Experiences;
using Britespokes.Services.GiftCards;
using Britespokes.Services.Orders;
using Britespokes.Services.ShoppingCart;
using Britespokes.Services.Tours;
using Britespokes.Services.Users;
using Britespokes.Web.App_Start;
using Britespokes.Web.Areas.Admin.Models.Experiences;
using Britespokes.Web.Areas.Admin.Models.GiftCards;
using Britespokes.Web.Infrastructure.Filters;
using Britespokes.Web.Infrastructure.Logging;
using Britespokes.Web.Mailers;
using Britespokes.Web.Models.GiftCards;
using Britespokes.Web.Models.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorizeNet;
using createsend_dotnet;
using System.Configuration;
using Britespokes.Services.Admin.SEOToolStaticPages;
using System.Text.RegularExpressions;
//using createsend_dotnet;

namespace Britespokes.Web.Controllers
{
    public class GiftCardsController : BritespokesController
    {
        private readonly IGiftCardService _giftCardService;
        private readonly IGiftCardOrderService _giftCardOrderService;
        private readonly IRegistrationService _registrationService;
        private readonly IUserService _userService;
        private readonly IUserMailer _userMailer;
        private readonly IGiftOrderConfirmationMailer _giftorderConfirmationMailer;
        private readonly ILogger _logger;
        private readonly ISEOToolStaticPageQueryService _seoToolStaticPageQueryService;

        public GiftCardsController(IGiftCardService giftCardService, IGiftCardOrderService giftCardOrderService, IUserService userService, ILogger logger, IRegistrationService registrationService, IUserMailer userMailer, IGiftOrderConfirmationMailer giftorderConfirmationMailer, ISEOToolStaticPageQueryService seoToolStaticPageQueryService)
        {
            _giftCardService = giftCardService;
            _giftCardOrderService = giftCardOrderService;

            _registrationService = registrationService;
            _userService = userService;
            _userMailer = userMailer;
            _giftorderConfirmationMailer = giftorderConfirmationMailer;
            _logger = logger;
            _seoToolStaticPageQueryService = seoToolStaticPageQueryService;
        }

        public ActionResult Index()
        {
            SEOToolStaticPage SEOTool = _seoToolStaticPageQueryService.FindSEOToolStaticPageByPermalink("b-giftcards");

            if (SEOTool != null)
            {
                ViewBag.FocusKeyword = SEOTool.FocusKeyword;
                ViewBag.MetaDescription = SEOTool.MetaDescription;
                ViewBag.Title = SEOTool.SEOTitle;
            }


            var giftCards = _giftCardService.All();
            return View(new GiftCardsIndex
            {
                Count = giftCards.Count(),
                GiftCards = Mapper.Map<List<GiftCard>>(giftCards)
            });
        }
        [GuestRequired]
        public ActionResult Showwithcolor(string permalink, string color)
        {
            TempData["color"] = color;
            return RedirectToRoute("giftcard-show",new{permalink=permalink});
        }

        [GuestRequired]
        public ActionResult Show(string permalink)
        {
            var giftcards=new GiftCardsIndex();
            try
            {
               
            
                var giftcard = _giftCardService.FindByPermalink(permalink);
                if (giftcard == null || !giftcard.IsPublished)
                    return HttpNotFound();

                var giftCardShow = Mapper.Map<GiftCardShow>(giftcard);

                giftCardShow.Color = TempData["color"].ToString();

                SEOTool SEOTool = new SEOTool();

                if (giftcard.SEOTools != null && giftcard.SEOTools.Count > 0)
                    SEOTool = giftcard.SEOTools.First();


                ViewBag.FocusKeyword = SEOTool.FocusKeyword;
                ViewBag.MetaDescription = SEOTool.MetaDescription;
                ViewBag.Title = SEOTool.SEOTitle;
                
                return View("show",giftCardShow);

            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return HttpNotFound();
            }
        }



        public ActionResult ConfirmGiftOrders(GiftOrder giftOrder)
        {
            try
            {
                if (Request.IsAjaxRequest())
                {
                    //creating pending order for gift cards
                    giftOrder.UserId = UserContext.UserId;
                    var order = _giftCardOrderService.CreateOrder(giftOrder);

                    var confirmGiftCards = Mapper.Map<ConfirmGiftCards>(order);
                    for (var i = 0; i < confirmGiftCards.GiftOrderDetail.Count; i++)
                        confirmGiftCards.GiftOrderDetail[i].Index = i + 1;

                    var GiftOrderDetails = confirmGiftCards.GiftOrderDetail;

                    return PartialView("_GiftOrderDetails", confirmGiftCards);
                }
                return PartialView("_GiftOrderDetails");
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return PartialView("_GiftOrderDetails");
            }

        }

        public ActionResult StartGiftOrders(ConfirmGiftCards giftcards)
        {
            try
            {
                if (Request.IsAjaxRequest())
                {


                    if (!ModelState.IsValid)
                    {
                        foreach (var value in ModelState.Values.ToList())
                        {
                            foreach (var error in value.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.ErrorMessage);
                            }
                        }

                        return PartialView("_GiftOrderDetails", giftcards);
                    }

                    else
                    {
                        _giftCardOrderService.UpdateGiftOrderDetails(giftcards);
                        return Redirect(Url.RouteUrl("billing-orderdetails", new { orderId = giftcards.GiftOrderId }));
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You must add at least one quantity for this gift order.");
                    return PartialView("_GiftOrderDetails", giftcards);
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return PartialView("_GiftOrderDetails");
            }
        }


        [Authorize]
        //[ProductionRequireHttps]
        public ActionResult Billing(int orderId)
        {
            var order = _giftCardOrderService.FindOrder(orderId, UserContext.UserId);
            // TODO: make this a filter?
            if (order.OrderStatus != _giftCardOrderService.StatusPending())
                return Redirect(Url.RouteUrl("giftconfirmation", new { orderId = order.Id }, SecureProtocol)); // TODO: handle canceled orders

            var billingDetails = _giftCardOrderService.BuildBillingDetails(order, (UserContext.IsGuest ? null : order.User));
            billingDetails.PaymentRequest = new PaymentRequest(order.Total, AuthorizeNetConfig.ApiLogin, AuthorizeNetConfig.TransactionKey, AuthorizeNetConfig.TestMode);

            if (order.FailedAt.HasValue)
                TempData["Error"] = order.LastFailureMessage;

            return View(billingDetails);
        }



        [Authorize]
        [HttpPost]
        //[ProductionRequireHttps]
        public ActionResult Billing(Britespokes.Services.GiftCards.BillingDetails billingDetails)
        {
            //decimal GiftAmount = 0;
            //int OrderDetailId = 0;

            var order = _giftCardOrderService.FindOrder(billingDetails.GiftOrderId, UserContext.UserId);
            if (!billingDetails.AcceptedTermsAndConditions)
                ModelState.AddModelError("AcceptedTermsAndConditions", "you must accept the terms and conditions to purchase a tour");
            if (order.OrderStatus != _giftCardOrderService.StatusPending())
                ModelState.AddModelError("", "This order is no longer pending");
            if (UserContext.IsGuest && string.IsNullOrEmpty(billingDetails.Password))
                ModelState.AddModelError("Password", "required");

            if (string.CompareOrdinal(billingDetails.Password, billingDetails.ConfirmPassword) != 0)
                ModelState.AddModelError("ConfirmPassword", "doesn't match");


            if (ModelState.IsValid)
            {
                if (UserContext.IsGuest)
                {
                    var user = _userService.Find(UserContext.UserId);
                    _registrationService.PromoteGuest(user, billingDetails.Email, billingDetails.Password);
                    _userMailer.SendWelcomeEmail(UserContext.Organization, user);
                }
                _giftCardOrderService.UpdateBillingDetails(billingDetails);

                //Campaign Monitor -Adding subscriber to Gift card recipients List

                AuthenticationDetails auth = new ApiKeyAuthenticationDetails(ConfigurationManager.AppSettings["CampaignMonitorAPI_key"]);
                Subscriber objSubscriber = new Subscriber(auth, ConfigurationManager.AppSettings["CampaignMonitorListID"]);

                for (int i = 0; i < order.GiftOrderDetail.Count; i++)
                {
                    List<SubscriberCustomField> customFields = new List<SubscriberCustomField>();
                    customFields.Add(new SubscriberCustomField() { Key = "Amount", Value = order.GiftOrderDetail[i].Amount ==null? "":order.GiftOrderDetail[i].Amount.ToString() });
                    customFields.Add(new SubscriberCustomField() { Key = "Your Name", Value = order.GiftOrderDetail[i].YourName == null ? "" : order.GiftOrderDetail[i].YourName.ToString() });
                    customFields.Add(new SubscriberCustomField() { Key = "Gift Code", Value = order.GiftOrderDetail[i].RecipientGiftCode == null ? "" : order.GiftOrderDetail[i].RecipientGiftCode.ToString() });
                    customFields.Add(new SubscriberCustomField() { Key = "Message", Value = order.GiftOrderDetail[i].Message == null ? "" : order.GiftOrderDetail[i].Message.ToString() });

                    string newSubscriberID = objSubscriber.Add(order.GiftOrderDetail[i].RecipientEmail.ToString(), null, customFields, false);
                }
                
                return Json(new { BillingDetails = billingDetails, Errors = new object[0] });
            }

            billingDetails.BillingOverview = _giftCardOrderService.BuildBillingOverview(order);
            billingDetails.PaymentRequest = new PaymentRequest(order.Total, AuthorizeNetConfig.ApiLogin, AuthorizeNetConfig.TransactionKey, AuthorizeNetConfig.TestMode);
            return Json(new { BillingDetails = billingDetails, Errors = ModelStateErrorsForJson() });
        }

        [HttpPost]
        public ActionResult AuthorizeNetSimGift(FormCollection post)
        {
            _logger.Info("keys: {0}", string.Join(", ", post.AllKeys));
            var orderId = Convert.ToInt32(post["GiftOrderId"]);
            var userId = Convert.ToInt32(post["UserId"]);
            var order = _giftCardOrderService.FindOrder(orderId, userId);

            _logger.Info("Authorizing SIM...");

            //the URL to redirect to- this MUST be absolute
            var successUrl = Url.RouteUrl("giftconfirmation", new { orderId = order.Id }, SecureProtocol);
            var failureUrl = Url.RouteUrl("billing-giftdetails", new { orderId = order.Id }, SecureProtocol);
            var redirectUrl = successUrl;

            var response = new SIMResponse(post);

            _logger.Info("Approved: {0}", response.Approved);
            _logger.Info("Code: {0}", response.ResponseCode);
            _logger.Info("Message: {0}", response.Message);
            _logger.Info("Authorization Code: {0}", response.AuthorizationCode);
            _logger.Info("Card Number: {0}", response.CardNumber);
            _logger.Info("Card Type: {0}", response.CardType);
            _logger.Info("Invoice Number: {0}", response.InvoiceNumber);
            _logger.Info("MD5 Hash: {0}", response.MD5Hash);
            _logger.Info("Transaction ID: {0}", response.TransactionID);

            //first order of business - validate that it was Authorize.Net that posted this using the
            //MD5 hash that was passed back to us
            var isValid = response.Validate(AuthorizeNetConfig.Md5HashValue, AuthorizeNetConfig.ApiLogin);

            _logger.Info("Valid: {0}", isValid);
            if (isValid && response.Approved)
            {
                _giftorderConfirmationMailer.SendGiftOrderConfirmationEmail(order);
                _giftCardOrderService.CompleteOrder(order, response.TransactionID);


                //Campaign Monitor -Adding subscriber to Gift card recipients List

                AuthenticationDetails auth = new ApiKeyAuthenticationDetails(ConfigurationManager.AppSettings["CampaignMonitorAPI_key"]);
                Subscriber objSubscriber = new Subscriber(auth, ConfigurationManager.AppSettings["CampaignMonitorListID"]);

                for (int i = 0; i < order.GiftOrderDetail.Count; i++)
                {
                    List<SubscriberCustomField> customFields = new List<SubscriberCustomField>();
                    customFields.Add(new SubscriberCustomField() { Key = "Amount", Value = order.GiftOrderDetail[i].Amount.ToString() });
                    customFields.Add(new SubscriberCustomField() { Key = "Your Name", Value = order.GiftOrderDetail[i].YourName.ToString() });
                    customFields.Add(new SubscriberCustomField() { Key = "Gift Code", Value = order.GiftOrderDetail[i].RecipientGiftCode.ToString() });
                    customFields.Add(new SubscriberCustomField() { Key = "Message", Value = order.GiftOrderDetail[i].Message.ToString() });

                    string newSubscriberID = objSubscriber.Add(order.GiftOrderDetail[i].RecipientEmail.ToString(), null, customFields, false);
                }

            }
            else
            {
                _giftCardOrderService.FailOrder(order, response.Message);
                redirectUrl = failureUrl;
            }

            return Content(AuthorizeNet.Helpers.CheckoutFormBuilders.Redirecter(redirectUrl));
        }

        [Authorize]
        //[ProductionRequireHttps]
        public ActionResult Confirmation(int orderId)
        {
            var order = _giftCardOrderService.FindOrder(orderId, UserContext.UserId);
            return View(order);
        }
    }
}
