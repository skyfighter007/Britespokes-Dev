using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Areas.Admin.Models.SubCategories
{
    public class SubCategoryEdit
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "List Image")]
        public string ListImageURL { get; set; }
        public string ListImageAltText { get; set; }
        [Display(Name = "List Image Caption")]
        public string ListImageCaption { get; set; }
        
        [Required]
        [Display(Name = "Slider Image")]
        public string SliderImageURL { get; set; }
        public string SliderImageAltText { get; set; }
        [Display(Name = "Slider Image Caption")]
        public string SliderImageCaption { get; set; }
        [Required]
        public string Permalink { get; set; }
        public bool Published { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }

        [Display(Name = "Focus Keyword")]
        public string FocusKeyword { get; set; }
        [StringLength(70)]
        [Display(Name = "SEO Title")]
        public string SEOTitle { get; set; }
        [StringLength(156)]
        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }

        public IEnumerable<Tour> TourList { get; set; }
        public IEnumerable<Tour> SelectedTourList { get; set; }
        public List<int> SelectedTours { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public List<string> TourFromPost { get; set; }



    }
}