using Britespokes.Core.Domain;
using Britespokes.Services.Authentication;
using Britespokes.Services.Users;
using Britespokes.Web.Mailers;
using System.Web.Mvc;

namespace Britespokes.Web.Controllers
{
  public class ConfirmationsController : BritespokesController
  {
    private readonly IUserService _userService;
    private readonly IAuthenticationService _authService;
    private readonly IUserMailer _userMailer;

    public ConfirmationsController(IUserService userService, IAuthenticationService authService, IUserMailer userMailer)
    {
      _userService = userService;
      _authService = authService;
      _userMailer = userMailer;
    }

    [Authorize]
    public ActionResult Create()
    {
      var user = _userService.FindUser(UserContext.Organization, UserContext.Email);
      if (user != null)
      {
        _userMailer.SendWelcomeEmail(UserContext.Organization, user);
        TempData["Info"] = "We have resent your confirmation email. Please check your inbox and spam folders.";
      }
      else
      {
        TempData["Error"] = "We were unable to resend your confirmation email at this time.";
      }
      return RedirectToAction("Index", "Home");
    }

    //
    // GET: /confirmation/?token=abcdef
    [AllowAnonymous]
    public ActionResult Show(string token)
    {
      User user = _userService.ConfirmByToken(token);

      if (user != null)
      {
        // update the authentication ticket
        _authService.Reauthenticate(user);
        // set message
        TempData["Info"] = "Thanks for confirming your email address.";
        // login if not already logged in
        if (!UserContext.IsAuthenticated)
          _authService.SetAuthenticated(user);
      }
      else
      {
        TempData["Error"] = "We could not confirm your account at this time.";
      }
      return RedirectToAction("Index", "Home");
    }
  }
}