using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Areas.Admin.Models.Tours
{
    public class TourEdit
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Permalink { get; set; }
        [Display(Name = "Published?")]
        public bool IsPublished { get; set; }

        [Display(Name = "Availability")]
        [Required]
        public int AvailabilityStatusId { get; set; }
        public IEnumerable<AvailabilityStatus> AvailabilityStatusList { get; set; }

        [Display(Name = "Type")]
        [Required]
        public int TourTypeId { get; set; }
        public IEnumerable<TourType> TourTypeList { get; set; }

        [Display(Name = "Length")]
        public string LengthDescription { get; set; }
        [Display(Name = "Departure City")]
        [Required]
        public string DepartureCity { get; set; }
        [Display(Name = "Returning City")]
        [Required]
        public string ReturningCity { get; set; }

        // these might be turn out to be campaign monitor specific
        [Display(Name = "Mailing List Url")]
        public string MailingListUrl { get; set; }
        [Display(Name = "Mailing List Email Name")]
        public string MailingListEmailName { get; set; }

        [Display(Name = "Mailing List Id")]
        public string MailingListID { get; set; }

        [Display(Name = "Sample Itinerary")]
        public string SampleDocumentURL { get; set; }


        public bool HasExperience { get; set; }

        //[Display(Name = "Category")]
        //[Required]
        //public int Category_Id { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<Category> SelectedCategoryList { get; set; }
        public List<int> SelectedCategories { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public List<string> CategoriesFromPost { get; set; }

        [Display(Name = "Focus Keyword")]
        public string FocusKeyword { get; set; }
        [StringLength(70)]
        [Display(Name = "SEO Title")]
        public string SEOTitle { get; set; }
        [StringLength(156)]
        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }

    }
}