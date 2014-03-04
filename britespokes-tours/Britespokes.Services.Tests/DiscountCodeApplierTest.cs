using System;
using Britespokes.Core.Domain;
using Britespokes.Services.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Britespokes.Services.Tests
{
  [TestClass]
  public class DiscountCodeApplierTest
  {
    [TestMethod]
    public void AppliesAdjustment()
    {
      const decimal orderTotal = 100.0m;
      var applier = new DiscountCodeApplier(DiscountCode(10.0m));
      var adjustment = applier.DiscountAdjustment(orderTotal);
      Assert.IsNotNull(adjustment);
      Assert.AreEqual(-10.0m, adjustment.Amount);
    }

    [TestMethod]
    public void CalculatesDiscountTotal()
    {
      const decimal orderTotal = 100.0m;
      var applier = new DiscountCodeApplier(DiscountCode(10.0m));
      Assert.AreEqual(90.0m, applier.DiscountedTotal(orderTotal));
    }

    [TestMethod]
    public void CalculatesDiscountTotalFromPercentage()
    {
      const decimal orderTotal = 150.0m;
      var applier = new DiscountCodeApplier(PercentageDiscountCode(20.0m));
      Assert.AreEqual(120.0m, applier.DiscountedTotal(orderTotal));
    }

    [TestMethod]
    public void DiscountedTotalCannotBeLessThanZero()
    {
      const decimal orderTotal = 100.0m;
      var applier = new DiscountCodeApplier(DiscountCode(110.0m));
      Assert.AreEqual(0.0m, applier.DiscountedTotal(orderTotal));
    }

    [TestMethod]
    public void DiscountedTotalFromPercentageCannotBeLessThanZero()
    {
      const decimal orderTotal = 100.0m;
      var applier = new DiscountCodeApplier(PercentageDiscountCode(110.0m));
      Assert.AreEqual(0.0m, applier.DiscountedTotal(orderTotal));
    }

    [TestMethod]
    public void IgnoresInactiveDiscountCode()
    {
      const decimal orderTotal = 100.0m;
      var applier = new DiscountCodeApplier(DiscountCode(10.0m, false));
      Assert.AreEqual(100.0m, applier.DiscountedTotal(orderTotal));
    }

    [TestMethod]
    public void IgnoresExpiredDiscountCode()
    {
      const decimal orderTotal = 100.0m;
      var applier = new DiscountCodeApplier(ExpiredDiscountCode(10.0m));
      Assert.AreEqual(100.0m, applier.DiscountedTotal(orderTotal));
    }

    [TestMethod]
    public void IgnoresUpcomingDiscountCode()
    {
      const decimal orderTotal = 100.0m;
      var applier = new DiscountCodeApplier(UpcomingDiscountCode(10.0m));
      Assert.AreEqual(100.0m, applier.DiscountedTotal(orderTotal));
    }

    private DiscountCode DiscountCode(decimal amount, bool isActive = true)
    {
      return new DiscountCode
      {
        Amount = amount,
        IsActive = isActive,
        StartsAt = DateTime.UtcNow.AddDays(-5),
        ExpiresAt = DateTime.UtcNow.AddDays(5)
      };
    }

    private DiscountCode PercentageDiscountCode(decimal percentage, bool isActive = true)
    {
      return new DiscountCode
      {
        Percentage = percentage,
        UsePercentage = true,
        IsActive = isActive,
        StartsAt = DateTime.UtcNow.AddDays(-5),
        ExpiresAt = DateTime.UtcNow.AddDays(5)
      };
    }

    private DiscountCode ExpiredDiscountCode(decimal amount)
    {
      var discountCode = DiscountCode(amount);
      discountCode.ExpiresAt = DateTime.UtcNow.AddDays(-1);
      return discountCode;
    }

    private DiscountCode UpcomingDiscountCode(decimal amount)
    {
      var discountCode = DiscountCode(amount);
      discountCode.StartsAt = DateTime.UtcNow.AddDays(5);
      discountCode.ExpiresAt = DateTime.UtcNow.AddDays(10);
      return discountCode;
    }
  }
}