using Britespokes.Core.Domain;

namespace Britespokes.Web.Mailers
{
    public class OrderConfirmationMailer : IOrderConfirmationMailer
    {
        private readonly OrderConfirmationMailerController _orderConfirmationMailerController;
        private readonly OrderNotificationMailerController _orderNotificationMailerController;

        public OrderConfirmationMailer(OrderConfirmationMailerController orderConfirmationMailerController,
          OrderNotificationMailerController orderNotificationMailerController)
        {
            _orderConfirmationMailerController = orderConfirmationMailerController;
            _orderNotificationMailerController = orderNotificationMailerController;
        }

        public void SendOrderConfirmationEmail(Order order)
        {
            // TODO: Mails are sent synchronously
            // this probably still should be refactored to use a real bg process
            _orderConfirmationMailerController.OrderConfirmationEmail(order).Deliver();
            _orderNotificationMailerController.OrderNotificationEmail(order).Deliver();
        }

    }
}
