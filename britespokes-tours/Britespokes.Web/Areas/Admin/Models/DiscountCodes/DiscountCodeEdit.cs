using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Britespokes.Web.Areas.Admin.Models.DiscountCodes
{
  [Bind(Exclude = "Tours,Products,ProductVariants")]
  public class DiscountCodeEdit
  {
    private string _lowerCode;

    public DiscountCodeEdit()
    {
      StartsAt = DateTime.UtcNow;
      ExpiresAt = StartsAt.AddDays(30);
      IsActive = true;
      IsGlobal = true;

      TourItems = ProductItems = ProductVariantItems = new SelectListItem[0];
    }

    public int Id { get; set; }
    public string Code { get; set; }

    [Required]
    [Display(Name = "Code")]
    public string LowerCode
    {
      get
      {
        return _lowerCode;
      }
      set
      {
        var lcode = value.ToLower();
        _lowerCode = Code = lcode;
      }
    }

    [Required]
    [Display(Name = "Start Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime StartsAt { get; set; }

    [Required]
    [Display(Name = "Expiration Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
    public DateTime ExpiresAt { get; set; }

    public bool IsActive { get; set; }
    public bool IsGlobal { get; set; }
    public bool UsePercentage { get; set; }
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }


    public IEnumerable<SelectListItem> TourItems { get; set; }
    public IEnumerable<SelectListItem> ProductItems { get; set; }
    public IEnumerable<SelectListItem> ProductVariantItems { get; set; }

    public string[] TourId { get; set; }
    public string[] ProductId { get; set; }
    public string[] ProductVariantId { get; set; }

    public string ToursJson { get; set; }
    public string ProductsJson { get; set; }
    public string ProductVariantsJson { get; set; }

    [Required]
    public decimal Discount
    {
      get
      {
        return UsePercentage ? Percentage : Amount;
      }
      set
      {
        if (UsePercentage)
          Percentage = value;
        else
          Amount = value;
      }
    }
  }
}