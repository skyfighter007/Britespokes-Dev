using System.Linq;
using System.Security.Principal;

namespace Britespokes.Services.Authentication
{
  public class CustomPrincipal : ICustomPrincipal
  {
    public IIdentity Identity { get; private set; }

    public bool IsInRole(string role)
    {
      return Roles.Contains(role);
    }

    public CustomPrincipal(string email)
    {
      Identity = new GenericIdentity(email);
      Roles = new string[0];
    }

    public int UserId { get; set; }
    public int OrganizationId { get; set; }
    public string[] Roles { get; set; }
    public bool IsConfirmed { get; set; }
    public string DisplayName { get; set; }

    public bool IsGuest
    {
      get { return Roles.Contains("Guest"); }
    }

    public bool IsAdmin
    {
      get { return Roles.Contains("Admin"); }
    }
  }
}
