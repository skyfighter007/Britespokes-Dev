using ActionMailer.Net.Mvc;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Mailers
{
  public class UserMailerController : MailerBase
  {
    public EmailResult WelcomeEmail(Organization org, User user)
    {
      To.Add(user.Email);
      // TODO: from address should be configurable somewhere
      From = "testing007111@gmail.com";
      Subject = "Your brite account with brite spokes";
      return Email("WelcomeEmail", user);
    }
  }
}