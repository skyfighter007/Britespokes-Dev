using System;

namespace Britespokes.Services.Admin.Orders
{
  public class OrderReportItem
  {
    public int Id { get; set;}
    public string OrderNumber { get; set; }
    public string OrderStatusName { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CompletedAt { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string IATA { get; set; }
    public string Affiliation { get; set; }
    public string SpecialInstructions { get; set; }

    public string TourName { get; set; }
    public string TourCode { get; set; }
    public string DepartureCode { get; set; }
    public DateTime DepartureDate { get; set; }
    public int TravelerCount { get; set; }
    public string Products { get; set; }

    public decimal Total { get; set; }
    public decimal ItemTotal { get; set; }
    public decimal AdjustmentTotal { get; set; }
    public string DiscountCode { get; set; }
    public string ChargeId { get; set; }

    public string BillingFirstName { get; set; }
    public string BillingLastName { get; set; }
    public string BillingAddress1 { get; set; }
    public string BillingAddress2 { get; set; }
    public string BillingCity { get; set; }
    public string BillingState { get; set; }
    public string BillingZipCode { get; set; }

    public string TravelerIds { get; set; }
    public string TravelerNames { get; set; }
    public string TravelerBirthdates { get; set; }
    public string TravelerPhoneNumbers { get; set; }
    public string TravelerEmails { get; set; }
    public string TravelerSpecialInstructions { get; set; }
  }
}