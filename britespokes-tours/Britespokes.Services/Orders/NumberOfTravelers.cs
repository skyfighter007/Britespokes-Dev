using Britespokes.Core.Domain;

namespace Britespokes.Services.Orders
{
  public static class NumberOfTravelers
  {
    public static int Compute(OrderProductVariant orderVariant)
    {
      var numberOfTravelers = 0;
      switch (orderVariant.ProductVariant.Name)
      {
        case "Single":
          numberOfTravelers = 1;
          break;
        case "Double":
          numberOfTravelers = 2;
          break;
        case "Triple":
          numberOfTravelers = 3;
          break;
        case "Quad":
          numberOfTravelers = 4;
          break;
      }
      return numberOfTravelers * orderVariant.Quantity;
    }
  }
}
