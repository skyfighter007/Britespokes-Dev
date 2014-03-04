using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Models.GiftCards
{
  public class GiftCardShow
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
      public bool IsPublished { get; set; }
      public string Permalink { get; set; }

      public string Color { get; set; }


       public int Quantity { get; set; }
  }
}