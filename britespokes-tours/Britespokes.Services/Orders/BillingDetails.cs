using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Orders
{
  public class BillingOverview
  {
    private const string DateFormat = "MM/dd/yyyy";

    public string TourName { get; set; }
    public string DepartureCode { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public int NumberOfRooms { get; set; }
    public decimal Total { get; set; }
    public decimal AdjustmentTotal { get; set; }
    public decimal ItemTotal { get; set; }
    public List<BillingRoomSummary> Rooms { get; set; }
    public IEnumerable<State> States { get; set; }
    public IEnumerable<Country> Countries { get; set; }

    public string FormattedDepartureDate
    {
      get { return DepartureDate.ToString(DateFormat); }
    }

    public string FormattedReturnDate
    {
      get { return ReturnDate.ToString(DateFormat); }
    }

    public string FormattedTotal
    {
      get { return string.Format("{0:C}", Total); }
    }

    public string FormattedItemTotal
    {
      get { return string.Format("{0:C}", ItemTotal); }
    }

    public string FormattedAdjustmentTotal
    {
      get { return string.Format("{0:C}", AdjustmentTotal); }
    }
  }

  public class BillingRoomSummary
  {
    public int Quantity { get; set; }
    public string DisplayName { get; set; }
    public string PluralDisplayName { get; set; }

    public string QualifiedDisplayName
    {
      get { return Quantity > 1 ? PluralDisplayName : DisplayName; }
    }
  }

  public class BillingDetails
  {
    public int OrderId { get; set; }
    public string OrderNumber { get; set; }
    public string[] DiscountCodes { get; set; }
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

    [Display(Name = "gift code")]
    public string GiftCode { get; set; }

    public decimal GiftAmount=0;

    public int GiftOrderDetailId;

    public BillingOverview BillingOverview { get; set; }
    public PaymentRequest PaymentRequest { get; set; }
  }
}