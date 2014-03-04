using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Orders
{
  public class OrderCalculator
  {
    private readonly Order _order;

    public OrderCalculator(Order order)
    {
      _order = order;
    }

    public void Calculate()
    {
      _order.ItemTotal = _order.ProductVariants.Sum(v => v.UnitPrice*v.Quantity);
      _order.AdjustmentTotal = CalculateAdjustmentTotal();
      _order.Total = _order.ItemTotal + _order.AdjustmentTotal;
    }

    public static void Calculate(Order order)
    {
      var c = new OrderCalculator(order);
      c.Calculate();
    }

    private decimal CalculateAdjustmentTotal()
    {
      decimal total = 0.0m;

      if (_order.Adjustments != null && _order.Adjustments.Count > 0)
        total = _order.Adjustments.Where(a => a.IsActive).Sum(a => a.Amount);

      return total;
    }
  }
}
