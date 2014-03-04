using System.ComponentModel.DataAnnotations;
using Britespokes.Web.Infrastructure.Validation;
using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Areas.Admin.Models.SEOToolStaticPages
{
    public class SEOToolStaticPageEdit
    {
        public int Id { get; set; }

        [Required]
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