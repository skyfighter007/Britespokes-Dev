using System.Web.Mvc;
using Britespokes.Core.Domain;
using Britespokes.Services.Authentication;
using Britespokes.Services.Users;
using Britespokes.Web.Controllers;
using Britespokes.Web.Infrastructure.Logging;
using Britespokes.Web.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Britespokes.Web.Tests
{
  [TestClass]
  public class SessionsControllerTest
  {
    [TestMethod]
    [Ignore] // TODO: fix tests
    public void CanLoginWithValidCredentials()
    {
      var authService = new Mock<IAuthenticationService>();
      var userService = new Mock<IUserService>();
      var logger = new Mock<ILogger>();

      var viewModel = new LoginInput
      {
        Email = "admin@britespokes.com",
        Password = "secret",
        RememberMe = false
      };

      var controller = new SessionsController(authService.Object, logger.Object);
      authService.Setup(s => s.Login(It.IsAny<Organization>(),
        "admin@britespokes.com", "secret", false)).Returns(true);
      userService.Setup(s => s.IsValidLogin(It.IsAny<Organization>(),
        "admin@britespokes.com", "secret")).Returns(new User());

      var result = controller.Create(viewModel, "/admin");

      Assert.IsInstanceOfType(result, typeof(RedirectResult));
      Assert.AreEqual("/admin", ((RedirectResult)result).Url);
    }

    [TestMethod]
    [Ignore] // TODO: fix tests
    public void CannotLoginWithInvalidCredentials()
    {
      var authService = new Mock<IAuthenticationService>();
      var logger = new Mock<ILogger>();
      authService.Setup(s => s.Login(It.IsAny<Organization>(),
        "admin@britespokes.com", "badpw", false)).Returns(false);

      var viewModel = new LoginInput
      {
        Email = "admin@britespokes.com",
        Password = "badpw",
        RememberMe = false
      };

      var controller = new SessionsController(authService.Object, logger.Object);

      var result = controller.Create(viewModel, "/admin");

      Assert.IsInstanceOfType(result, typeof(ViewResult));
      Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
    }
  }
}