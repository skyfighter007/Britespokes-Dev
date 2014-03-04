namespace Britespokes.Services.Authentication
{
  public class CustomPrincipalSerializeModel
  {
    public int UserId { get; set; }
    public int OrganizationId { get; set; }
    public string[] Roles { get; set; }
    public bool IsConfirmed { get; set; }
    public string DisplayName { get; set; }
  }
}
