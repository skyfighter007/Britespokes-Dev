namespace Britespokes.Services.Security
{
  public interface IEncryptionService
  {
    HashedText ComputeHash(string text);
    HashedText ComputeHash(string text, string salt);
  }
}
