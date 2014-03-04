namespace Britespokes.Services.Admin.GiftCards
{
    public class GiftCardUpdate
    {
        public int Id { get; set; }
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
        public string Permalink { get; set; }
        public bool IsPublished { get; set; }

        public string FocusKeyword { get; set; }
        public string SEOTitle { get; set; }
        public string MetaDescription { get; set; }

    }

}