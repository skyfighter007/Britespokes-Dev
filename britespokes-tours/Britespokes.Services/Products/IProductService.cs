using Britespokes.Core.Domain;

namespace Britespokes.Services.Products
{
  public interface IProductService
  {
    Product Find(int id);
    ProductVariant FindVariant(int variantId);
  }
}
