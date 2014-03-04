using Britespokes.Core.Domain;

namespace Britespokes.Services.Authentication
{
  public interface IAuthenticationService
  {
    void SetAuthenticated(User user, bool persistent = false);
    void Reauthenticate(User user, bool isPersistent = false);
    bool Login(Organization organization, string username, string password, bool persistent);
    void Logout();
  }
}
