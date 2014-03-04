using System.Collections.Generic;
using System.Linq;

namespace Britespokes.Core.Domain
{
  public class Role : Entity
  {
    public const string RoleGuestName = "Guest";
    public const string RoleCustomerName = "Customer";
    public const string RoleAdminName = "Admin";
    private static string[] _roleNames;

    public string Name { get; set; }
    public virtual ICollection<User> Users { get; set; }

    public static string[] RoleNames
    {
      get
      {
        return _roleNames ?? (_roleNames = new[]
        { RoleGuestName, RoleCustomerName, RoleAdminName });
      }
    }

    public static bool IsValidRoleName(string roleName)
    {
      return (RoleNames.Contains(roleName));
    }
  }
}
