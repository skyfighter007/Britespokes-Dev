namespace Britespokes.Core.Domain
{
  public class TourMeta : BaseEntity
  {
    public int TourId { get; set; }
    public virtual Tour Tour { get; set; }

    public string Description { get; set; }
    public string PriceIncludesHeader { get; set; }
    public string PriceIncludes1 { get; set; }
    public string PriceIncludes2 { get; set; }
    public string PriceIncludesHeader2 { get; set; }
    public string PriceIncludes3 { get; set; }
    public string PriceIncludes4 { get; set; }
    public string AdditionalInformationHeader { get; set; }
    public string AdditionalInformation1 { get; set; }
    public string AdditionalInformation2 { get; set; }

    public string BannerImageUrl { get; set; }
    public string BannerImageAltText { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string ThumbnailImageAltText { get; set; }
    public string ThumbnailCaption { get; set; }
    public string JourneyDescription { get; set; }

    public string BriterPriceIncludesHeader1 { get; set; }
    public string BriterPriceIncludes1 { get; set; }
    public string BriterPriceIncludes2 { get; set; }
    public string BriterPriceIncludesHeader2 { get; set; }
    public string BriterPriceIncludes3 { get; set; }
    public string BriterPriceIncludes4 { get; set; }
  }
}
