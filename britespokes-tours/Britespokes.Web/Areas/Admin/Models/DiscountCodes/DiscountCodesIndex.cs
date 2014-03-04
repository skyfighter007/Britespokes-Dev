using System;
using System.Collections.Generic;

namespace Britespokes.Web.Areas.Admin.Models.DiscountCodes
{
  public class DiscountCodesIndex
  {
    public IEnumerable<DiscountCodeIndex> DiscountCodes { get; set; }
    public int Count { get; set; }
  }

  public class DiscountCodeIndex
  {
    public int Id { get; set; }
    public string LowerCode { get; set; }
    public DateTime? StartsAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsGlobal { get; set; }
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }
    public bool UsePercentage { get; set; }

    public int NumberOfTimesUsed { get; set; }

    public string FormattedDiscount
    {
      get {
        return UsePercentage ? string.Format("{0:0}%", Percentage) : string.Format("{0:C}", Amount);
      }
    }
  }
}