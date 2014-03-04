using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.GiftCards
{
    public class GiftCardQueryService : IGiftCardQueryService
    {
        private readonly IRepository<GiftCard> _giftcardRepo;

        public GiftCardQueryService(IRepository<GiftCard> giftcardRepo)
        {
            _giftcardRepo = giftcardRepo;
        }

        public IQueryable<GiftCard> GiftCards()
        {
            return _giftcardRepo.All;
        }

        public IQueryable<GiftCard> GiftCards(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return GiftCards();

            var q = search.ToLowerInvariant();
            return _giftcardRepo.FindBy(t => t.GiftCode.ToLower().Contains(q) || t.Name.ToLower().Contains(q));
        }

        public GiftCard FindGiftCard(int giftCardId)
        {
            return _giftcardRepo.Find(giftCardId);
        }

        public bool IsCodeUnique(string code, int? giftcardId = null)
        {
            var query = _giftcardRepo.FindBy(t => t.GiftCode == code);
            if (giftcardId.HasValue)
                query = query.Where(t => t.Id != giftcardId);
            return query.Any();
        }
    }
}