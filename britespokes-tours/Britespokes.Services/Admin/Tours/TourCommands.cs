using System.Collections.Generic;
namespace Britespokes.Services.Admin.Tours
{
  public class TourUpdate
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Permalink { get; set; }
    public bool IsPublished { get; set; }
    public int AvailabilityStatusId { get; set; }
    public int Category_Id { get; set; }
    public int TourTypeId { get; set; }
    public string LengthDescription { get; set; }
    public string DepartureCity { get; set; }
    public string ReturningCity { get; set; }
    public string MailingListUrl { get; set; }
    public string MailingListEmailName { get; set; }
    public string MailingListID { get; set; }
    public string SampleDocumentURL { get; set; }
    public string FocusKeyword { get; set; }
    public string SEOTitle { get; set; }
    public string MetaDescription { get; set; }

    public List<int> SelectedCategories { get; set; }
  }

  public class TourMetaUpdate
  {
    public int TourId { get; set; }
    public bool TourIsPublished { get; set; }
    public string Description { get; set; }
    public string PriceIncludesHeader { get; set; }
    public string PriceIncludes1 { get; set; }
    public string PriceIncludes2 { get; set; }
    public string PriceIncludesHeader2 { get; set; }
    public string PriceIncludes3 { get; set; }
    public string PriceIncludes4 { get; set; }
    public string BriterPriceIncludesHeader1 { get; set; }
    public string BriterPriceIncludes1 { get; set; }
    public string BriterPriceIncludes2 { get; set; }
    public string BriterPriceIncludesHeader2 { get; set; }
    public string BriterPriceIncludes3 { get; set; }
    public string BriterPriceIncludes4 { get; set; }
    public string AdditionalInformationHeader { get; set; }
    public string AdditionalInformation1 { get; set; }
    public string AdditionalInformation2 { get; set; }
    public string BannerImageUrl { get; set; }
    public string BannerImageAltText { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string ThumbnailImageAltText { get; set; }
    public string ThumbnailCaption { get; set; }
    public string JourneyDescription { get; set; }
  }
}