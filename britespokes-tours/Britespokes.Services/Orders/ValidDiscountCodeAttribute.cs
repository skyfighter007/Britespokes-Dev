using System.ComponentModel.DataAnnotations;
using System.Linq;
using Britespokes.Core;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Orders
{
  public class ValidDiscountCodeAttribute : ValidationAttribute
  {
    private bool _isActive;
    private bool _isAvailable;
    private bool _wasFound;

    public override bool IsValid(object value)
    {
      var code = value as string;
      if (string.IsNullOrEmpty(code)) return true;

      var discountCodeRepo = Container.Get<IRepository<DiscountCode>>();
      var discountCode = discountCodeRepo.
        FindBy(dc => dc.LowerCode == code.ToLower()).
        SingleOrDefault();

      var isValid = false;
      if (discountCode != null) _wasFound = true;

      if (_wasFound && discountCode != null)
      {
        _isActive = discountCode.IsActive;
        _isAvailable = !discountCode.IsExpired();
        isValid = _isActive && _isAvailable;
      }

      return _wasFound && isValid;
    }

    public override string FormatErrorMessage(string name)
    {
      if (!_wasFound) return "Invalid discount code.";
      if (!_isActive) return "Sorry, this discount code is not active.";
      if (!_isAvailable) return "Sorry, this discount code is not available or is expired.";

      return "Invalid discount code.";
    }
  }
}