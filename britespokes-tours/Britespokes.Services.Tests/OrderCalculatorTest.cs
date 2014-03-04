using System.Collections.Generic;
using Britespokes.Core.Domain;
using Britespokes.Services.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Britespokes.Services.Tests
{
  [TestClass]
  public class OrderCalculatorTest
  {
    [TestMethod]
    public void CalculatesItemTotal()
    {
      var order = CreateOrder();
      order.ProductVariants.Add(CreateVariant("test1", 10.0m));
      order.ProductVariants.Add(CreateVariant("test2", 20.0m, 2));

      new OrderCalculator(order).Calculate();

      Assert.AreEqual(50.0m, order.ItemTotal);
    }

    [TestMethod]
    public void CalculatesAdjustmentTotal()
    {
      var order = CreateOrder();
      order.Adjustments.Add(CreateAdjustment(10.0m));
      order.Adjustments.Add(CreateAdjustment(-5.0m));
      order.Adjustments.Add(CreateAdjustment(-7.0m, false));

      new OrderCalculator(order).Calculate();

      Assert.AreEqual(5.0m, order.AdjustmentTotal);
    }

    [TestMethod]
    public void CalculatesTotal()
    {
      var order = CreateOrder();
      order.ProductVariants.Add(CreateVariant("test1", 10.0m));
      order.ProductVariants.Add(CreateVariant("test2", 20.0m, 2));
      order.Adjustments.Add(CreateAdjustment(10.0m));
      order.Adjustments.Add(CreateAdjustment(-5.0m));
      order.Adjustments.Add(CreateAdjustment(-7.0m, false));

      new OrderCalculator(order).Calculate();

      Assert.AreEqual(55.0m, order.Total);
    }


    private Order CreateOrder()
    {
      return new Order
        {
          ProductVariants = new List<OrderProductVariant>(),
          Adjustments = new List<Adjustment>()
        };
    }

    private OrderProductVariant CreateVariant(string name, decimal price, int quantity = 1)
    {
      return new OrderProductVariant
        {
          ProductVariant = new ProductVariant { Name = name },
          UnitPrice = price,
          Quantity = quantity
        };
    }

    private Adjustment CreateAdjustment(decimal amount, bool isActive = true)
    {
      return new Adjustment {Amount = amount, IsActive = isActive};
    }
  }
}
