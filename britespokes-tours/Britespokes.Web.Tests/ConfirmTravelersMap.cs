using System;
using System.Collections.Generic;
using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Orders;
using Britespokes.Web.App_Start.AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Britespokes.Web.Tests
{
  [TestClass]
  public class ConfirmTravelersMap
  {
    public ConfirmTravelersMap()
    {
      AutoMapperConfiguration.Configure();
    }

    [TestMethod]
    public void ShouldMapOrderId()
    {
      var order = ExampleOrder();
      var confirmTravelers = Mapper.Map<ConfirmTravelers>(order);
      Assert.AreEqual(order.Id, confirmTravelers.OrderId);
    }

    [TestMethod]
    public void ShouldMapTravelers()
    {
      var order = ExampleOrder();
      var confirmTravelers = Mapper.Map<ConfirmTravelers>(order);
      Assert.AreEqual(order.Travelers.Count, confirmTravelers.Travelers.Count);
    }

    [TestMethod]
    public void ShouldMapFirstTraveler()
    {
      var order = ExampleOrder();
      var confirmTravelers = Mapper.Map<ConfirmTravelers>(order);

      Assert.AreEqual(
        order.Travelers[0].Id,
        confirmTravelers.Travelers[0].Id);
      Assert.AreEqual(
        order.Travelers[0].OrderId,
        confirmTravelers.Travelers[0].OrderId);
      Assert.AreEqual(
        order.Travelers[0].FirstName,
        confirmTravelers.Travelers[0].FirstName);
      Assert.AreEqual(
        order.Travelers[0].LastName,
        confirmTravelers.Travelers[0].LastName);
      Assert.AreEqual(
        order.Travelers[0].Email,
        confirmTravelers.Travelers[0].Email);
      Assert.AreEqual(
        order.Travelers[0].EmailItinerary,
        confirmTravelers.Travelers[0].EmailItinerary);
      Assert.AreEqual(
        order.Travelers[0].PhoneNumber,
        confirmTravelers.Travelers[0].PhoneNumber);
      Assert.AreEqual(
        order.Travelers[0].Birthday,
        confirmTravelers.Travelers[0].Birthday);
      Assert.AreEqual(
        order.Travelers[0].SpecialInstructions,
        confirmTravelers.Travelers[0].SpecialInstructions);
    }

    private Order ExampleOrder()
    {
      return new Order
        {
          Id = 1,
          Travelers = new List<Traveler>
            {
              new Traveler
                {
                  EmailItinerary = false,
                  SpecialInstructions = "none",
                  FirstName = "Joe",
                  LastName = "Doe",
                  Birthday = new DateTime(1990, 4, 22),
                  Email = "joe@example.com",
                  PhoneNumber = "5551239876"
                },
              new Traveler
                {
                  EmailItinerary = false,
                  SpecialInstructions = "none",
                  FirstName = "Jane",
                  LastName = "Doe",
                  Email = "jane@example.com",
                  PhoneNumber = "5551239876"
                }
            }
        };
    }
  }
}