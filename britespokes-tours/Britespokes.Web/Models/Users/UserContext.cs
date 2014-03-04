using Britespokes.Core.Domain;
using Britespokes.Services.Authentication;

namespace Britespokes.Web.Models.Users
{
  public class UserContext
  {
    private readonly Organization _organization;
    private readonly CustomPrincipal _customPrincipal;

    public UserContext(Organization organization, CustomPrincipal customPrincipal)
    {
      _organization = organization;
      _customPrincipal = customPrincipal;
    }

    public Organization Organization
    {
      get { return _organization; }
    }

    public int UserId
    {
      get { return _customPrincipal.UserId; }
    }

    public string Email
    {
      get { return _customPrincipal.Identity.Name; }
    }

    public string DisplayName
    {
      get { return _customPrincipal.DisplayName; }
    }

    public bool IsAuthenticated
    {
      get { return _customPrincipal != null && _customPrincipal.Identity.IsAuthenticated; }
    }

    public bool IsGuest
    {
      get { return _customPrincipal != null && _customPrincipal.Identity.IsAuthenticated && _customPrincipal.IsGuest; }
    }

    public bool IsConfirmed
    {
      get { return _customPrincipal != null && _customPrincipal.Identity.IsAuthenticated && _customPrincipal.IsConfirmed; }
    }
  }
}