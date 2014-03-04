using System;
using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Orders;
using Britespokes.Web.Helpers;

namespace Britespokes.Web.Areas.Admin.Models.Orders
{
  public class OrdersIndex
  {
    private const string AllStatusName = "All";

    public OrdersIndex()
    {
      StatusNames = OrderStatus.StatusNames.ToList();
      StatusNames.Insert(0, AllStatusName);
    }

    public IEnumerable<Order> Orders { get; set; }
    public List<string> StatusNames { get; private set; }

    public string StatusFilter { get; set; }
    public int Count { get; set; }

    public int CompletedCount { get; set; }
    public int FailedCount { get; set; }
    public int PendingCount { get; set; }
    public int TotalCount { get; set; }
    public PagingInfo PagingInfo { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public void SetCounts(IEnumerable<StatusCount> countsByStatus)
    {
      var statusCounts = countsByStatus.ToList();
      CompletedCount = GetCountForStatus("Complete", statusCounts);
      FailedCount = GetCountForStatus("Failed", statusCounts);
      PendingCount = GetCountForStatus("Pending", statusCounts);
      TotalCount = statusCounts.Sum(s => s.Count);
    }

    private static int GetCountForStatus(string name, IEnumerable<StatusCount> countsByStatus)
    {
      var count = 0;
      var statusCount = countsByStatus.FirstOrDefault(s => s.Name == name);
      if (statusCount != null) count = statusCount.Count;
      return count;
    }
  }
}