namespace Britespokes.Core.Domain
{
  public class Country : Entity
  {
    public string Iso { get; set; }
    public string Name { get; set; }
    public string PrintableName { get; set; }
    public string Iso3 { get; set; }
    public int? CountryCode { get; set; }
  }
}
