using System.Linq;
using ActionMailer.Net.Mvc;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Mailers
{
  public class OrderConfirmationEmail
  {
   
    public Order Order { get; set; }
    public Tour Tour { get; set; }
    public Departure Departure { get; set; }
  }


  public class OrderConfirmationMailerController : MailerBase
  {
    public EmailResult OrderConfirmationEmail(Order order)
    {
      var departure = order.ProductVariants.First().ProductVariant.Product.Departure;
      var tour = departure.Tour;
      var model = new OrderConfirmationEmail
        {
          Order = order,
          Tour = tour,
          Departure = departure
        };

      To.Add(order.User.Email);
      // TODO: from address should be configurable somewhere
      From = "testing007111@gmail.com";

      Subject = string.Format("brite spokes order confirmation Trip number: {0}", order.OrderNumber);
      return Email("OrderConfirmationEmail", model);
    }

  }
}