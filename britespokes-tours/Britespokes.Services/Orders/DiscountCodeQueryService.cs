using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Orders
{
  public class DiscountCodeQueryService : IDiscountCodeQueryService
  {
    private readonly IRepository<DiscountCode> _discountCodeRepo;

    public DiscountCodeQueryService(IRepository<DiscountCode> discountCodeRepo)
    {
      _discountCodeRepo = discountCodeRepo;
    }

    public DiscountCode Find(object id)
    {
      return _discountCodeRepo.Find(id);
    }

    public DiscountCode FindByCode(string code)
    {
      return _discountCodeRepo.FindBy(dc => dc.LowerCode == code.ToLower()).SingleOrDefault();
    }
  }
}