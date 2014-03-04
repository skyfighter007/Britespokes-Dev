using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.GiftCards;
using Britespokes.Services.Orders;
using Britespokes.Services.Users;
using Britespokes.Web.Areas.Admin.Models.DiscountCodes;
using Britespokes.Web.Areas.Admin.Models.Experiences;
using Britespokes.Web.Areas.Admin.Models.Timeline;
using Britespokes.Web.Models.GiftCards;
using Britespokes.Web.Models.Tours;
using Britespokes.Web.Models.Users;

namespace Britespokes.Web.App_Start.AutoMapper
{
  public class ViewModelProfile : Profile
  {
    protected override void Configure()
    {
      Mapper.CreateMap<Order, ConfirmTravelers>()
            .ForMember(d => d.OrderId, s => s.MapFrom(o => o.Id))
            .ReverseMap();
      Mapper.CreateMap<Traveler, TravelerDetails>().ReverseMap();

      Mapper.CreateMap<Tour, TourShow>()
        .ForMember(t => t.Description, opt => opt.MapFrom(t => t.TourMeta.Description))
        .ForMember(t => t.BannerImageUrl, opt => opt.MapFrom(t => t.TourMeta.BannerImageUrl))
        .ForMember(t => t.PriceIncludesHeader, opt => opt.MapFrom(t => t.TourMeta.PriceIncludesHeader))
        .ForMember(t => t.PriceIncludes1, opt => opt.MapFrom(t => t.TourMeta.PriceIncludes1))
        .ForMember(t => t.PriceIncludes2, opt => opt.MapFrom(t => t.TourMeta.PriceIncludes2))
        .ForMember(t => t.PriceIncludesHeader2, opt => opt.MapFrom(t => t.TourMeta.PriceIncludesHeader2))
        .ForMember(t => t.PriceIncludes3, opt => opt.MapFrom(t => t.TourMeta.PriceIncludes3))
        .ForMember(t => t.PriceIncludes4, opt => opt.MapFrom(t => t.TourMeta.PriceIncludes4))
        .ForMember(t => t.briterPriceIncludesHeader1, opt => opt.MapFrom(t => t.TourMeta.BriterPriceIncludesHeader1))
        .ForMember(t => t.briterPriceIncludes1, opt => opt.MapFrom(t => t.TourMeta.BriterPriceIncludes1))
        .ForMember(t => t.briterPriceIncludes2, opt => opt.MapFrom(t => t.TourMeta.BriterPriceIncludes2))
        .ForMember(t => t.briterPriceIncludesHeader2, opt => opt.MapFrom(t => t.TourMeta.BriterPriceIncludesHeader2))
        .ForMember(t => t.briterPriceIncludes3, opt => opt.MapFrom(t => t.TourMeta.BriterPriceIncludes3))
        .ForMember(t => t.briterPriceIncludes4, opt => opt.MapFrom(t => t.TourMeta.BriterPriceIncludes4))
        .ForMember(t => t.AdditionalInformationHeader, opt => opt.MapFrom(t => t.TourMeta.AdditionalInformationHeader))
        .ForMember(t => t.AdditionalInformation1, opt => opt.MapFrom(t => t.TourMeta.AdditionalInformation1))
        .ForMember(t => t.BannerImageAltText, opt => opt.MapFrom(t => t.TourMeta.BannerImageAltText))
        .ForMember(t => t.AdditionalInformation2, opt => opt.MapFrom(t => t.TourMeta.AdditionalInformation2));

      Mapper.CreateMap<DiscountCode, DiscountCodeIndex>();

      Mapper.CreateMap<Experience, ExperiencesIndexItem>()
        .ForMember(e => e.Permalink, opt => opt.MapFrom(e => e.Tour.Permalink))
        .ForMember(e => e.ThumbnailImageUrl, opt => opt.MapFrom(e => e.Tour.TourMeta.ThumbnailImageUrl))
         .ForMember(e => e.ThumbnailImageAltText, opt => opt.MapFrom(e => e.Tour.TourMeta.ThumbnailImageAltText))
        .ForMember(e => e.ThumbnailCaption, opt => opt.MapFrom(e => e.Tour.TourMeta.ThumbnailCaption))
        .ForMember(e => e.JourneyDescription, opt => opt.MapFrom(e => e.Tour.TourMeta.JourneyDescription));
      Mapper.CreateMap<ExperiencePosition, Experience>();

      Mapper.CreateMap<TourTimelineItemPosition, TourTimelineItem>();

      Mapper.CreateMap<GiftCard, GiftCardShow>();
    Mapper.CreateMap<GiftOrder, ConfirmGiftCards>()
          .ForMember(d => d.GiftOrderId, s => s.MapFrom(o => o.Id))
          .ForMember(d => d.GiftOrderQuantity, s => s.MapFrom(o => o.Quantity))
          .ForMember(d => d.GiftCardName, s => s.MapFrom(o => o.GiftCard.Name))
          .ForMember(d => d.GiftCardDescription, s => s.MapFrom(o => o.GiftCard.GiftDescription))
          .ReverseMap();
      Mapper.CreateMap<GiftOrderDetail, GiftOrderDetails>().ReverseMap();

      Mapper.CreateMap<User, UpdateUserInput>().ReverseMap();
      base.Configure();
    }
  }
}