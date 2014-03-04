using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Britespokes.Core.Domain;
using Britespokes.Services.Users;

namespace Britespokes.Services.Authentication
{
  public class FormsAuthenticationService : IAuthenticationService
  {
    private readonly IUserService _userService;
    private const int CookieVersion = 1;

    public FormsAuthenticationService(IUserService userService)
    {
      _userService = userService;
    }

    // assumes user has authenticated in some way, i.e. via the confirmation
    // process. Call this method with care
    public void SetAuthenticated(User user, bool persistent = false)
    {
      if (user != null)
      {
        var cookieName = CookieName(user);
        var encryptedTicket = GenerateEncryptedTicket(user, cookieName, persistent);
        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        HttpContext.Current.Response.Cookies.Add(authCookie);
      }
    }

    public void Reauthenticate(User user, bool isPersistent = false)
    {
      var authCookie = FormsAuthentication.GetAuthCookie(user.Email, false);
      authCookie.Value = GenerateEncryptedTicket(user, user.Email, isPersistent);
      HttpContext.Current.Response.Cookies.Set(authCookie);
    }

    public bool Login(Organization organization, string username, string password, bool persistent = false)
    {
      User user = _userService.IsValidLogin(organization, username, password);
      if (user != null)
      {
        SetAuthenticated(user, persistent);
        LogLogin(user);
      }
      return user != null;
    }

    public void Logout()
    {
      FormsAuthentication.SignOut();
    }

    private string GenerateEncryptedTicket(User user, string cookieName, bool persistent)
    {
      if (user == null) return null;

      var serializeModel = new CustomPrincipalSerializeModel
        {
          UserId = user.Id,
          OrganizationId = user.OrganizationId,
          IsConfirmed = user.IsConfirmed,
          Roles = _userService.GetRoles(user),
          DisplayName = user.FirstName
        };

      var serializer = new JavaScriptSerializer();
      var userData = serializer.Serialize(serializeModel);

      var authTicket = new FormsAuthenticationTicket(
        CookieVersion, // version
        cookieName, // name
        DateTime.UtcNow, //created
        DateTime.UtcNow.AddMinutes(60), // expires
        persistent, // persistent?
        userData // user data
        );

      return FormsAuthentication.Encrypt(authTicket);
    }

    private string CookieName(User user)
    {
      var name = user.Email ?? Guid.NewGuid().ToString();
      return string.Format("__BRITESPOKES_{0}", name);
    }

    private void LogLogin(User user)
    {
       user.LastLoginAt = DateTime.UtcNow;
      _userService.UpdateUser(user);
    }
  }
}
