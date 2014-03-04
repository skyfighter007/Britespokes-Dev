using System.ComponentModel.DataAnnotations;

namespace Britespokes.Web.Areas.Admin.Models.Tours
{
  public class ProductVariantEdit
  {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public decimal PriceForBriter { get; set; }
    [Required]
    public bool IsEnabled { get; set; }
  }
}