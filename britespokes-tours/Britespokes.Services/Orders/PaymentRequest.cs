using System.Globalization;
using AuthorizeNet;

namespace Britespokes.Services.Orders
{
  public class PaymentRequest
  {
    public PaymentRequest(decimal amount, string apiLogin, string transactionKey, bool testMode)
    {
      Amount = amount;
      ApiLogin = apiLogin;
      Sequence = Crypto.GenerateSequence();
      Timestamp = Crypto.GenerateTimestamp();
      Fingerprint = Crypto.GenerateFingerprint(transactionKey,
          ApiLogin, Amount, Sequence.ToString(CultureInfo.InvariantCulture),
          Timestamp.ToString(CultureInfo.InvariantCulture));

      Url = testMode ? Gateway.TEST_URL : Gateway.LIVE_URL;
    }

    public string Sequence { get; private set; }
    public int Timestamp { get; private set; }
    public string Fingerprint { get; private set; }
    public decimal Amount { get; private set; }
    public string ApiLogin { get; private set; }
    public string Url { get; private set; }
  }
}