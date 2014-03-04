using Britespokes.Core.Domain;

namespace Britespokes.Services.Users
{
  public interface IRoleService
  {
    Role Guest();
    Role Customer();
    Role Traveler();
    Role Admin();
  }
}