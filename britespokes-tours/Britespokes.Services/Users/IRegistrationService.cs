using Britespokes.Core.Domain;

namespace Britespokes.Services.Users
{
  public interface IRegistrationService
  {
      User RegisterCustomer(Organization organization, User user, string password, string firstName, string lastName, string IATA, string affiliation);
    User RegisterGuest(Organization organization);
    User PromoteGuest(User guest, string email, string password);
    bool IsRegistered(Organization organization, string username);
    bool IsAllowedEmailDomain(Organization organization, string emailAddress);
  }
}
