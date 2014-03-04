using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Britespokes.Web.Areas.Api.Models.Departures
{
  [DataContract(Name = "departure")]
  public class ApiDeparture
  {
    [DataMember(Name = "productId")]
    public int ProductId { get; set; }
    [DataMember(Name = "code")]
    public string Code { get; set; }
    [DataMember(Name = "name")]
    public string Name { get; set; }
  }

  [CollectionDataContract(Name = "departures", ItemName = "departure")]
  public class ApiDepartures : Collection<ApiDeparture> {}
}