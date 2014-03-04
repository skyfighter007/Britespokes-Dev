using Britespokes.Core.Domain;

namespace Britespokes.Services.Users
{
  public interface IUserService
  {
    User FindUser(Organization organization, string username);
    User Find(int userId);
    UpdateUserInput FindUser(int userId);
    User AddUser(User user);
    User UpdateUser(User user);
    User UpdateUserAccount(UpdateUserInput updateUserAccount, int userId);
    User IsValidLogin(Organization organization, string username, string password);
    string[] GetRoles(User user);

    void ConfirmationEmailSent(User user);
    User ConfirmByToken(string token);
  }
}
