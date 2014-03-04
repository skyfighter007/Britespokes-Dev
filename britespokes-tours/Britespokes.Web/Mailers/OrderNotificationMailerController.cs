using System.Linq;
using ActionMailer.Net.Mvc;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Mailers
{
    public class OrderNotificationMailerController : MailerBase
    {
        private readonly string _orderNotificationEmailAddress;

        public OrderNotificationMailerController(string orderNotificationEmailAddress)
        {
            _orderNotificationEmailAddress = orderNotificationEmailAddress;
        }


        public EmailResult OrderNotificationEmail(Order order)
        {
            var departure = order.ProductVariants.First().ProductVariant.Product.Departure;
            var tour = departure.Tour;
            var model = new OrderConfirmationEmail
              {
                  Order = order,
                  Tour = tour,
                  Departure = departure
              };

            To.Add(_orderNotificationEmailAddress);
            // TODO: from address should be configurable somewhere
            From = "testing007111@gmail.com";
            Subject = string.Format("brite spokes order notification for Trip number: {0}", order.OrderNumber);
            return Email("OrderNotificationEmail", model);
        }

    }
}