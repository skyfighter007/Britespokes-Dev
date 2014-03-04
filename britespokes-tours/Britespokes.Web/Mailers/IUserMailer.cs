using Britespokes.Core.Domain;

namespace Britespokes.Web.Mailers
{
  public interface IUserMailer
  {
    void SendWelcomeEmail(Organization org, User user);
  }
}
