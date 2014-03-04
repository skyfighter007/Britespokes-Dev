namespace Britespokes.Services.Security
{
  public struct HashedText
  {
    public string Text { get; set; }
    public string Salt { get; set; }
  }
}
