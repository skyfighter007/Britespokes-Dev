using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using AuthorizeNet.APICore;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.DiscountCodes
{
  public class DiscountCodeService : IDiscountCodeService
  {
    private readonly IRepository<DiscountCode> _discountCodeRepo;
    private readonly IRepository<Order> _orderRepo;
    private readonly IRepository<DiscountAdjustment> _adjustmentRepo;

    public DiscountCodeService(IRepository<DiscountCode> discountCodeRepo, IRepository<Order> orderRepo, IRepository<DiscountAdjustment> adjustmentRepo)
    {
      _discountCodeRepo = discountCodeRepo;
      _orderRepo = orderRepo;
      _adjustmentRepo = adjustmentRepo;
    }

    public DiscountCode FindDiscountCode(int discountCodeId)
    {
      return _discountCodeRepo.Find(discountCodeId);
    }

    public void Update(DiscountCode discountCode)
    {
      _discountCodeRepo.Update(discountCode);
    }

    public void Remove(int discountCodeId)
    {
      _discountCodeRepo.Delete(discountCodeId);
    }

    public void Add(DiscountCode discountCode)
    {
      _discountCodeRepo.Add(discountCode);
    }

    public IQueryable<DiscountCode> All()
    {
      return _discountCodeRepo.All;
    }

    public Dictionary<int, int> UsageCounts()
    {
      var results = _discountCodeRepo.All
        .Join(_adjustmentRepo.All, d => d.Id, a => a.DiscountCodeId, (d, a) => new { d, a })
        .Join(_orderRepo.All, t => t.a.OrderId, o => o.Id, (t, o) => new { t.d, t.a, o })
        .Where(t => t.o.OrderStatus.Name == OrderStatus.CompleteStatusName)
        .GroupBy(t => t.d.Id)
        .Select(g => new { Id = g.Key, Count = g.Distinct().Count() });

      return results.ToDictionary(result => result.Id, result => result.Count);
    }
  }
}