using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using Britespokes.Core.Domain;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Britespokes.Services.Admin.Orders
{
  public class OrderReportService : IOrderReportService
  {
    private readonly IOrderQueryService _orderQueryService;
    private readonly CsvConfiguration _configuration;

    public OrderReportService(IOrderQueryService orderQueryService)
    {
      _orderQueryService = orderQueryService;
      _configuration = new CsvConfiguration { Encoding = Encoding.UTF8 };
      _configuration.RegisterClassMap(new OrderReportItemMap());

      CreateAutoMapperMap();
    }

    public byte[] Generate(IEnumerable<Order> orders)
    {
      using (var mem = new MemoryStream())
      using (var writer = new StreamWriter(mem))
      using (var csv = new CsvWriter(writer, _configuration))
      {
        csv.WriteRecords(GetReportItems(orders));
        writer.Flush();
        return mem.ToArray();
      }
    }

    private IEnumerable<OrderReportItem> GetReportItems(IEnumerable<Order> orders)
    {
      var reportItems = new List<OrderReportItem>();

      foreach (var order in orders)
      {
        var reportItem = Mapper.Map<OrderReportItem>(order);

        var travelers = order.Travelers;
        reportItem.TravelerIds = string.Join("*", travelers.Select(t => t.Id));
        reportItem.TravelerNames = string.Join("*", travelers.Select(t => t.FullName));
        reportItem.TravelerBirthdates = string.Join("*",
          travelers.Select(t => (t.Birthday.HasValue ? t.Birthday.Value.ToString("MM/dd/yyyy") : "")));
        reportItem.TravelerPhoneNumbers = string.Join("*", travelers.Select(t => t.PhoneNumber));
        reportItem.TravelerEmails = string.Join("*", travelers.Select(t => t.Email));
        reportItem.TravelerSpecialInstructions = string.Join("*", travelers.Select(t => t.SpecialInstructions));

        var orderSummary = _orderQueryService.OrderSummary(order);
        var tour = orderSummary.Tours.FirstOrDefault();
        var departure = orderSummary.Departures.FirstOrDefault();
        if (tour != null)
        {
          reportItem.TourCode = tour.Code;
          reportItem.TourName = tour.Name;
        }
        if (departure != null)
        {
          reportItem.DepartureCode = departure.Code;
          reportItem.DepartureDate = departure.DepartureDate;
        }

        var products =
          orderSummary.OrderVariants.Select(
            orderProductVariant => string.Format("{0}:{1}", orderProductVariant.Key, orderProductVariant.Value))
            .ToList();
        reportItem.Products = string.Join(", ", products);

        if (orderSummary.DiscountCodes != null)
        {
          var discountCodes = orderSummary.DiscountCodes;
          reportItem.DiscountCode = string.Join(", ", discountCodes.Select(d => d.Code));
        }

        reportItems.Add(reportItem);
      }

      return reportItems;
    }

    private static void CreateAutoMapperMap()
    {
      Mapper.CreateMap<Order, OrderReportItem>()
        .ForMember(r => r.TravelerCount, opt => opt.MapFrom(o => o.Travelers.Count))
        .ForMember(r => r.FirstName, opt => opt.MapFrom(o => o.User.FirstName))
        .ForMember(r => r.LastName, opt => opt.MapFrom(o => o.User.LastName))
    
        .ForMember(r => r.Email, opt => opt.MapFrom(o => o.User.Email))
        .ForMember(r => r.BillingFirstName, opt => opt.MapFrom(o => o.BillingAddress.FirstName))
        .ForMember(r => r.BillingLastName, opt => opt.MapFrom(o => o.BillingAddress.LastName))
        .ForMember(r => r.BillingAddress1, opt => opt.MapFrom(o => o.BillingAddress.Address1))
        .ForMember(r => r.BillingAddress2, opt => opt.MapFrom(o => o.BillingAddress.Address2))
        .ForMember(r => r.BillingCity, opt => opt.MapFrom(o => o.BillingAddress.City))
        .ForMember(r => r.BillingState, opt => opt.MapFrom(o => o.BillingAddress.StateOrProvince))
        .ForMember(r => r.BillingZipCode, opt => opt.MapFrom(o => o.BillingAddress.ZipCode));
    }

    private class OrderReportItemMap : CsvClassMap<OrderReportItem>
    {
      public override void CreateMap()
      {
        Map(o => o.Id).Name("OrderId");
        Map(o => o.OrderStatusName).Name("Status");
        Map(o => o.UpdatedAt).Name("Updated Date").TypeConverter<CsvDateTimeTypeConverter>();
        Map(o => o.CompletedAt).Name("Completed Date").TypeConverter<CsvDateTimeTypeConverter>();
        Map(o => o.OrderNumber);
        Map(o => o.SpecialInstructions);

        Map(o => o.TourCode);
        Map(o => o.TourName);
        Map(o => o.DepartureCode);
        Map(o => o.DepartureDate).TypeConverterOption("MM/dd/yyyy");
        Map(o => o.Products);

        Map(o => o.TravelerCount);
        Map(o => o.ItemTotal).TypeConverterOption("C");
        Map(o => o.AdjustmentTotal).TypeConverterOption("C");
        Map(o => o.Total).TypeConverterOption("C");
        Map(o => o.ChargeId);
        Map(o => o.DiscountCode);

        Map(o => o.FirstName);
        Map(o => o.LastName);
        Map(o => o.Email);
        Map(o => o.IATA);
        Map(o => o.Affiliation);

        Map(o => o.BillingFirstName);
        Map(o => o.BillingLastName);
        Map(o => o.BillingAddress1);
        Map(o => o.BillingAddress2);
        Map(o => o.BillingCity);
        Map(o => o.BillingState);
        Map(o => o.BillingZipCode);

        Map(o => o.TravelerIds);
        Map(o => o.TravelerNames);
        Map(o => o.TravelerBirthdates);
        Map(o => o.TravelerPhoneNumbers);
        Map(o => o.TravelerEmails);
        Map(o => o.TravelerSpecialInstructions);
      }
    }
  }

  internal class CsvDateTimeTypeConverter : ITypeConverter
  {
    public string ConvertToString(TypeConverterOptions options, object value)
    {
      var dt = value as DateTime?;
      return dt == null || dt.Value == DateTime.MinValue ? "n/a" : dt.Value.ToString("MM/dd/yyyy hh:mm tt");
    }

    public object ConvertFromString(TypeConverterOptions options, string text)
    {
      return DateTime.Parse(text);
    }

    public bool CanConvertFrom(Type type)
    {
      return (type == typeof (DateTime) || type == typeof (DateTime?));
    }

    public bool CanConvertTo(Type type)
    {
      return (type == typeof (string));
    }
  }
}