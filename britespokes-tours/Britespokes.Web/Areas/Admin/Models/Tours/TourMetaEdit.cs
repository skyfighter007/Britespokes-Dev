using System.ComponentModel.DataAnnotations;
using Britespokes.Web.Helpers;

namespace Britespokes.Web.Areas.Admin.Models.Tours
{
    public class TourMetaEdit
    {
        private string _description;
        private string _priceIncludes1;
        private string _priceIncludes2;
        private string _priceIncludes3;
        private string _priceIncludes4;
        private string _briterpriceIncludes1;
        private string _briterpriceIncludes2;
        private string _briterpriceIncludes3;
        private string _briterpriceIncludes4;
        private string _additionalInformation1;
        private string _additionalInformation2;

        public string Code { get; set; }
        public string Name { get; set; }
        public int TourId { get; set; }
        public string TourName { get; set; }
        public string TourPermalink { get; set; }
        public bool TourIsPublished { get; set; }
        public bool TourHasExperience { get; set; }
        [Display(Name = "Banner Image")]
        [Required]
        public string BannerImageUrl { get; set; }
        public string BannerImageAltText { get; set; }
        
        
        [Display(Name = "Thumbnail")]
        [Required]
        public string ThumbnailImageUrl { get; set; }
        public string ThumbnailImageAltText { get; set; }
        [Display(Name = "Thumbnail Caption")]
        [Required]
        public string ThumbnailCaption { get; set; }
        [Display(Name = "Journey Description")]
        public string JourneyDescription { get; set; }

        [Required]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = Sanitize(value);
            }
        }
        #region Brite Tab details
        [Display(Name = "Brite Tab Header1")]
        public string PriceIncludesHeader { get; set; }

        [Display(Name = "Brite Tab Header1 Contents")]
        public string PriceIncludes1
        {
            get
            {
                return _priceIncludes1;
            }
            set
            {
                _priceIncludes1 = Sanitize(value);
            }
        }

        [Display(Name = "Brite Tab Header1 Contents (2nd column)")]
        public string PriceIncludes2
        {
            get
            {
                return _priceIncludes2;
            }
            set
            {
                _priceIncludes2 = Sanitize(value);
            }
        }
        [Display(Name = "Brite Tab Header2")]
        public string PriceIncludesHeader2 { get; set; }

        [Display(Name = "Brite Tab Header2 Contents")]
        public string PriceIncludes3
        {
            get
            {
                return _priceIncludes3;
            }
            set
            {
                _priceIncludes3 = Sanitize(value);
            }
        }

        [Display(Name = "Brite Tab Header2 Contents (2nd column)")]
        public string PriceIncludes4
        {
            get
            {
                return _priceIncludes4;
            }
            set
            {
                _priceIncludes4 = Sanitize(value);
            }
        }
        #endregion

        #region Briter Tab details
        [Display(Name = "Briter Tab Header1")]
        public string BriterPriceIncludesHeader1 { get; set; }

        [Display(Name = "Briter Tab Header1 Contents")]
        public string BriterPriceIncludes1
        {
            get
            {
                return _briterpriceIncludes1;
            }
            set
            {
                _briterpriceIncludes1 = Sanitize(value);
            }
        }

        [Display(Name = "Briter Tab Header1 Contents (2nd column)")]
        public string BriterPriceIncludes2
        {
            get
            {
                return _briterpriceIncludes2;
            }
            set
            {
                _briterpriceIncludes2 = Sanitize(value);
            }
        }
        [Display(Name = "Briter Tab Header2")]
        public string BriterPriceIncludesHeader2 { get; set; }

        [Display(Name = "Briter Tab Header2 Contents")]
        public string BriterPriceIncludes3
        {
            get
            {
                return _briterpriceIncludes3;
            }
            set
            {
                _briterpriceIncludes3 = Sanitize(value);
            }
        }

        [Display(Name = "Briter Tab Header2 Contents (2nd column)")]
        public string BriterPriceIncludes4
        {
            get
            {
                return _briterpriceIncludes4;
            }
            set
            {
                _briterpriceIncludes4 = Sanitize(value);
            }
        }
        #endregion

        [Display(Name = "Additional Information Header")]
        public string AdditionalInformationHeader { get; set; }

        [Display(Name = "Additional Information")]
        public string AdditionalInformation1
        {
            get
            {
                return _additionalInformation1;
            }
            set
            {
                _additionalInformation1 = Sanitize(value);
            }
        }

        [Display(Name = "Additional Information (2nd column)")]
        public string AdditionalInformation2
        {
            get { return _additionalInformation2; }
            set { _additionalInformation2 = Sanitize(value); }
        }

        private static string Sanitize(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? value : HtmlUtility.Instance.SanitizeHtml(value);
        }
    }
}