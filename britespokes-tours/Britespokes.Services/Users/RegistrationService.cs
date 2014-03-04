using System;
using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Domain;
using Britespokes.Services.Authentication;
using Britespokes.Services.Security;

namespace Britespokes.Services.Users
{
  public class RegistrationService : IRegistrationService
  {
    private readonly IUserService _userService;
    private readonly IAuthenticationService _authService;
    private readonly IRoleService _roleService;
    private readonly IEncryptionService _encryptionService;

    public RegistrationService(IUserService userService, IAuthenticationService authService,
      IRoleService roleService, IEncryptionService encryptionService)
    {
      _userService = userService;
      _authService = authService;
      _roleService = roleService;
      _encryptionService = encryptionService;
    }

    public User RegisterCustomer(Organization organization, User user, string password, string firstName, string lastName, string IATA, string affiliation)
    {
      // assumes an unsaved, in-memory user
      SetCredentials(user, password);
      user.FirstName = firstName;
      user.LastName = lastName;
      user.OrganizationId = organization.Id;
      user.IATA = IATA;
      user.Affiliation = affiliation;
      user.AddRole(_roleService.Customer());
      _userService.AddUser(user);
      _authService.Login(organization, user.Email, password, false);
      return user;
    }

    public User RegisterGuest(Organization organization)
    {
      var guest = new User
        {
          IsActive = true,
          OrganizationId = organization.Id
        };
      guest.AddRole(_roleService.Guest());
      _userService.AddUser(guest);
      _authService.SetAuthenticated(guest);
      return guest;
    }

    public User PromoteGuest(User guest, string email, string password)
    {
      guest.Email = email;
      SetCredentials(guest, password);
      guest.AddRole(_roleService.Customer());
      guest.RemoveRole(_roleService.Guest());
      _userService.UpdateUser(guest);
      _authService.Reauthenticate(guest);
      return guest;
    }

    public bool IsRegistered(Organization organization, string username)
    {
      return _userService.FindUser(organization, username) != null;
    }

    public bool IsAllowedEmailDomain(Organization organization, string emailAddress)
    {
      if (!organization.RestrictedEmailDomains)
        return true;

      if (emailAddress.IndexOf('@') == -1)
        return false;

      if (organization.EmailDomains == null)
        organization.EmailDomains = new List<EmailDomain>();

      string[] domainList = organization.EmailDomains.Select(d => d.Domain).ToArray();
      string emailDomain = emailAddress.Split('@')[1];
      return domainList.Contains(emailDomain);
    }

    private void SetCredentials(User user, string password)
    {
      var hashedText = _encryptionService.ComputeHash(password);
      user.PasswordSalt = hashedText.Salt;
      user.Password = hashedText.Text;
      user.ConfirmationToken = Guid.NewGuid().ToString().ToLower().Replace("-", "");
    }
  }
}
