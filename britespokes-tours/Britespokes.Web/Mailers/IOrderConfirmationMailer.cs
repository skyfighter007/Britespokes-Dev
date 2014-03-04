using Britespokes.Core.Domain;

namespace Britespokes.Web.Mailers
{
  public interface IOrderConfirmationMailer
  {
    void SendOrderConfirmationEmail(Order order);

  }
}
