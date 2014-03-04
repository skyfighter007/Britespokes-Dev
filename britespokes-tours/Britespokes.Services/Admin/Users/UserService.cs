using System.Data.Entity;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Users
{
  public class UserService : IUserService
  {
    private readonly IRepository<User> _userRepo;
    private readonly IRepository<Role> _roleRepo;

    public UserService(IRepository<User> userRepo, IRepository<Role> roleRepo)
    {
      _userRepo = userRepo;
      _roleRepo = roleRepo;
    }

    public IQueryable<User> UsersByRole(string roleName)
    {
      return !Role.IsValidRoleName(roleName)
        ? _userRepo.All.Include(u => u.Roles)
        : _userRepo.All.Include(u => u.Roles).Where(u => u.Roles.Select(r => r.Name).Contains(roleName));
    }

    public IQueryable<User> RegisteredUsers()
    {
      var users = _userRepo.All.
                            Include(u => u.Roles).
                            Where(u => !u.Roles.Select(r => r.Name).Contains("Guest"));
      return users;
    }

    public IQueryable<User> GuestUsers()
    {
      return UsersByRole("Guest");
    }

    public User FindUser(int userId)
    {
      return _userRepo.Find(userId);
    }

    public int RegisteredCount()
    {
      return RegisteredUsers().Count();
    }

    public int GuestCount()
    {
      return GuestUsers().Count();
    }

    public void UpdateComment(int userId, string adminComment)
    {
      var user = FindUser(userId);
      if (user == null) return;
      user.AdminComment = adminComment;
      _userRepo.Update(user);
    }

    public void ToggleAdmin(int userId)
    {
      var user = FindUser(userId);
      if (user == null) return;
      user.SetRole(user.IsAdmin ? FindRole(Role.RoleCustomerName) : FindRole(Role.RoleAdminName));
      _userRepo.Update(user);
    }

    public void Delete(User user)
    {
      _userRepo.Delete(user);
    }

    private Role FindRole(string name)
    {
      return _roleRepo.FindBy(r => r.Name == name).Single();
    }
  }
}