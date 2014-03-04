using System;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using Britespokes.Services.Security;
using Omu.ValueInjecter;
using Britespokes.Services.Authentication;
using System.Web;
using System.Web.Security;

namespace Britespokes.Services.Users
{
  public class UserService : IUserService
  {
    private readonly IRepository<User> _userRepo;
    private readonly IEncryptionService _encryptionService;


    public UserService(IRepository<User> userRepo, IEncryptionService encryptionService)
    {
      _userRepo = userRepo;
      _encryptionService = encryptionService;
      
    }

    public User Find(int userId)
    {
      return _userRepo.Find(userId);
    }

    public UpdateUserInput FindUser(int userId)
    {
        var user = _userRepo.Find(userId);

        //var updateUser = new UpdateUserInput();
        var updateUser = new UpdateUserInput()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            IATA=user.IATA,
            Affiliation=user.Affiliation
        };

        if(user.Address!=null)
        {
          updateUser.Phone = user.Address.Phone;
          updateUser.City = user.Address.City;
        }

        return updateUser;
    }

    public User AddUser(User user)
    {
      _userRepo.Add(user);
      return user;
    }

    public User UpdateUser(User user)
    {
      _userRepo.Update(user);
      return user;
    }

    public User UpdateUserAccount(UpdateUserInput updateUserInput,int userId)
    {
        var user = _userRepo.Find(userId);
        user.FirstName = updateUserInput.FirstName;
        user.LastName = updateUserInput.LastName;
        user.Email = updateUserInput.Email;
        user.IATA = updateUserInput.IATA;
        user.Affiliation = updateUserInput.Affiliation;
        var address = user.Address ?? new Address();
        address.FirstName = user.FirstName;
        address.LastName = user.LastName;
        address.Phone = updateUserInput.Phone;
        address.City = updateUserInput.City;
        address.CountryId = 226;// TODO: hardcoded country for now
        user.Address = address;
       
        _userRepo.Update(user);
        FormsAuthenticationService service = new FormsAuthenticationService(this);
        service.SetAuthenticated(user, false);       
        return user;
    }
   
    public User IsValidLogin(Organization organization, string username, string password)
    {
      User user = FindUser(organization, username);

      if (user != null)
      {
        string hashedPw = _encryptionService.ComputeHash(password, user.PasswordSalt).Text;
        if (hashedPw == user.Password)
          return user;
      }

      return null;
    }

    public User FindUser(Organization organization, string username)
    {
      return _userRepo
        .FindBy(u => u.Email.ToLower() == username.ToLower() && u.OrganizationId == organization.Id)
        .SingleOrDefault();
    }

    public string[] GetRoles(User user)
    {
      string[] roles = null;
      if (user.Roles != null)
        roles = user.Roles.Select(r => r.Name).ToArray();
      return roles;
    }

    public void ConfirmationEmailSent(User user)
    {
      user.ConfirmationSentAt = DateTime.UtcNow;
      _userRepo.Update(user);
    }

    public User ConfirmByToken(string token)
    {
      User user = _userRepo.FindBy(u => u.ConfirmationToken == token.Trim()
        .ToLower())
        .FirstOrDefault();

      if (user != null)
      {
        user.ConfirmedAt = DateTime.UtcNow;
        user.ConfirmationToken = null;
        _userRepo.Update(user);
      }

      return user;
    }
  }
}