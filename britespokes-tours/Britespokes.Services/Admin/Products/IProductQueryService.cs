using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Products
{
  public interface IProductQueryService
  {
    Product FindProduct(int productId);
    IEnumerable<ProductVariant> ProductVariants(int productId);
    IEnumerable<ProductVariant> ProductVariants(Product product);
    ProductVariant FindProductVariant(int productVariantId);
  }
}