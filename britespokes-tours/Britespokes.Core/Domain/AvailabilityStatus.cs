namespace Britespokes.Core.Domain
{
  public class AvailabilityStatus : Entity
  {
    public const int Available = 1;
    public const int ComingSoon = 2;
    public const int SoldOut = 3;
    public const int StudentInquiryForm = 4;

    public string Name { get; set; }
  }
}
