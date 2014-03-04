using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Users
{
  public interface IUserService
  {
    User FindUser(int userId);
    IQueryable<User> UsersByRole(string roleName);
    IQueryable<User> RegisteredUsers();
    IQueryable<User> GuestUsers();
    int RegisteredCount();
    int GuestCount();
    void UpdateComment(int userId, string adminComment);
    void ToggleAdmin(int userId);
    void Delete(User user);
  }
}
