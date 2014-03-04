using System.Web.Mvc;
using Britespokes.Services.Authentication;
using Britespokes.Web.Infrastructure.Filters;
using Britespokes.Web.Infrastructure.Logging;
using Britespokes.Web.Models.Users;

namespace Britespokes.Web.Controllers
{
  public class SessionsController : BritespokesController
  {
    private readonly IAuthenticationService _authService;
    private readonly ILogger _logger;

    public SessionsController(IAuthenticationService authService, ILogger logger)
    {
      _authService = authService;
      _logger = logger;
    }

    [AllowAnonymous]
      //[ProductionRequireHttps]
    public ActionResult Create()
    {
      if (UserContext.IsAuthenticated && !UserContext.IsGuest)
        return Redirect("/");
      return View();
    }

    [AllowAnonymous]
    [HttpPost]
      //[ProductionRequireHttps]
    public ActionResult Create(LoginInput login, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        if (_authService.Login(UserContext.Organization, login.Email, login.Password, login.RememberMe))
        {
          _logger.Info("Authenticated {0} in {1}", login.Email, UserContext.Organization.Name);
          return Redirect(returnUrl ?? "/");
        }

        ModelState.AddModelError("Password", "incorrect email address or password");
      }

      return View("Create");
    }

    public RedirectResult Delete(string returnUrl = "~/")
    {
      _authService.Logout();
      return Redirect(returnUrl);
    }
  }
}