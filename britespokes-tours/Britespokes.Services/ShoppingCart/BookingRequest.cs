using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Britespokes.Services.Orders;

namespace Britespokes.Services.ShoppingCart
{
  public class ProductRequest
  {
    [Required]
    public int VariantId { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    [Required]
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int PeoplePerRoom { get; set; }
  }

  public class BookingRequest
  {
    [Required]
    public int UserId { get; set; }
    public string Code { get; set; }
    public string TourName { get; set; }
    [Required]
    public int ProductId { get; set; }
    public DateTime DepartureDate { get; set; }
    public List<ProductRequest> ProductRequests { get; set; }
    [ValidDiscountCode]
    public string DiscountCode { get; set; }
  }
}