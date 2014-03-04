using System;
using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
    public class SEOToolStaticPage : Entity
    {
        public SEOToolStaticPage()
        {
            PostedOn = UpdatedAt = DateTime.UtcNow;
        }
        
        public string FocusKeyword { get; set; }
        public string SEOTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Permalink { get; set; }

        public DateTime? PostedOn { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
