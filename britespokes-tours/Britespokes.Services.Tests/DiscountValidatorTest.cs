using System.Collections.Generic;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using Britespokes.Services.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Britespokes.Services.Tests
{
  [TestClass]
  public class DiscountCodeValidatorTest
  {
    [TestMethod]
    public void DiscountCodeNotFoundIsInvalid()
    {
      var result = MockedValidator(null).Validate("test", null);
      Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    public void DiscountCodeNotFoundReturnsInvalidError()
    {
      var result = MockedValidator(null).Validate("test", null);
      Assert.AreEqual("Invalid discount code.", result.ErrorMessage);
    }

    [TestMethod]
    public void GlobalDiscountCodeIsValid()
    {
      var dc = GlobalDiscountCode();
      var result = MockedValidator(dc).Validate(dc.Code, null);
      Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void NoVariantssForNonGlobalDiscountCodeIsInvalid()
    {
      var dc = DiscountCode();
      var result = MockedValidator(dc).Validate(dc.Code, null);
      Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    public void NoVariantssForNonGlobalDiscountCodeReturnsNotAvailableError()
    {
      var dc = DiscountCode();
      var result = MockedValidator(dc).Validate(dc.Code, null);
      Assert.IsTrue(result.ErrorMessage.Contains("not available"));
    }

    [TestMethod]
    public void VariantRestrictedDiscountCodeIsValidForIncludedVariants()
    {
      var dc = VariantRestrictedDiscountCode(new [] { 1, 2 });
      var result = MockedValidator(dc).Validate(dc.Code, new[] { 2 });
      Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void VariantRestrictedDiscountCodeIsInvalidForExcludedVariants()
    {
      var dc = VariantRestrictedDiscountCode(new [] { 1, 2 });
      var result = MockedValidator(dc).Validate(dc.Code, new[] { 3 });
      Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    public void VariantRestrictedDiscountCodeReturnsNotApplicableMessageForExcludedVariants()
    {
      var dc = VariantRestrictedDiscountCode(new [] { 1, 2 });
      var result = MockedValidator(dc).Validate(dc.Code, new[] { 3 });
      Assert.IsTrue(result.ErrorMessage.Contains("not applicable"));
    }

    [TestMethod]
    public void ProductRestrictedDiscountCodeIsValidForIncludedVariants()
    {
      var variantRepo = new Mock<IRepository<ProductVariant>>();
      variantRepo.Setup(r => r.Find(2)).Returns(new ProductVariant { ProductId = 100 });

      var dc = ProductRestrictedDiscountCode(new[] { 100, 200 });
      var result = MockedValidator(dc, variantRepo.Object).Validate(dc.Code, new[] { 2 });
      Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void ProductRestrictedDiscountCodeIsInvalidForExcludedVariants()
    {
      var variantRepo = new Mock<IRepository<ProductVariant>>();
      variantRepo.Setup(r => r.Find(2)).Returns(new ProductVariant { ProductId = 101 });

      var dc = ProductRestrictedDiscountCode(new [] { 100, 200 });
      var result = MockedValidator(dc).Validate(dc.Code, new[] { 2 });
      Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    public void ProductRestrictedDiscountCodeReturnsNotApplicableMessageForExcludedVariants()
    {
      var variantRepo = new Mock<IRepository<ProductVariant>>();
      variantRepo.Setup(r => r.Find(2)).Returns(new ProductVariant { ProductId = 101 });

      var dc = ProductRestrictedDiscountCode(new[] { 100, 200 });
      var result = MockedValidator(dc, variantRepo.Object).Validate(dc.Code, new[] { 2 });
      Assert.IsTrue(result.ErrorMessage.Contains("not applicable"));
    }

    [TestMethod]
    public void TourRestrictedDiscountCodeIsValidForIncludedVariants()
    {
      var variantRepo = new Mock<IRepository<ProductVariant>>();
      variantRepo.Setup(r => r.Find(2)).Returns(new ProductVariant
      {
        Product = new Product
        {
          Departure = new Departure
          {
            TourId = 1000
          }
        }
      });

      var dc = TourRestrictedDiscountCode(new[] { 1000, 2000 });
      var result = MockedValidator(dc, variantRepo.Object).Validate(dc.Code, new[] { 2 });
      Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void TourRestrictedDiscountCodeIsInvalidForExcludedVariants()
    {
      var variantRepo = new Mock<IRepository<ProductVariant>>();
      variantRepo.Setup(r => r.Find(2)).Returns(new ProductVariant
      {
        Product = new Product
        {
          Departure = new Departure
          {
            TourId = 1001
          }
        }
      });

      var dc = TourRestrictedDiscountCode(new [] { 1000, 2000 });
      var result = MockedValidator(dc).Validate(dc.Code, new[] { 2 });
      Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    public void TourRestrictedDiscountCodeReturnsNotApplicableMessageForExcludedVariants()
    {
      var variantRepo = new Mock<IRepository<ProductVariant>>();
      variantRepo.Setup(r => r.Find(2)).Returns(new ProductVariant
      {
        Product = new Product
        {
          Departure = new Departure
          {
            TourId = 1001
          }
        }
      });

      var dc = TourRestrictedDiscountCode(new[] { 1000, 2000 });
      var result = MockedValidator(dc, variantRepo.Object).Validate(dc.Code, new[] { 2 });
      Assert.IsTrue(result.ErrorMessage.Contains("not applicable"));
    }

    private DiscountCodeValidator MockedValidator(DiscountCode discountCode,
      IRepository<ProductVariant> variantRepo)
    {
      var code = discountCode == null ? "invalid" : discountCode.Code;
      var service = new Mock<IDiscountCodeQueryService>();
      service.Setup(r => r.FindByCode(code)).Returns(discountCode);

      return new DiscountCodeValidator(service.Object, variantRepo);
    }

    private DiscountCodeValidator MockedValidator(DiscountCode discountCode)
    {
      var variantRepo = new Mock<IRepository<ProductVariant>>();
      variantRepo.Setup(r => r.Find(It.IsAny<int>())).Returns<ProductVariant>(null);

      return MockedValidator(discountCode, variantRepo.Object);
    }

    private DiscountCode DiscountCode()
    {
      return new DiscountCode
      {
        Code = "dc1",
        LowerCode = "dc1",
        IsGlobal = false
      };
    }

    private DiscountCode GlobalDiscountCode()
    {
      return new DiscountCode
      {
        Code = "global",
        LowerCode = "global",
        IsGlobal = true
      };
    }

    private DiscountCode VariantRestrictedDiscountCode(IEnumerable<int> variantIds)
    {
      var dc = DiscountCode();

      dc.ProductVariants = new List<ProductVariant>();
      foreach (var variantId in variantIds)
        dc.ProductVariants.Add(new ProductVariant { Id = variantId });

      return dc;
    }

    private DiscountCode ProductRestrictedDiscountCode(IEnumerable<int> productIds)
    {
      var dc = DiscountCode();

      dc.Products = new List<Product>();
      foreach (var productId in productIds)
        dc.Products.Add(new Product { Id = productId });

      return dc;
    }

    private DiscountCode TourRestrictedDiscountCode(IEnumerable<int> tourIds)
    {
      var dc = DiscountCode();

      dc.Tours = new List<Tour>();
      foreach (var tourId in tourIds)
        dc.Tours.Add(new Tour { Id = tourId });

      return dc;
    }
  }
}