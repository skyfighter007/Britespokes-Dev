using System;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using System.Collections.Generic;

namespace Britespokes.Services.Admin.GiftCards
{
    public class GiftCardCommandService : IGiftCardCommandService
    {
        private readonly IRepository<GiftCard> _giftcardRepo;


        public GiftCardCommandService(IRepository<GiftCard> giftcardRepo)
        {
            _giftcardRepo = giftcardRepo;

        }
        public void Add(GiftCardUpdate giftcardUpdate)
        {
            var giftcard = new GiftCard
            {
                GiftCode = giftcardUpdate.GiftCode,
                Name = giftcardUpdate.Name,
                GiftDescription = giftcardUpdate.GiftDescription,
                Price = giftcardUpdate.Price,
                BannerImageURL = giftcardUpdate.BannerImageURL,
                BannerImageAltText = giftcardUpdate.BannerImageAltText,
                ThumbnailImageURL = giftcardUpdate.ThumbnailImageURL,
                ThumbnailImageAltText = giftcardUpdate.ThumbnailImageAltText,
                ThumbnailCaption = giftcardUpdate.ThumbnailCaption,
                ThumbnailDescription = giftcardUpdate.ThumbnailDescription,
                Terms_Condition = giftcardUpdate.Terms_Condition,
                Permalink= giftcardUpdate.Permalink,
                IsPublished = giftcardUpdate.IsPublished,

            };



            SEOTool SEOTool = null;

            if (giftcard.SEOTools != null && giftcard.SEOTools.Count() > 0)
                SEOTool = giftcard.SEOTools.First();
            else
            {
                SEOTool = new Core.Domain.SEOTool();
                giftcard.SEOTools = new List<Core.Domain.SEOTool>();
                giftcard.SEOTools.Add(SEOTool);
            }

            SEOTool.FocusKeyword = giftcardUpdate.FocusKeyword;
            SEOTool.MetaDescription = giftcardUpdate.MetaDescription;
            SEOTool.SEOTitle = giftcardUpdate.SEOTitle;

            _giftcardRepo.Add(giftcard);
        }

        public void Update(GiftCardUpdate giftcardUpdate)
        {
            var giftcard = _giftcardRepo.Find(giftcardUpdate.Id);

            giftcard.GiftCode = giftcardUpdate.GiftCode;
            giftcard.Name = giftcardUpdate.Name;
            giftcard.GiftDescription = giftcardUpdate.GiftDescription;
            giftcard.Price = giftcardUpdate.Price;
            giftcard.BannerImageURL = giftcardUpdate.BannerImageURL;
            giftcard.BannerImageAltText = giftcardUpdate.BannerImageAltText;
            giftcard.ThumbnailImageURL = giftcardUpdate.ThumbnailImageURL;
            giftcard.ThumbnailImageAltText = giftcardUpdate.ThumbnailImageAltText;
            giftcard.ThumbnailCaption = giftcardUpdate.ThumbnailCaption;
            giftcard.ThumbnailDescription = giftcardUpdate.ThumbnailDescription;
            giftcard.Terms_Condition = giftcardUpdate.Terms_Condition;
            giftcard.Permalink = giftcardUpdate.Permalink;
            giftcard.IsPublished = giftcardUpdate.IsPublished;


            SEOTool SEOTool = null;

            if (giftcard.SEOTools != null && giftcard.SEOTools.Count() > 0)
                SEOTool = giftcard.SEOTools.First();
            else
            {
                SEOTool = new Core.Domain.SEOTool();
                giftcard.SEOTools = new List<Core.Domain.SEOTool>();
                giftcard.SEOTools.Add(SEOTool);
            }

            SEOTool.FocusKeyword = giftcardUpdate.FocusKeyword;
            SEOTool.MetaDescription = giftcardUpdate.MetaDescription;
            SEOTool.SEOTitle = giftcardUpdate.SEOTitle;

            _giftcardRepo.Update(giftcard);
        }

        public void Delete(int giftcardId)
        {

            // delete the giftcard
            _giftcardRepo.Delete(giftcardId);
        }
    }
}