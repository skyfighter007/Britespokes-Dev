namespace Britespokes.Core.Domain
{
  public class EmailDomain : Entity
  {
    public string Domain { get; set; }
    public int OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }
  }
}
