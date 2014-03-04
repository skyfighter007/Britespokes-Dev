using AutoMapper;
using Britespokes.Services.Admin.Departures;
using Britespokes.Services.Admin.Products;
using Britespokes.Web.Areas.Api.Models.Departures;
using Britespokes.Web.Areas.Api.Models.Products;

namespace Britespokes.Web.Areas.Api.Controllers
{
  public class ProductVariantsController : ApiBaseController
  {
    private readonly IProductQueryService _productQueryService;

    public ProductVariantsController(IProductQueryService productQueryService)
    {
      _productQueryService = productQueryService;
    }

    // GET api/product_variants
    public ApiProductVariants GetProductVariants(int productId)
    {
      return Mapper.Map<ApiProductVariants>(_productQueryService.ProductVariants(productId));
    }

    // GET api/product_variants/5
    public ApiProductVariant Get(int productVariantId)
    {
      return Mapper.Map<ApiProductVariant>(_productQueryService.FindProductVariant(productVariantId));
    }
  }
}