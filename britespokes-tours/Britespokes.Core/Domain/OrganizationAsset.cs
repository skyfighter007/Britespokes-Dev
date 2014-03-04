namespace Britespokes.Core.Domain
{
  public class OrganizationAsset : Asset
  {
    public int OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }
  }
}
