using System;

namespace Britespokes.Services.Admin.Users
{
  public interface IUserPruneService
  {
    int PruneGuests();
    int PruneGuests(TimeSpan olderThan);
  }
}