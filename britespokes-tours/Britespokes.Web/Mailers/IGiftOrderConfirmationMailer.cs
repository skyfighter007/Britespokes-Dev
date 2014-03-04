using Britespokes.Core.Domain;

namespace Britespokes.Web.Mailers
{
    public interface IGiftOrderConfirmationMailer
  {
    void SendGiftOrderConfirmationEmail(GiftOrder order);

  }
}
