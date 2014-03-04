using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Orders
{
  public interface IOrderReportService
  {
    byte[] Generate(IEnumerable<Order> orders);
  }
}