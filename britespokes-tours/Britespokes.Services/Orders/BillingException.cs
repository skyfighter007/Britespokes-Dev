using System;

namespace Britespokes.Services.Orders
{
  public class BillingException : ApplicationException
  {
    private readonly string _errorMessage;
    private readonly string _errorType;

    public BillingException(string errorMessage, string errorType)
      : base(string.Format("Could not bill via Authorize.Net: {0} ({1})", errorMessage, errorType))
    {
      _errorMessage = errorMessage;
      _errorType = errorType;
    }

    public string ErrorMessage { get { return _errorMessage; } }
    public string ErrorType { get { return _errorType; } }
  }
}
