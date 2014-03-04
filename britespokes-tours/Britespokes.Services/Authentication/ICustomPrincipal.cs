using System.Security.Principal;

namespace Britespokes.Services.Authentication
{
  public interface ICustomPrincipal : IPrincipal
  {
    int UserId { get; set; }
    int OrganizationId { get; set; }
    string[] Roles { get; set; }
    bool IsConfirmed { get; set; }
    bool IsGuest { get; }
    string DisplayName { get; }
  }
}
