namespace Britespokes.Core.Domain
{
  public class ProductAsset : Asset
  {
    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; }
  }
}
