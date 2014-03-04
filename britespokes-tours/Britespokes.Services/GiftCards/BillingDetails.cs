using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Britespokes.Core.Domain;
using Britespokes.Services.Orders;

namespace Britespokes.Services.GiftCards
{
  public class BillingOverview
  {
    private const string DateFormat = "MM/dd/yyyy";

    public string GiftCardName { get; set; }
    public decimal Total { get; set; }
    public decimal ItemTotal { get; set; }
    public int NumberOfCards { get; set; }
    public IEnumerable<State> States { get; set; }
    public IEnumerable<Country> Countries { get; set; }

    public string FormattedTotal
    {
        get { return string.Format("{0:C}", Total); }
    }

    public string FormattedItemTotal
    {
        get { return string.Format("{0:C}", ItemTotal); }
    }
  }

  public class BillingRoomSummary
  {
    public int Quantity { get; set; }
 
  }

  public class BillingDetails
  {
    public int GiftOrderId { get; set; }
    public string GiftOrderNumber { get; set; }

    public int UserId { get; set; }
    public int? BillingAddressId { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Display(Name = "password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Display(Name = "confirm password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "first name")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "last name")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "street address")]
    public string Address1 { get; set; }
    [Display(Name = "address 2")]
    public string Address2 { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "city")]
    public string City { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "zip code")]
    public string ZipCode { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "state")]
    public string StateOrProvince { get; set; }
    public int CountryId { get; set; }
    [Display(Name = "email")]
    public string Phone { get; set; }
    public string MobilePhone { get; set; }
    [Display(Name = "special requests")]
    public string SpecialInstructions { get; set; }
    [Display(Name = "i've read and accept the terms & conditions")]
    public bool AcceptedTermsAndConditions { get; set; }

    public BillingOverview BillingOverview { get; set; }
    public PaymentRequest PaymentRequest { get; set; }
  }
}