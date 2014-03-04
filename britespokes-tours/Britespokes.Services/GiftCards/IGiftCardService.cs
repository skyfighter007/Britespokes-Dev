using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.GiftCards
{
    public interface IGiftCardService
  {
    IQueryable<GiftCard> All();
    GiftCard Find(int id);
    GiftCard FindByPermalink(string permalink);
    //GiftCard FindByCode(string code);
   // Departure FindDeparture(string code);
  }
}