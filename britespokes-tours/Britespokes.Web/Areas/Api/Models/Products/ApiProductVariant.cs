using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Britespokes.Web.Areas.Api.Models.Products
{
  [DataContract(Name = "productVariant")]
  public class ApiProductVariant
  {
    [DataMember(Name = "id")]
    public int Id { get; set; }
    [DataMember(Name = "displayName")]
    public string Name { get; set; }
    [DataMember(Name = "name")]
    public string DisplayName { get; set; }
    [DataMember(Name = "code")]
    public string Code { get; set; }
  }

  [CollectionDataContract(Name = "productVariants", ItemName = "productVariant")]
  public class ApiProductVariants : Collection<ApiProductVariant> {}
}