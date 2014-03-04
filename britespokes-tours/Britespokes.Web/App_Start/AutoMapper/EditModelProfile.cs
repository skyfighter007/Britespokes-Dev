using System.Linq;
using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Departures;
using Britespokes.Services.Admin.GiftCards;
using Britespokes.Services.Admin.Timeline;
using Britespokes.Services.Admin.Tours;
using Britespokes.Web.Areas.Admin.Models.DiscountCodes;
using Britespokes.Web.Areas.Admin.Models.GiftCards;
using Britespokes.Web.Areas.Admin.Models.Organizations;
using Britespokes.Web.Areas.Admin.Models.Timeline;
using Britespokes.Web.Areas.Admin.Models.Tours;
using Britespokes.Web.Areas.Admin.Models.Categories;
using Britespokes.Web.Areas.Admin.Models.Perspectives;
using Britespokes.Services.Admin.Perspectives;
using Britespokes.Web.Models.Comments;
using Britespokes.Services.Categories;
using Britespokes.Web.Areas.Admin.Models.SEOToolStaticPages;
using Britespokes.Services.Admin.SEOToolStaticPages;
using Britespokes.Services.Admin.SubCategories;
using Britespokes.Web.Areas.Admin.Models.SubCategories;

namespace Britespokes.Web.App_Start.AutoMapper
{
    public class EditModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Organization, OrganizationEdit>()
              .ForMember(o => o.EmailDomainList,
                         opt => opt.MapFrom(o => string.Join(",", o.EmailDomains.Select(d => d.Domain).ToArray())))
              .ReverseMap();

            CreateMap<DiscountCode, DiscountCodeEdit>().ReverseMap();

            CreateMap<Tour, TourEdit>()
              .ForMember(t => t.HasExperience, opt => opt.MapFrom(t => t.Experiences.Any()));

            CreateMap<TourEdit, TourUpdate>();

            CreateMap<SubCategory, SubCategoryEdit>();
            CreateMap<SubCategory, SubCategoryUpdate>();
            CreateMap<SubCategoryEdit, SubCategoryUpdate>().ReverseMap();

            CreateMap<GiftCard, GiftCardEdit>();
            CreateMap<GiftCardEdit, GiftCardUpdate>();
            CreateMap<Category, CategoryEdit>().ReverseMap();
            CreateMap<CategoryUpdate, CategoryEdit>().ReverseMap();
            CreateMap<PerspectivePost, PerspectiveEdit>().ReverseMap();
            CreateMap<PerspectivePostUpdate, PerspectiveEdit>().ReverseMap();

            CreateMap<SEOToolStaticPage, SEOToolStaticPageEdit>().ReverseMap();
            CreateMap<SEOToolStaticPageUpdate, SEOToolStaticPageEdit>().ReverseMap();

            CreateMap<CommentUpdate, CommentEdit>().ReverseMap();

            CreateMap<PerspectivePost, Britespokes.Web.Models.Perspectives.PerspectiveEdit>().ReverseMap();
            CreateMap<PerspectivePostUpdate, Britespokes.Web.Models.Perspectives.PerspectiveEdit>().ReverseMap();
            

            CreateMap<TourMeta, TourMetaEdit>()
              .ForMember(t => t.TourHasExperience, opt => opt.MapFrom(t => t.Tour.Experiences.Any()));
            CreateMap<TourMetaEdit, TourMetaUpdate>();

            CreateMap<Departure, DepartureEdit>()
              .ForMember(d => d.Name, opt => opt.MapFrom(d => d.Product.Name))
              .ForMember(d => d.Description, opt => opt.MapFrom(d => d.Product.Description))
              .ForMember(d => d.AvailableOn, opt => opt.MapFrom(d => d.Product.AvailableOn))
              .ForMember(d => d.ProductVariants, opt => opt.MapFrom(d => d.Product.ProductVariants));
            CreateMap<DepartureEdit, DepartureUpdate>();

            CreateMap<ProductVariant, ProductVariantEdit>();
            CreateMap<ProductVariantEdit, ProductVariantUpdate>();

            CreateMap<TourTimelineItem, TimelineItemEdit>();
            CreateMap<TimelineItemEdit, TimelineItemUpdate>();

            base.Configure();
        }
    }
}
