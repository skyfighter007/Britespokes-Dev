using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.DiscountCodes
{
  public interface IDiscountCodeService
  {
    IQueryable<DiscountCode> All();
    Dictionary<int, int> UsageCounts();
    DiscountCode FindDiscountCode(int discountCodeId);
    void Update(DiscountCode discountCode);
    void Remove(int discountCodeId);
    void Add(DiscountCode discountCode);
  }
}