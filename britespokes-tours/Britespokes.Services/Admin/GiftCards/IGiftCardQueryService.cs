using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.GiftCards
{
    public interface IGiftCardQueryService
    {
        IQueryable<GiftCard> GiftCards();
        IQueryable<GiftCard> GiftCards(string search);
        GiftCard FindGiftCard(int GiftCardId);
        bool IsCodeUnique(string GiftCode, int? GiftCardId = null);
    }
}