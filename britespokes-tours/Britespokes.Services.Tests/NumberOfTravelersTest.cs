using Britespokes.Core.Domain;
using Britespokes.Services.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Britespokes.Services.Tests
{
  [TestClass]
  public class NumberOfTravelersTest
  {
    [TestMethod]
    public void ComputeForOneSingle()
    {
      var num = NumberOfTravelers.Compute(CreateVariant("Single", 1));
      Assert.AreEqual(1, num);
    }

    [TestMethod]
    public void ComputeForTwoSingles()
    {
      var num = NumberOfTravelers.Compute(CreateVariant("Single", 2));
      Assert.AreEqual(2, num);
    }

    [TestMethod]
    public void ComputeForOneDouble()
    {
      var num = NumberOfTravelers.Compute(CreateVariant("Double", 1));
      Assert.AreEqual(2, num);
    }

    [TestMethod]
    public void ComputeForTwoDoubles()
    {
      var num = NumberOfTravelers.Compute(CreateVariant("Double", 2));
      Assert.AreEqual(4, num);
    }

    [TestMethod]
    public void ComputeForOneTriple()
    {
      var num = NumberOfTravelers.Compute(CreateVariant("Triple", 1));
      Assert.AreEqual(3, num);
    }

    [TestMethod]
    public void ComputeForTwoTriples()
    {
      var num = NumberOfTravelers.Compute(CreateVariant("Triple", 2));
      Assert.AreEqual(6, num);
    }

    [TestMethod]
    public void ComputeForOneQuad()
    {
      var num = NumberOfTravelers.Compute(CreateVariant("Quad", 1));
      Assert.AreEqual(4, num);
    }

    [TestMethod]
    public void ComputeForTwoQuads()
    {
      var num = NumberOfTravelers.Compute(CreateVariant("Quad", 2));
      Assert.AreEqual(8, num);
    }

    private OrderProductVariant CreateVariant(string name, int quantity)
    {
      return new OrderProductVariant
        {
          ProductVariant = new ProductVariant { Name = name },
          Quantity = quantity
        };
    }
  }
}
