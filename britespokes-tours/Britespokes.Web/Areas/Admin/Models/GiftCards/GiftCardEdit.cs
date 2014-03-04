using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Areas.Admin.Models.GiftCards
{
    public class GiftCardEdit
    {
        public int Id { get; set; }
        [Required]
        public string GiftCode { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string GiftDescription { get; set; }

        [Display(Name = "Published?")]
        public bool IsPublished { get; set; }

        [Display(Name = "Price")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "ThumbnailCaption")]
        [Required]
        public string ThumbnailCaption { get; set; }
        public string ThumbnailImageAltText { get; set; }
        [Display(Name = "ThumbnailDescription")]
        [Required]
        public string ThumbnailDescription { get; set; }

        [Required]
        [Display(Name = "Thumbnail Image")]
        public string ThumbnailImageURL { get; set; }
       
        [Required]
        [Display(Name = "Banner Image")]
        public string BannerImageURL { get; set; }
        public string BannerImageAltText { get; set; }

        [Display(Name = "Terms and Condition")]
        public string Terms_Condition { get; set; }

        [Required]
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
    }
}