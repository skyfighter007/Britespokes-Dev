using System;
using System.Linq;

namespace Britespokes.Services.Admin.Users
{
  public class UserPruneService : IUserPruneService
  {
    private readonly IUserService _userService;

    public UserPruneService(IUserService userService)
    {
      _userService = userService;
    }

    public int PruneGuests()
    {
      return PruneGuests(TimeSpan.FromHours(24));
    }

    public int PruneGuests(TimeSpan olderThan)
    {
      var dt = DateTime.UtcNow.Subtract(olderThan);
      var guests = _userService.GuestUsers().Where(u => u.UpdatedAt < dt).ToList();
      var count = guests.Count();
      foreach (var guest in guests) { _userService.Delete(guest); }
      return count;
    }
  }
}