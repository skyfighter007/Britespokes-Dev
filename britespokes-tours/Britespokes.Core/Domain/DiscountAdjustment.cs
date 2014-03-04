namespace Britespokes.Core.Domain
{
  public class DiscountAdjustment : Adjustment
  {
    public int? DiscountCodeId { get; set; }
    public DiscountCode DiscountCode { get; set; }
  }
}