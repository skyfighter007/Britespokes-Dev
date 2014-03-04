using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.GiftCards
{
    public interface IGiftCardCommandService
  {
        void Add(GiftCardUpdate giftcardUpdate);
        void Update(GiftCardUpdate giftcardUpdate);
        void Delete(int giftcardId);
  }
}