using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Britespokes.Services.Tests
{
  [TestClass]
  public class OrderReportServiceTest
  {
    private string _report;
    private string[] _reportLines;

    public OrderReportServiceTest()
    {
      _report = GetReport();
      _reportLines = GetReportLines();
    }

    [TestMethod]
    public void ReturnsALinePerOrder()
    {
      Assert.AreEqual(SampleOrders().Count() + 1, _reportLines.Length);
    }

    [TestMethod]
    public void IncludesOrderId()
    {
      Assert.IsTrue(_reportLines [ 1 ].StartsWith("1,"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesOrderStatus()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",Complete"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesCompletedAt()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",08/30/2013 01:34 PM"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesBlankCompletedAt()
    {
      Assert.IsTrue(_reportLines [ 2 ].Contains(",n/a"), _reportLines [ 2 ]);
    }

    [TestMethod]
    public void IncludesUpdatedAt()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",08/29/2013 02:24 PM"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesOrderNumber()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",xxff"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesCustomerName()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",John,Doe"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesCustomerEmail()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",johndoe@example.com"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesSpecialInstructions()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",instructions"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesTourName()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",Cover your Bases"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesTourCode()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",CYB4NY"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesDepartureCode()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",CYB4NY01"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesTravelerCount()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",2"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesOrderTotal()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",$100.00"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesItemTotal()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",$120.00"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesAdjustmentTotal()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",($20.00)"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesDiscountCode()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",\"disc1, disc2\""), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesChargeId()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",5422136378"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesProductVariantInfo()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",\"Single:1, Double:1\""), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesBillingFirstName()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",Jonathan Red"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesBillingLastName()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",Doe-Red"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesBillingAddress1()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",123 Main St"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesBillingAddress2()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",Apt 1507"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesBillingCity()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",New York"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesBillingState()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",NY"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesBillingZip()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",11201"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesDepartureDate()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",09/05/2013"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesTravelerIds()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",1001*1002"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesTravelerNames()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",Arch Stanton*Bill Carson"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesTravelerEmails()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",arch@example.com*billc@example.com"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesTravelerBirthdates()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains("01/05/1974*08/03/1994"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesTravelerPhoneNumbers()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",5555555555*"), _reportLines [ 1 ]);
    }

    [TestMethod]
    public void IncludesTravelerSpecialInstructions()
    {
      Assert.IsTrue(_reportLines [ 1 ].Contains(",*Dietary Restrictions"), _reportLines [ 1 ]);
    }

    private static string GetReport()
    {
      var orders = SampleOrders();
      var orderQueryService = new Mock<IOrderQueryService>();
      orderQueryService.Setup(q => q.OrderSummary(It.IsAny<Order>()))
        .Returns(SampleOrderSummaries(orders).FirstOrDefault());
      var svc = new OrderReportService(orderQueryService.Object);
      return Encoding.UTF8.GetString(svc.Generate(SampleOrders()));
    }

    private string[] GetReportLines()
    {
      if (_reportLines != null) return _reportLines;
      if (_report == null)
        _report = GetReport();
      _reportLines = _report.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
      return _reportLines;
    }

    private static IEnumerable<OrderSummary> SampleOrderSummaries(IEnumerable<Order> orders)
    {
      return orders.Select(order => new OrderSummary
      {
        Id = order.Id,
        Order = order,
        Tours = new[] { new Tour { Name = "Cover your Bases", Code = "CYB4NY" } },
        Departures = new[] { new Departure { Code = "CYB4NY01", DepartureDate = new DateTime(2013, 9, 5)} },
        DiscountCodes = new[] { new DiscountCode { Code = "disc1" }, new DiscountCode { Code = "disc2"} },
        OrderVariants = new Dictionary<string, int> { { "Single", 1 }, { "Double", 1 }}
      }).ToList();
    }

    private static IEnumerable<Order> SampleOrders()
    {
      return new List<Order>
      {
        new Order
        {
          Id = 1,
          OrderNumber = "xxff",
          OrderStatus = new OrderStatus { Name = "Complete" },
          UpdatedAt = new DateTime(2013, 8, 29, 14, 24, 0),
          CompletedAt = new DateTime(2013, 8, 30, 13, 34, 0),
          ItemTotal = 120.0m,
          AdjustmentTotal = -20.0m,
          Total = 100.0m,
          SpecialInstructions = "instructions",
          ChargeId = "5422136378",
          User = new User
          {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com"
          },
          BillingAddress = new Address
          {
            FirstName = "Jonathan Red",
            LastName = "Doe-Red",
            Address1 = "123 Main St",
            Address2 = "Apt 1507",
            City = "New York",
            StateOrProvince = "NY",
            ZipCode = "11201"
          },
          Travelers = new List<Traveler>
          {
            new Traveler
            {
              Id = 1001,
              FirstName = "Arch",
              LastName = "Stanton",
              Email = "arch@example.com",
              PhoneNumber = "5555555555",
              Birthday = new DateTime(1974, 01, 05),
              SpecialInstructions = null,
            },
            new Traveler
            {
              Id = 1002,
              FirstName = "Bill",
              LastName = "Carson",
              Email = "billc@example.com",
              Birthday = new DateTime(1994, 08, 03),
              SpecialInstructions = "Dietary Restrictions"
            }
          },
        },
        new Order
        {
          Id = 2,
          OrderNumber = "kljd",
          OrderStatus = new OrderStatus { Name = "Canceled" },
          ItemTotal = 120.0m,
          AdjustmentTotal = -20.0m,
          Total = 200.0m,
          SpecialInstructions = "more instructions",
          User = new User
          {
            FirstName = "Jane",
            LastName= "Doe",
            Email = "jane@example.com"
          },
          BillingAddress = new Address
          {
            FirstName = "Jane Red",
            LastName = "Red Doe",
            Address1 = "123 Main St",
            Address2 = "Apt 1507",
            City = "New York",
            StateOrProvince = "NY",
            ZipCode = "11201"
          },
          Travelers = new List<Traveler>
          {
            new Traveler
            {
              Id = 2001,
              FirstName = "Jane",
              LastName = "Doe",
              Email = "jane@eschland.com",
              Birthday = DateTime.Now.AddYears(-19),
              PhoneNumber = "5555555555",
              SpecialInstructions = "Dietary Restrictions"
            }
          }
        },
      };
    }
  }
}