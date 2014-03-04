using System.Web.Mvc;
using Britespokes.Core.Domain;
using Britespokes.Services.Users;
using Britespokes.Web.Infrastructure.Filters;
using Britespokes.Web.Mailers;
using Britespokes.Web.Models.Users;
using Facebook;
using System;

namespace Britespokes.Web.Controllers
{
  public class RegistrationsController : BritespokesController
  {
    private readonly IRegistrationService _registrationService;
    private readonly IUserMailer _userMailer;

    public RegistrationsController(IRegistrationService registrationService, IUserMailer userMailer)
    {
      _registrationService = registrationService;
      _userMailer = userMailer;
    }

    [AllowAnonymous]
      //[ProductionRequireHttps]
    public ActionResult Create(string returnUrl = null)
    {
      var viewModel = new RegisterInput();
      return View(viewModel);
    }

    [HttpPost]
    [AllowAnonymous]
      //[ProductionRequireHttps]
    public ActionResult Create(RegisterInput viewModel, string returnUrl = null)
    {
      Organization org = UserContext.Organization;

      if (org.IsPasscodeRequired && string.IsNullOrEmpty(viewModel.Passcode))
        ModelState.AddModelError("Passcode", "The Company passcode field is required.");
      if (org.IsPasscodeRequired && viewModel.Passcode != org.Passcode)
        ModelState.AddModelError("Passcode", "Invalid Passcode.");
      if (!_registrationService.IsAllowedEmailDomain(org, viewModel.Email))
        ModelState.AddModelError("Email", "Please register using an approved company email address.");
      if (viewModel.Password != viewModel.ConfirmPassword)
        ModelState.AddModelError("ConfirmPassword", "confirmation does not match");

      if (ModelState.IsValid)
      {
        if (_registrationService.IsRegistered(org, viewModel.Email))
        {
          ModelState.AddModelError("Email", "email address has already been registered");
        }
        else
        {
          RegisterUser(org, viewModel.Email, viewModel.Password, viewModel.FirstName, viewModel.LastName,viewModel.IATA,viewModel.Affiliation);
          return Redirect(returnUrl ?? "/");
        }
      }

      return View(viewModel);
    }

    private void RegisterUser(Organization org, string email, string password, string firstName, string lastName,string IATA,string affiliation)
    {
      var user = new User
        {
          Email = email.ToLower()
        };

       user = _registrationService.RegisterCustomer(org, user, password, firstName, lastName,IATA,affiliation);
      _userMailer.SendWelcomeEmail(org, user);
    }
    private Uri RedirectUri
    {
        get
        {
            var uriBuilder = new UriBuilder(Request.Url);
            uriBuilder.Query = null;
            uriBuilder.Fragment = null;
            uriBuilder.Path = Url.RouteUrl("facebookcallback");// .Action("FacebookCallback");
            return uriBuilder.Uri;
        }
    }
    public ActionResult Facebook()
    {
        var fb = new FacebookClient();
        var loginUrl = fb.GetLoginUrl(new
        {
            client_id = "1413945942187674",
            client_secret = "4b4dbffff26c7f1ea9dcb9efa7abe1a1",
            redirect_uri = RedirectUri.AbsoluteUri,
            response_type = "code",
            scope = "email" // Add other permissions as needed
        });

        return Redirect(loginUrl.AbsoluteUri);

    }

    public ActionResult FacebookCallback(string code)
    {
        var fb = new FacebookClient();
        dynamic result = fb.Post("oauth/access_token", new
        {
            client_id = "1413945942187674",
            client_secret = "4b4dbffff26c7f1ea9dcb9efa7abe1a1",
            redirect_uri = RedirectUri.AbsoluteUri,
            code = code
        });

        var accessToken = result.access_token;

        //// Store the access token in the session
        Session["AccessToken"] = accessToken;

        // update the facebook client with the access token so 
        // we can make requests on behalf of the user
        fb.AccessToken = accessToken;

        // Get the user's information
        dynamic me = fb.Get("me?fields=first_name,last_name,id,email");
        string email = me.email;

        // Set the auth cookie
        // FormsAuthentication.SetAuthCookie(email, false);
        var viewModel = new RegisterInput();
        viewModel.FirstName=me.first_name;
        viewModel.LastName=me.last_name;
        viewModel.Email=me.email;
        return View("Create",viewModel);
    }
  }
}