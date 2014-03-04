using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Britespokes.Web.Areas.Api.Models.Tours
{
  [DataContract(Name = "tour")]
  public class ApiTour
  {
    [DataMember(Name = "id")]
    public int Id { get; set; }
    [DataMember(Name = "code")]
    public string Code { get; set; }
    [DataMember(Name = "name")]
    public string Name { get; set; }
    [DataMember(Name = "permalink")]
    public string Permalink { get; set; }
  }

  [CollectionDataContract(Name = "tours", ItemName = "tour")]
  public class ApiTours : Collection<ApiTour> {}
}