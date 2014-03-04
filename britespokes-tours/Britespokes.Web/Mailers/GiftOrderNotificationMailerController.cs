using System.Linq;
using ActionMailer.Net.Mvc;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Mailers
{
  public class GiftOrderNotificationMailerController : MailerBase
  {
    private readonly string _giftorderNotificationEmailAddress;

    public GiftOrderNotificationMailerController(string giftorderNotificationEmailAddress)
    {
        _giftorderNotificationEmailAddress = giftorderNotificationEmailAddress;
    }

    public EmailResult GiftOrderNotificationEmail(GiftOrder giftorder)
    {
        
        var model = new GiftOrderConfirmationEmail
        {
            GiftOrder = giftorder
        };

        To.Add(giftorder.User.Email);
        // TODO: from address should be configurable somewhere
        From = "testing007111@gmail.com";
        Subject = string.Format("brite spokes order notification for Order number: {0}", giftorder.OrderNumber);
        return Email("GiftOrderNotificationEmail", model);
    }
  }
}