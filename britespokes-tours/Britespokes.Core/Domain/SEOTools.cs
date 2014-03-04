using System;
using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
    public class SEOTool : Entity
    {
        public SEOTool()
        {
            PostedOn = UpdatedAt = DateTime.UtcNow;
        }
        
        //public string Permalink { get; set; }

        public string FocusKeyword { get; set; }
        public string SEOTitle { get; set; }
        public string MetaDescription { get; set; }

        public virtual List<GiftCard> GiftCards { get; set; }
        public virtual List<Tour> Tours { get; set; }
        public virtual List<Category> Categories { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
        public virtual List<PerspectivePost> PerspectivePosts { get; set; }
        

        public DateTime? PostedOn { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
