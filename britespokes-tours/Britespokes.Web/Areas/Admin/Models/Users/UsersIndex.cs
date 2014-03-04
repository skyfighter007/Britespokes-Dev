using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Domain;
using Britespokes.Web.Helpers;

namespace Britespokes.Web.Areas.Admin.Models.Users
{
  public class UsersIndex
  {
    public const string AllRoleName = "All";

    public UsersIndex()
    {
      RoleNames = Role.RoleNames.ToList();
      RoleNames.Insert(0, AllRoleName);
    }

    public IEnumerable<User> Users { get; set; }
    public int UserCount { get; set; }
    public int GuestCount { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public string RoleFilter { get; set; }
    public List<string> RoleNames { get; set; }
  }
}