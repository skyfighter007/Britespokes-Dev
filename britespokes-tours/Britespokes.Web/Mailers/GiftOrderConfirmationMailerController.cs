using System.Linq;
using ActionMailer.Net.Mvc;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Mailers
{

  public class GiftOrderConfirmationEmail
  {
      public GiftOrder GiftOrder { get; set; }
     
  }

  public class GiftOrderConfirmationMailerController : MailerBase
  {
    public EmailResult GiftOrderConfirmationEmail(GiftOrder giftorder)
    {
        //var departure = order.ProductVariants.First().ProductVariant.Product.Departure;
       // var tour = departure.Tour;
        var model = new GiftOrderConfirmationEmail
        {
            GiftOrder = giftorder
   
        };

        To.Add(giftorder.User.Email);
        // TODO: from address should be configurable somewhere
        From = "testing007111@gmail.com";
        //foreach (var item in order.GiftOrderDetail)
        //{
        //    BCC.Add(item.RecipientEmail);
        //    Subject = string.Format("brite spokes order confirmation Gift Code: {0}", item.RecipientGiftCode);
        //}
        Subject = string.Format("brite spokes gift order confirmation number: {0}", giftorder.OrderNumber);
        
        return Email("GiftOrderConfirmationEmail", model);
    }


    public EmailResult GiftOrderConfirmationEmailToRecipient(GiftOrder giftorder,int index)
    {
        //var departure = order.ProductVariants.First().ProductVariant.Product.Departure;
        // var tour = departure.Tour;
        var model = new GiftOrderConfirmationEmail
        {
            GiftOrder = giftorder

        };

        To.Add(giftorder.GiftOrderDetail[index].RecipientEmail);
        // TODO: from address should be configurable somewhere
        From = "testing007111@gmail.com";

        
        Subject = string.Format("brite spokes gift gift code: {0}", giftorder.GiftOrderDetail[index].RecipientGiftCode);

        return Email("RecipientGiftCodeEmail", model);
    }

  }
}