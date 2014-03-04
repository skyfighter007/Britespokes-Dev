using Britespokes.Core.Domain;

namespace Britespokes.Services.Orders
{
  public interface IDiscountCodeQueryService
  {
    DiscountCode Find(object id);
    DiscountCode FindByCode(string code);
  }
}