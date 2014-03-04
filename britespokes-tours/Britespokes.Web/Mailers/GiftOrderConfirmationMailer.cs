using Britespokes.Core.Domain;

namespace Britespokes.Web.Mailers
{
    public class GiftOrderConfirmationMailer : IGiftOrderConfirmationMailer
    {
        private readonly GiftOrderConfirmationMailerController _giftorderConfirmationMailerController;
        private readonly GiftOrderNotificationMailerController _giftorderNotificationMailerController;

        public GiftOrderConfirmationMailer(GiftOrderConfirmationMailerController giftorderConfirmationMailerController,
          GiftOrderNotificationMailerController giftorderNotificationMailerController)
        {
            _giftorderConfirmationMailerController = giftorderConfirmationMailerController;
            _giftorderNotificationMailerController = giftorderNotificationMailerController;
        }

        public void SendGiftOrderConfirmationEmail(GiftOrder order)
        {
            // TODO: Mails are sent synchronously
            // this probably still should be refactored to use a real bg process
            _giftorderConfirmationMailerController.GiftOrderConfirmationEmail(order).Deliver();
            _giftorderNotificationMailerController.GiftOrderNotificationEmail(order).Deliver();
            
            for (int i = 0; i < order.GiftOrderDetail.Count; i++)
            {
                _giftorderConfirmationMailerController.GiftOrderConfirmationEmailToRecipient(order,i).Deliver();

            }
        }
    }
}
