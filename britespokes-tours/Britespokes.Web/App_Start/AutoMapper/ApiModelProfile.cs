using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Web.Areas.Api.Models.Departures;
using Britespokes.Web.Areas.Api.Models.Products;
using Britespokes.Web.Areas.Api.Models.Tours;

namespace Britespokes.Web.App_Start.AutoMapper
{
  public class ApiModelProfile : Profile
  {
    protected override void Configure()
    {
      Mapper.CreateMap<Tour, ApiTour>();
      Mapper.CreateMap<Departure, ApiDeparture>()
        .ForMember(da => da.ProductId, opt => opt.MapFrom(d => d.Product.Id))
        .ForMember(da => da.Name, opt => opt.MapFrom(d => d.Product.Name));
      Mapper.CreateMap<ProductVariant, ApiProductVariant>()
        .ForMember(pv => pv.Code, opt => opt.MapFrom(pv => pv.Product.Departure.Code));

      base.Configure();
    }
  }
}