using Britespokes.Core.Domain;
using Britespokes.Services.Users;
using System;

namespace Britespokes.Web.Mailers
{
  public class UserMailer : IUserMailer
  {
    private readonly UserMailerController _userMailerController;
    private readonly IUserService _userService;

    public UserMailer(UserMailerController userMailerController, IUserService userService)
    {
      _userMailerController = userMailerController;
      _userService = userService;
    }

    public void SendWelcomeEmail(Organization org, User user)
    {
      // TODO: Mails are sent synchronously
      // this probably still should be refactored to use a real bg process
        try
        {
            _userMailerController.WelcomeEmail(org, user).Deliver();
            _userService.ConfirmationEmailSent(user);
        }
        catch (Exception e)
        {
        }
   
    }
  }
}
