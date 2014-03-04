using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
    public class GiftCard : Entity
    {
        public string GiftCode { get; set; }
        public string Name { get; set; }
        public string GiftDescription { get; set; }
        public decimal Price { get; set; }
        public string BannerImageURL { get; set; }
        public string BannerImageAltText { get; set; }
        public string ThumbnailImageURL { get; set; }
        public string ThumbnailImageAltText { get; set; }
        public string ThumbnailCaption { get; set; }
        public string ThumbnailDescription { get; set; }
        public string Terms_Condition { get; set; }
        public bool IsPublished { get; set; }
        public virtual List<SEOTool> SEOTools { get; set; }
        public string Permalink { get; set; }
        

    }
}
