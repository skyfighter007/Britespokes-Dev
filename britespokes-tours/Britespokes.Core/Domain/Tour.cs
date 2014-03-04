using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
  public class Tour : Entity
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public string Permalink { get; set; }
    public string LengthDescription { get; set; }
    public string DepartureCity { get; set; }
    public string ReturningCity { get; set; }

    // these might be turn out to be campaign monitor specific
    public string MailingListUrl { get; set; }
    public string MailingListID { get; set; }
    public string MailingListEmailName { get; set; }
    public string SampleDocumentURL { get; set; }

    public bool IsPublished { get; set; }

    public int? AvailabilityStatusId { get; set; }
    public virtual AvailabilityStatus AvailabilityStatus { get; set; }

    public int TourTypeId { get; set; }
    public virtual TourType TourType { get; set; }
       
    public virtual List<Departure> Departures { get; set; }
    public virtual List<TourTimelineItem> TourTimelineItems { get; set; }
    public virtual List<Experience> Experiences { get; set; }
    public virtual List<DiscountCode> DiscountCodes { get; set; }

    public virtual TourMeta TourMeta { get; set; }

    //public virtual Category Category { get; set; }

    public virtual List<Category> Categories { get; set; }
    public virtual List<SubCategory> SubCategories { get; set; }
    public virtual List<PerspectivePost> PerspectivePosts { get; set; }

    public virtual List<SEOTool> SEOTools { get; set; }

  }
}
