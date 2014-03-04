using System;
using SimpleCrypto;

namespace Britespokes.Services.Security
{
  public class EncryptionService : IEncryptionService
  {
    private readonly ICryptoService _cryptoService;
    private readonly int _saltSize = 16;
    private readonly int _iterations = 50;

    public EncryptionService()
    {
      _cryptoService = new PBKDF2();
    }

    public EncryptionService(int saltSize, int iterations)
    {
      _saltSize = saltSize;
      _iterations = iterations;
    }

    public HashedText ComputeHash(string text)
    {
      var salt = _cryptoService.GenerateSalt(_iterations, _saltSize);
      return ComputeHash(text, salt);
    }

    public HashedText ComputeHash(string text, string salt)
    {
      var saltAndText = String.Concat(text, salt);
      return new HashedText
        {
          Salt = salt,
          Text = _cryptoService.Compute(saltAndText, salt)
        };
    }
  }
}
