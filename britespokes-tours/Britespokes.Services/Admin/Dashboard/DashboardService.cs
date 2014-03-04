using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Dashboard
{
  public class DashboardService : IDashboardService
  {
    private readonly IRepository<Tour> _tourRepo;
    private readonly IRepository<User> _userRepo;
    private readonly IRepository<Order> _orderRepo;

    public DashboardService(IRepository<Tour> tourRepo, IRepository<User> userRepo, IRepository<Order> orderRepo)
    {
      _tourRepo = tourRepo;
      _userRepo = userRepo;
      _orderRepo = orderRepo;
    }


    public MainStats MainStats()
    {
      var mainStats = new MainStats
        {
          TourCount = _tourRepo.All.Count(),
          UserCount = _userRepo.All.Count(),
          OrderCount = _orderRepo.All.Count(),
          TotalSales = CalculateTotalSales()
        };
      return mainStats;
    }

    private decimal CalculateTotalSales()
    {
      var totalSales = _orderRepo.All.Where(o => o.OrderStatus.Name == OrderStatus.CompleteStatusName).Sum(o => (decimal?)o.Total);
      return totalSales.HasValue ? totalSales.Value : 0.0M;
    }
  }
}
