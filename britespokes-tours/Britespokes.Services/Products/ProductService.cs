using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Products
{
  public class ProductService : IProductService
  {
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<ProductVariant> _productVariantRepository;

    public ProductService(IRepository<Product> productRepository, IRepository<ProductVariant> productVariantRepository)
    {
      _productRepository = productRepository;
      _productVariantRepository = productVariantRepository;
    }

    public Product Find(int id)
    {
      return _productRepository.Find(id);
    }

    public ProductVariant FindVariant(int variantId)
    {
      return _productVariantRepository.Find(variantId);
    }
  }
}
