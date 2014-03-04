using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AuthorizeNet;
using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Experiences;
using Britespokes.Services.Orders;
using Britespokes.Services.ShoppingCart;
using Britespokes.Services.Tours;
using Britespokes.Services.Users;
using Britespokes.Web.App_Start;
using Britespokes.Web.Areas.Admin.Models.Experiences;
using Britespokes.Web.Infrastructure.Filters;
using Britespokes.Web.Infrastructure.Logging;
using Britespokes.Web.Mailers;
using Britespokes.Web.Models.Tours;
using Britespokes.Services.Categories;
using Britespokes.Web.Models.Home;
using createsend_dotnet;
using System.Configuration;
using Britespokes.Services.Perspectives;
using System.IO;
using Britespokes.Services.Admin.SEOToolStaticPages;
using Britespokes.Web.Models.College;



namespace Britespokes.Web.Controllers
{
    public class ToursController : BritespokesController
    {
        private readonly ITourService _tourService;
        private readonly IExperienceQueryService _experienceQueryService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderService _orderService;
        private readonly IRegistrationService _registrationService;
        private readonly IUserService _userService;
        private readonly IUserMailer _userMailer;
        private readonly IOrderConfirmationMailer _orderConfirmationMailer;
        private readonly ILogger _logger;
        private readonly IDiscountCodeValidator _discountCodeValidator;
        private readonly ICategoryService _categoryQueryService;
        private readonly IPerspectiveService _perspectiveService;
        private readonly ISEOToolStaticPageQueryService _seoToolStaticPageQueryService;
        private readonly IStudentInquiryMailer _mailer;
        public ToursController(ITourService tourService, IExperienceQueryService experienceQueryService, IShoppingCartService shoppingCartService, IOrderService orderService, IRegistrationService registrationService, IUserService userService, ILogger logger, IDiscountCodeValidator discountCodeValidator, IUserMailer userMailer, IOrderConfirmationMailer orderConfirmationMailer, ICategoryService categoryService, IPerspectiveService perspectiveService, ISEOToolStaticPageQueryService seoToolStaticPageQueryService, IStudentInquiryMailer mailer)
        {
            _tourService = tourService;
            _experienceQueryService = experienceQueryService;
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
            _registrationService = registrationService;
            _userService = userService;
            _userMailer = userMailer;
            _orderConfirmationMailer = orderConfirmationMailer;
            _logger = logger;
            _discountCodeValidator = discountCodeValidator;
            _categoryQueryService = categoryService;
            _perspectiveService = perspectiveService;
            _seoToolStaticPageQueryService = seoToolStaticPageQueryService;
            _mailer = mailer;
        }






        public ActionResult Index()
        {

            var experiences = _experienceQueryService.Experiences("");
            return View(new ExperiencesIndex
            {
                Count = experiences.Count(),
                Experiences = Mapper.Map<List<ExperiencesIndexItem>>(experiences)
            });
        }

        public ActionResult Experiences()
        {
            SEOToolStaticPage SEOTool = _seoToolStaticPageQueryService.FindSEOToolStaticPageByPermalink("experiences");

            if (SEOTool != null)
            {
                ViewBag.FocusKeyword = SEOTool.FocusKeyword;
                ViewBag.MetaDescription = SEOTool.MetaDescription;
                ViewBag.Title = SEOTool.SEOTitle;
            }

            var categories = _categoryQueryService.All();



            return View(new CategoryShow
            {
                Categories = categories.ToList()
            });
        }

        public ActionResult ExperiencesTours(string permalink)
        {
            Category _category = _categoryQueryService.All().Where(v => v.Permalink == permalink).First();
            Session["Category"] = _category;
            var experiences = _experienceQueryService.Experiences(permalink);
            var subcategory = _category.SubCategories;

            SEOTool SEOTool = new SEOTool();

            if (_category.SEOTools != null && _category.SEOTools.Count > 0)
                SEOTool = _category.SEOTools.First();


            ViewBag.FocusKeyword = SEOTool.FocusKeyword;
            ViewBag.MetaDescription = SEOTool.MetaDescription;
            ViewBag.Title = SEOTool.SEOTitle;

            TempData["Info"] = _category;

            return View(new ExperiencesIndex
            {
                Count = experiences.Count(),
                CategoryName = _category.Name,
                CategoryPermalink = _category.Permalink,
                Experiences = Mapper.Map<List<ExperiencesIndexItem>>(experiences),
                subcategory = subcategory
            });
        }

        public ActionResult SubCategoryTours(string permalink, string subpermalink)
        {
            Category _category = _categoryQueryService.All().Where(v => v.Permalink == permalink).First();
            Session["Category"] = _category;
            var experiences = _experienceQueryService.Experiences(permalink);

            var subcategory = _category.SubCategories.Where(p => p.Permalink == subpermalink).ToList();
            var tours = subcategory.FirstOrDefault().Tours.ToList();

            SEOTool SEOTool = new SEOTool();

            if (_category.SEOTools != null && _category.SEOTools.Count > 0)
                SEOTool = _category.SEOTools.First();


            ViewBag.FocusKeyword = SEOTool.FocusKeyword;
            ViewBag.MetaDescription = SEOTool.MetaDescription;
            ViewBag.Title = SEOTool.SEOTitle;

            TempData["Info"] = _category;

            return View(new ExperiencesIndex
            {
                Count = experiences.Count(),
                CategoryName = _category.Name,
                CategoryPermalink = _category.Permalink,
                Experiences = Mapper.Map<List<ExperiencesIndexItem>>(experiences),
                subcategory = subcategory,
                Tours = tours
            });
        }

        public ActionResult SubscribeTour(TourShow tourShow)
        {
            try
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
                    tourShow.IsTripBooked = false;
                    return PartialView("_Coming Soon", tourShow);
                }

                tourShow.IsTripBooked = true;
                if (tourShow.MailingListID != null)
                {
                    AuthenticationDetails auth = new ApiKeyAuthenticationDetails(ConfigurationManager.AppSettings["CampaignMonitorAPI_key"]);
                    Subscriber objSubscriber = new Subscriber(auth, tourShow.MailingListID);
                    string newSubscriberID = objSubscriber.Add(tourShow.EmailToSubscribe.ToString(), null, null, false);
                }

                return PartialView("_Coming Soon", tourShow);

            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return PartialView("_Coming Soon");
            }

        }

        public ActionResult Show(string permalink)
        {
            try
            {
                var perspectiveposts = _perspectiveService.PostByTour(permalink);
                var tour = _tourService.FindByPermalink(permalink);

                if (tour == null || !tour.IsPublished)
                    return HttpNotFound();
                int OrganizationId = UserContext.Organization.Id;
                var tourShow = PopulateTourShow(tour, OrganizationId);

                SEOTool SEOTool = new SEOTool();

                if (tour.SEOTools != null && tour.SEOTools.Count > 0)
                    SEOTool = tour.SEOTools.First();


                ViewBag.FocusKeyword = SEOTool.FocusKeyword;
                ViewBag.MetaDescription = SEOTool.MetaDescription;
                ViewBag.Title = SEOTool.SEOTitle;


                tourShow.PerspectivePosts = perspectiveposts.OrderByDescending(m => m.UpdatedAt).Take(2).ToList();

                return TourView(tourShow);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return HttpNotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Preview(string permalink)
        {
            var tour = _tourService.FindByPermalink(permalink);
            if (tour == null)
                return HttpNotFound();
            int OrganizationId = UserContext.Organization.Id;
            var tourShow = PopulateTourShow(tour, OrganizationId);
            return TourView(tourShow);
        }

        [GuestRequired]
        [HttpPost]
        public ActionResult ConfirmRooms(string code, string package)
        {


            if (TempData["code"] != null)
            {
                code = TempData["code"].ToString();
                package = TempData["package"].ToString();
            }
            if (string.IsNullOrEmpty(code))
                return HttpNotFound();

            var departure = _tourService.FindDeparture(code);
            if (departure == null)
                return HttpNotFound();

            var bookingRequest = new BookingRequest
            {
                TourName = departure.Tour.Name,
                DepartureDate = departure.DepartureDate,
                ProductId = departure.Product.Id,
                ProductRequests = new List<ProductRequest>()
            };
            foreach (var productVariant in departure.Product.ProductVariants)
            {
                if (!productVariant.IsEnabled) continue;
                bookingRequest.ProductRequests.Add(new ProductRequest
                {
                    VariantId = productVariant.Id,
                    Price = package == "1" ? productVariant.Price : productVariant.PriceForBriter,
                    DisplayName = productVariant.DisplayName,
                    Description = productVariant.Description,
                    Quantity = 0,
                    PeoplePerRoom = CalculatePeoplePerRoom(productVariant)
                });
            }

            return View("ConfirmRooms", bookingRequest);
        }



        [Authorize]
        [HttpPost]
        public ActionResult StartOrder(BookingRequest bookingRequest)
        {
            if (bookingRequest.ProductRequests.Sum(r => r.Quantity) <= 0)
                ModelState.AddModelError("", "You must pick at least one room for this tour.");

            if (!string.IsNullOrWhiteSpace(bookingRequest.DiscountCode))
            {
                var validation = _discountCodeValidator.Validate(bookingRequest.DiscountCode,
                  bookingRequest.ProductRequests.Where(pr => pr.Quantity > 0).Select(pr => pr.VariantId).ToArray());
                if (!validation.IsValid)
                    ModelState.AddModelError("DiscountCode", "Sorry, this discount code is not applicable.");
            }

            if (!ModelState.IsValid) return View("ConfirmRooms", bookingRequest);

            // right now, the shopping cart doesn't really do anything
            // since we only support booking a single tour at a time
            // so, we will immediately move into the order processing phase
            bookingRequest.UserId = UserContext.UserId;

            try
            {
                _shoppingCartService.ProcessRequest(bookingRequest);
                var order = _shoppingCartService.CreatePendingOrder(UserContext.UserId);
                if (!string.IsNullOrEmpty(bookingRequest.DiscountCode))
                    _orderService.ApplyDiscountCode(order, bookingRequest.DiscountCode);
                return Redirect(Url.RouteUrl("confirm-travelers", new { orderId = order.Id }, SecureProtocol));
            }
            catch (ProductVariantNotEnabledException)
            {
                ModelState.AddModelError("", "One or more products selected are not available. Contact us for help.");
                return View("ConfirmRooms", bookingRequest);
            }
        }

        [Authorize]
        //[ProductionRequireHttps]
        public ActionResult ConfirmTravelers(int orderId)
        {
            var order = _orderService.FindOrder(orderId, UserContext.UserId);
            // TODO: make this a filter?
            if (order.OrderStatus != _orderService.StatusPending())
                return Redirect(Url.RouteUrl("confirmation", new { orderId = order.Id }, SecureProtocol)); // TODO: handle canceled orders
            var confirmTravelers = Mapper.Map<ConfirmTravelers>(order);
            // ugly way to set an index on the collection for display in the view
            for (var i = 0; i < confirmTravelers.Travelers.Count; i++)
                confirmTravelers.Travelers[i].Index = i + 1;

            //#if DEBUG
            //      var traveler1 = confirmTravelers.Travelers[0];
            //      traveler1.FirstName = "Joe";
            //      traveler1.LastName = "Case";
            //      traveler1.Email = "jcase4@hotmail.com";
            //      traveler1.PhoneNumber = "555-555-5555";
            //      traveler1.Birthday = DateTime.Parse("1977-05-11");
            //#endif

            return View(confirmTravelers);
        }

        [Authorize]
        [HttpPost]
        //[ProductionRequireHttps]
        public ActionResult ConfirmTravelers(ConfirmTravelers confirmTravelers)
        {
            if (ModelState.IsValid)
            {
                var order = _orderService.FindOrder(confirmTravelers.OrderId, UserContext.UserId);
                // TODO: make this a filter?
                if (order.OrderStatus != _orderService.StatusPending())
                    return Redirect(Url.RouteUrl("confirmation", new { orderId = order.Id }, SecureProtocol)); // TODO: handle canceled orders
                _orderService.UpdateTravelers(confirmTravelers);
                return Redirect(Url.RouteUrl("billing-details", new { orderId = order.Id }));
            }
            return View(confirmTravelers);
        }

        [Authorize]
        //[ProductionRequireHttps]
        public ActionResult Billing(int orderId)
        {
            var order = _orderService.FindOrder(orderId, UserContext.UserId);
            // TODO: make this a filter?
            if (order.OrderStatus != _orderService.StatusPending())
                return Redirect(Url.RouteUrl("confirmation", new { orderId = order.Id }, SecureProtocol)); // TODO: handle canceled orders

            var billingDetails = _orderService.BuildBillingDetails(order, (UserContext.IsGuest ? null : order.User));
            billingDetails.PaymentRequest = new PaymentRequest(order.Total, AuthorizeNetConfig.ApiLogin, AuthorizeNetConfig.TransactionKey, AuthorizeNetConfig.TestMode);

            if (order.FailedAt.HasValue)
                TempData["Error"] = order.LastFailureMessage;

            return View(billingDetails);
        }

        [Authorize]
        [HttpPost]
        //[ProductionRequireHttps]
        public ActionResult Billing(Britespokes.Services.Orders.BillingDetails billingDetails)
        {
            //decimal GiftAmount = 0;
            //int OrderDetailId = 0;

            var order = _orderService.FindOrder(billingDetails.OrderId, UserContext.UserId);
            if (!billingDetails.AcceptedTermsAndConditions)
                ModelState.AddModelError("AcceptedTermsAndConditions", "you must accept the terms and conditions to purchase a tour");
            if (order.OrderStatus != _orderService.StatusPending())
                ModelState.AddModelError("", "This order is no longer pending");
            if (UserContext.IsGuest && string.IsNullOrEmpty(billingDetails.Password))
                ModelState.AddModelError("Password", "required");

            if (string.CompareOrdinal(billingDetails.Password, billingDetails.ConfirmPassword) != 0)
                ModelState.AddModelError("ConfirmPassword", "doesn't match");

            //Gift Code Validation and also it returns the amount of gift
            if (!string.IsNullOrWhiteSpace(billingDetails.GiftCode) && !_orderService.ValidateGiftCode(billingDetails.GiftCode, billingDetails.Email, ref billingDetails.GiftAmount, ref billingDetails.GiftOrderDetailId))
                ModelState.AddModelError("GiftCode", "gift code either used or doesn't exists");

            if (ModelState.IsValid)
            {
                if (UserContext.IsGuest)
                {
                    var user = _userService.Find(UserContext.UserId);
                    _registrationService.PromoteGuest(user, billingDetails.Email, billingDetails.Password);
                    _userMailer.SendWelcomeEmail(UserContext.Organization, user);
                }

                //Saving detail of gift card used
                if (billingDetails.GiftOrderDetailId != 0)
                {
                    _orderService.AddGiftCardSummaries(billingDetails);
                }
                _orderService.UpdateBillingDetails(billingDetails);

                //For gift code
                billingDetails.PaymentRequest = new PaymentRequest(order.Total, AuthorizeNetConfig.ApiLogin, AuthorizeNetConfig.TransactionKey, AuthorizeNetConfig.TestMode);

                //To be remove only for testing
                // _orderConfirmationMailer.SendOrderConfirmationEmail(order);


                return Json(new { BillingDetails = billingDetails, Errors = new object[0] });
            }

            billingDetails.BillingOverview = _orderService.BuildBillingOverview(order);
            billingDetails.PaymentRequest = new PaymentRequest(order.Total, AuthorizeNetConfig.ApiLogin, AuthorizeNetConfig.TransactionKey, AuthorizeNetConfig.TestMode);
            return Json(new { BillingDetails = billingDetails, Errors = ModelStateErrorsForJson() });
        }

        [HttpPost]
        public ActionResult AuthorizeNetSim(FormCollection post)
        {
            _logger.Info("keys: {0}", string.Join(", ", post.AllKeys));
            var orderId = Convert.ToInt32(post["OrderId"]);
            var userId = Convert.ToInt32(post["UserId"]);
            var order = _orderService.FindOrder(orderId, userId);

            _logger.Info("Authorizing SIM...");

            //the URL to redirect to- this MUST be absolute
            var successUrl = Url.RouteUrl("confirmation", new { orderId = order.Id }, SecureProtocol);
            var failureUrl = Url.RouteUrl("billing-details", new { orderId = order.Id }, SecureProtocol);
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
                _orderConfirmationMailer.SendOrderConfirmationEmail(order);
                _orderService.CompleteOrder(order, response.TransactionID);
            }
            else
            {
                _orderService.FailOrder(order, response.Message);
                redirectUrl = failureUrl;
            }

            return Content(AuthorizeNet.Helpers.CheckoutFormBuilders.Redirecter(redirectUrl));
        }

        [Authorize]
        //[ProductionRequireHttps]
        public ActionResult Confirmation(int orderId)
        {
            var order = _orderService.FindOrder(orderId, UserContext.UserId);
            return View(order);
        }

        public ActionResult StudentInquiryForm(string permalink)
        {
            var studentenquiryform = new StudentInquiryForm() { TourPermalink = permalink };

            return View(studentenquiryform);
        }
        [HttpPost]
        public ActionResult Create(StudentInquiryForm form, string permalink)
        {
            if (ModelState.IsValid)
            {
                var tour = _tourService.FindByPermalink(permalink);
                //_mailer.SendStudentInquiry(tour, form);
                TempData["Info"] = "We've submitted your inquiry and will be in touch shortly.";
                return RedirectToRoute("tour-show", new { permalink });
            }
            else
            {
                return View("StudentInquiryForm", form);
            }
        }
        // TODO: move somewhere else
        private static int CalculatePeoplePerRoom(ProductVariant productVariant)
        {
            var ppr = 1;
            switch (productVariant.DisplayName)
            {
                case "Single":
                    ppr = 1;
                    break;
                case "Double":
                    ppr = 2;
                    break;
                case "Triple":
                    ppr = 3;
                    break;
                case "Quad":
                    ppr = 4;
                    break;
            }
            return ppr;
        }

        private static TourShow PopulateTourShow(Tour tour, int OrganizationId)
        {

            var tourShow = Mapper.Map<TourShow>(tour);
            var products = tour.Departures.
              Where(d => d.AvailabilityStatusId == AvailabilityStatus.Available).
              Select(d => d.Product).ToList();
            if (products.Any())
            {
                var allVariants = products.SelectMany(p => p.ProductVariants).ToList();
                if (allVariants.Any())
                {
                    tourShow.BestSinglePrice = BestPriceForVariant(allVariants, "Single", "brite");
                    tourShow.BestDoublePrice = BestPriceForVariant(allVariants, "Double", "brite");
                    tourShow.BestTriplePrice = BestPriceForVariant(allVariants, "Triple", "brite");
                    tourShow.BestQuadPrice = BestPriceForVariant(allVariants, "Quad", "brite");

                    tourShow.BriterBestSinglePrice = BestPriceForVariant(allVariants, "Single", "briter");
                    tourShow.BriterBestDoublePrice = BestPriceForVariant(allVariants, "Double", "briter");
                    tourShow.BriterBestTriplePrice = BestPriceForVariant(allVariants, "Triple", "briter");
                    tourShow.BriterBestQuadPrice = BestPriceForVariant(allVariants, "Quad", "briter");
                }
            }

            //departures filtered by organizations
            var departure = tourShow.Departures.Where(pr => pr.Organizations.Any(us => us.Id == OrganizationId));
            tourShow.Departures = departure.ToList();
           
            return tourShow;
        }

        private static decimal? BestPriceForVariant(IEnumerable<ProductVariant> allVariants, string variantName, string packageType)
        {
            var variants = allVariants.Where(v => v.Name == variantName && v.IsEnabled).ToArray();
            if (variants.Any())
            {
                if (packageType == "brite")
                    return variants.Min(v => v.Price);
                else
                    return variants.Min(v => v.PriceForBriter);
            }
            return null;
        }

        private ViewResult TourView(TourShow tourShow)
        {
            if (ViewExists(tourShow.Permalink))
                return View(tourShow.Permalink, tourShow);

            var typeName = tourShow.TourType.Name;//ViewExists(typeName) ? typeName : "Show", tourShow)
            return View("Show", tourShow);
        }


        //private ActionResult downloadPDF(string str)
        //{

        //    string path = Path.GetFullPath("");
        //    string filename = Path.GetFileName("");
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
        //    string contentType = "application/pdf";
        //    return File(path, contentType, filename);
        //}

    }
}