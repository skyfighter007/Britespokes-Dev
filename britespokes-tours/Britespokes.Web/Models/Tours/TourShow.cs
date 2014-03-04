using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Models.Tours
{
    public class TourShow
    {
        private string _description;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Permalink { get; set; }
        public string LengthDescription { get; set; }
        public string DepartureCity { get; set; }
        public string ReturningCity { get; set; }

        public string MailingListUrl { get; set; }
        public string MailingListID { get; set; }
        public bool IsTripBooked = false;
        public string MailingListEmailName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailToSubscribe { get; set; }

        public AvailabilityStatus AvailabilityStatus { get; set; }
        public TourType TourType { get; set; }
        public List<Departure> Departures { get; set; }
        public List<TourTimelineItem> TourTimelineItems { get; set; }

        public decimal? BestSinglePrice { get; set; }
        public decimal? BestDoublePrice { get; set; }
        public decimal? BestTriplePrice { get; set; }
        public decimal? BestQuadPrice { get; set; }
        public decimal? BriterBestSinglePrice { get; set; }
        public decimal? BriterBestDoublePrice { get; set; }
        public decimal? BriterBestTriplePrice { get; set; }
        public decimal? BriterBestQuadPrice { get; set; }

        public string Description
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_description))
                    return !_description.StartsWith("<p>") ? string.Format("<p>{0}</p>", _description) : _description;
                return _description;
            }
            set { _description = value; }
        }

        public string BannerImageUrl { get; set; }
        public string BannerImageAltText { get; set; }
        //for brite package
        public string PriceIncludesHeader { get; set; }
        public string PriceIncludes1 { get; set; }
        public string PriceIncludes2 { get; set; }
        public string PriceIncludesHeader2 { get; set; }
        public string PriceIncludes3 { get; set; }
        public string PriceIncludes4 { get; set; }
        //for brite package
        //for briter package
        public string briterPriceIncludesHeader1 { get; set; }
        public string briterPriceIncludes1 { get; set; }
        public string briterPriceIncludes2 { get; set; }
        public string briterPriceIncludesHeader2 { get; set; }
        public string briterPriceIncludes3 { get; set; }
        public string briterPriceIncludes4 { get; set; }
        //for briter package
        public string AdditionalInformationHeader { get; set; }
        public string AdditionalInformation1 { get; set; }
        public string AdditionalInformation2 { get; set; }

        public string SampleDocumentURL { get; set; }

        public bool ShowMailingListForm
        {
            get
            {
                return !(string.IsNullOrEmpty(MailingListUrl) || string.IsNullOrEmpty(MailingListEmailName) || string.IsNullOrEmpty(MailingListID));
            }
            
        }

      

        public List<PerspectivePost> PerspectivePosts { get; set; }
    }
}