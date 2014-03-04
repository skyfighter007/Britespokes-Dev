using System.IO;
using System.Text;
using System.Web.Mvc;
using AuthorizeNet;
using Britespokes.Core.Domain;
using Britespokes.Services.Orders;
using Britespokes.Services.Users;
using Britespokes.Web.App_Start;
using Britespokes.Web.Infrastructure.Filters;

namespace Britespokes.Web.Controllers
{
  public class TestCheckoutController : BritespokesController
  {
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;

    public TestCheckoutController(IOrderService orderService, IUserService userService)
    {
      _orderService = orderService;
      _userService = userService;
    }

    [Authorize]
      //[ProductionRequireHttps]
    public ActionResult TestCheckout()
    {
      var billingDetails = new BillingDetails
        {
          UserId = UserContext.UserId,
          CountryId = 226,
          BillingOverview = new BillingOverview
            {
              States = _orderService.States()
            },
          PaymentRequest =
            new PaymentRequest(5.0M, AuthorizeNetConfig.ApiLogin, AuthorizeNetConfig.TransactionKey,
                               AuthorizeNetConfig.TestMode)
        };

      SetBillingAddressFromLastOrder(billingDetails, _userService.Find(UserContext.UserId));
      return View(billingDetails);
    }

    [Authorize]
    [HttpPost]
      //[ProductionRequireHttps]
    // ReSharper disable InconsistentNaming
    public ActionResult TestCheckout(BillingDetails billingDetails, decimal? x_amount)
    // ReSharper restore InconsistentNaming
    {
      if (!billingDetails.AcceptedTermsAndConditions)
        ModelState.AddModelError("AcceptedTermsAndConditions", "you must accept the terms and conditions to purchase a tour");

      if (ModelState.IsValid)
      {
        return Json(new { BillingDetails = billingDetails, Errors = new object[0] });
      }

      billingDetails.BillingOverview = new BillingOverview { States = _orderService.States() };
      billingDetails.PaymentRequest = new PaymentRequest((x_amount.HasValue ? x_amount.Value : 5.0M), AuthorizeNetConfig.ApiLogin, AuthorizeNetConfig.TransactionKey, AuthorizeNetConfig.TestMode);
      return Json(new { BillingDetails = billingDetails, Errors = ModelStateErrorsForJson() });
    }

    [HttpPost]
    public ActionResult TestCheckoutSim(FormCollection post)
    {
      var response = new SIMResponse(post);

      var output = new StringBuilder();

      //first order of business - validate that it was Authorize.Net that posted this using the
      //MD5 hash that was passed back to us
      var isValid = response.Validate(AuthorizeNetConfig.Md5HashValue, AuthorizeNetConfig.ApiLogin);

      if (isValid && response.Approved)
      {
        output.AppendFormat("Completed Test Checkout<br/>");
      }
      else
      {
        output.AppendFormat("Failed Test Checkout<br/>");
      }

      output.AppendFormat("<br/>Valid: {0}", isValid);
      output.AppendFormat("<br/>Approved: {0}", response.Approved);
      output.AppendFormat("<br/>Code: {0}", response.ResponseCode);
      output.AppendFormat("<br/>Message: {0}", response.Message);
      output.AppendFormat("<br/>Authorization Code: {0}", response.AuthorizationCode);
      output.AppendFormat("<br/>Card Number: {0}", response.CardNumber);
      output.AppendFormat("<br/>Card Type: {0}", response.CardType);
      output.AppendFormat("<br/>Invoice Number: {0}", response.InvoiceNumber);
      output.AppendFormat("<br/>MD5 Hash: {0}", response.MD5Hash);
      output.AppendFormat("<br/>Transaction ID: {0}", response.TransactionID);

      var appData = HttpContext.Server.MapPath("~/App_Data/");
      var outputFile = string.Format("{0}.txt", response.InvoiceNumber);
      System.IO.File.WriteAllText(Path.Combine(appData, outputFile), output.ToString());

      //the URL to redirect to- this MUST be absolute
      var redirectUrl = Url.RouteUrl("test-checkout-confirmation", new { invoiceNumber = response.InvoiceNumber }, SecureProtocol);

      return Content(AuthorizeNet.Helpers.CheckoutFormBuilders.Redirecter(redirectUrl));
    }

    [Authorize]
      //[ProductionRequireHttps]
    public ActionResult Confirmation(string invoiceNumber)
    {
      var appData = HttpContext.Server.MapPath("~/App_Data/");
      var file = string.Format("{0}.txt", invoiceNumber);
      var output = System.IO.File.ReadAllText(Path.Combine(appData, file));

      if (!string.IsNullOrEmpty(output))
      {
        if (output.StartsWith("Completed"))
          TempData["Info"] = output;
        else
          TempData["Error"] = output;
      }
      else
        TempData["Error"] = "Could not find response file.";

      return View();
    }

    private void SetBillingAddressFromLastOrder(BillingDetails billingDetails, User user)
    {
      billingDetails.FirstName = user.FirstName;
      billingDetails.LastName = user.LastName;
      billingDetails.Email = user.Email;

      var lastOrder = _orderService.LastOrderForUser(user);
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
  }
}
