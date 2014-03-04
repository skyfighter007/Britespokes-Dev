using System.Collections.Generic;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Products
{
  public class ProductQueryService : IProductQueryService
  {
    private readonly IRepository<Product> _productRepo;
    private readonly IRepository<ProductVariant> _productVariantRepo;

    public ProductQueryService(IRepository<Product> productRepo, IRepository<ProductVariant> productVariantRepo)
    {
      _productRepo = productRepo;
      _productVariantRepo = productVariantRepo;
    }

    public Product FindProduct(int productId)
    {
      return _productRepo.Find(productId);
    }

    public IEnumerable<ProductVariant> ProductVariants(int productId)
    {
      var product = _productRepo.Find(productId);
      return ProductVariants(product);
    }

    public IEnumerable<ProductVariant> ProductVariants(Product product)
    {
      return product.ProductVariants;
    }

    public ProductVariant FindProductVariant(int productVariantId)
    {
      return _productVariantRepo.Find(productVariantId);
    }
  }
}