using System;
using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
    public class Category : Entity
    {
        public Category()
        {
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public DateTime? AvailableOn { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ListImageURL { get; set; }
        public string ListImageAltText { get; set; }
        public string ListImageCaption { get; set; }
        public string SliderImageURL { get; set; }
        public string SliderImageAltText { get; set; }
        public string SliderImageCaption { get; set; }
        //public string SliderCaption { get; set; }
        //public string Description { get; set; }
        public string Permalink { get; set; }
        public virtual List<Tour> Tours { get; set; }
        public virtual List<SEOTool> SEOTools { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }

        public bool Published { get; set; }

        
    }
}
