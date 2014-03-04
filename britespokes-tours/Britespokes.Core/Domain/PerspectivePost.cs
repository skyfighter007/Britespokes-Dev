using System;
using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
    public class PerspectivePost : Entity
    {
        public PerspectivePost()
        {
            PostedOn = UpdatedAt = DateTime.UtcNow;
        }

        public int UserId { get; set; }
        public virtual User User { get; set; }


        public string Headline { get; set; }
        public string BodyContent { get; set; }
        public string ThumbnailImageURL { get; set; }
        public string ThumbnailImageAltText  { get; set; }
        public string Image1URL { get; set; }
        public string Image1AltText { get; set; }
        public string Image1Caption { get; set; }
        public string Image2URL { get; set; }
        public string Image2AltText { get; set; }
        public string Image2Caption { get; set; }
        public string Image3URL { get; set; }
        public string Image3AltText { get; set; }
        public string Image3Caption { get; set; }
        public string Image4URL { get; set; }
        public string Image4AltText { get; set; }
        public string Image4Caption { get; set; }
        public string Permalink { get; set; }
        public string FocusKeyword { get; set; }
        public string SEOTitle { get; set; }
        public string MetaDescription { get; set; }
        public bool IsPublished { get; set; }
        
        public DateTime? PostedOn { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int TourId { get; set; }
        public virtual Tour Tours { get; set; }

        public virtual List<SEOTool> SEOTools { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
