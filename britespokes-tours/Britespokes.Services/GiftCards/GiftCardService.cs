using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.GiftCards
{
  public class GiftCardService : IGiftCardService
  {
   private readonly IRepository<GiftCard> _giftCardRepository;
    

    public GiftCardService(IRepository<GiftCard> giftCardRepository)
    {
        _giftCardRepository = giftCardRepository;
  
    }

    public IQueryable<GiftCard> All()
    {
        return _giftCardRepository.All.Where(a => a.IsPublished == true);
    }

    public GiftCard Find(int id)
    {
        return _giftCardRepository.Find(id);
    }

    public GiftCard FindByPermalink(string permalink)
    {
        return _giftCardRepository.FindBy(t => t.Permalink == permalink).SingleOrDefault();
    }

    //public Tour FindByCode(string code)
    //{
    //    return _giftCardRepository.FindBy(t => t.Code == code).SingleOrDefault();
    //}

   
  }
}
