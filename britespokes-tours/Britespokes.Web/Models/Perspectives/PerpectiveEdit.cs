using System.ComponentModel.DataAnnotations;
using Britespokes.Web.Infrastructure.Validation;
using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Models.Perspectives
{
    public class PerspectiveEdit
    {
        public int Id { get; set; }
        //[Required]
        //public string Code { get; set; }
        //[Required]
        //public string Name { get; set; }
        //public string Description { get; set; }
        //[Required]
        //public string ListImageURL { get; set; }
        //public string ListImageCaption { get; set; }
        //[Required]
        //public string SliderImageURL { get; set; }
        //public string SliderImageCaption { get; set; }
        //[Required]
        //public string Permalink { get; set; }
        //public bool Published { get; set; }
        [Required]

        public string Headline { get; set; }
        [Display(Name = "Body Content")]
        public string BodyContent { get; set; }
        [Display(Name = "Thumbnail Image")]
        public string ThumbnailImageURL { get; set; }
        public string ThumbnailImageAltText { get; set; }
        [Display(Name = "Image 1")]
        public string Image1URL { get; set; }
        public string Image1AltText { get; set; }
         [Display(Name = "Image 1 Caption")]
        public string Image1Caption { get; set; }
        [Display(Name = "Image 2")]
        public string Image2URL { get; set; }
        public string Image2AltText { get; set; }
        [Display(Name = "Image 2 Caption")]
        public string Image2Caption { get; set; }

        [Display(Name = "Image 3")]
        public string Image3URL { get; set; }
        public string Image3AltText { get; set; }
        [Display(Name = "Image 3 Caption")]
        public string Image3Caption { get; set; }
        [Display(Name = "Image 4")]
        public string Image4URL { get; set; }
        public string Image4AltText { get; set; }
        [Display(Name = "Image 4 Caption")]
        public string Image4Caption { get; set; }

        [Display(Name = "Permalink")]
        public string Permalink { get; set; }
        [Display(Name = "Focus Keyword")]
        public string FocusKeyword { get; set; }
        [StringLength(70)]
        [Display(Name = "SEO Title")]
        public string SEOTitle { get; set; }
        [StringLength(156)]
        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }
        [Display(Name = "Is Published")]
        public bool IsPublished { get; set; }
       
        //public IEnumerable<Category> CategoryList { get; set; }
        //public IEnumerable<Category> SelectedCategoryList { get; set; }
        //public List<int> SelectedCategories { get; set; }
        //[Required(ErrorMessage = "Category is required")]
        //public List<string> CategoriesFromPost { get; set; }

        [Display(Name = "Tour")]
        [Required]
        public int TourId { get; set; }
        public IEnumerable<Tour> TourList { get; set; }

    }
}