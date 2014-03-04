using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Britespokes.Services.GiftCards
{
  public class GiftOrderDetails
  {
    public int Id { get; set; }
    public int GiftOrderId { get; set; }
    public int Index { get; set; }

    [Required(ErrorMessage = "required")]
    [Display(Name = "Recipient Email")]
    [DataType(DataType.EmailAddress)]
    public string RecipientEmail { get; set; }

    [Required(ErrorMessage = "required")]
    [Display(Name = "Your Name")]
    public string YourName { get; set; }

    [Required(ErrorMessage = "required")]
    [Display(Name = "Amount")]
    [RegularExpression(@"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$", ErrorMessage = "Amount is invalid.")]
    [Range(1, double.MaxValue, ErrorMessage = "Please enter gift card amount.")]
    public string DisplayAmount
    {
        get { return Amount.ToString(); }
        set { Amount =Convert.ToDecimal(value); } //assign Amount
    } 

    [Display(Name = "Message")]
    [UIHint("MultilineText")]
    public string Message { get; set; }

    public decimal Amount { get; private set; }
  }

  public class ConfirmGiftCards
  {
    public int GiftOrderId { get; set; }
    public int GiftOrderQuantity { get; set; }
    public int GiftCardName { get; set; }
    public int GiftCardDescription { get; set; }
    public List<GiftOrderDetails> GiftOrderDetail { get; set; }
  }
}