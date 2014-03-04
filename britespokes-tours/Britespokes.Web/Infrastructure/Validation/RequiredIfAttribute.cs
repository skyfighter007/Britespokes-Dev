using System;
using System.ComponentModel.DataAnnotations;

namespace Britespokes.Web.Infrastructure.Validation
{
  // http://blogs.msdn.com/b/stuartleeks/archive/2011/10/06/flexible-conditional-validation-with-asp-net-mvc-3.aspx
  public class RequiredIfAttribute : ValidationAttribute
  {
    private readonly string _condition;

    public RequiredIfAttribute(string condition)
    {
      _condition = condition;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var conditionFunction = CreateExpression(validationContext.ObjectType, _condition);
      var conditionMet = (bool)conditionFunction.DynamicInvoke(validationContext.ObjectInstance);
      if (conditionMet)
      {
        if (value == null)
        {
          return new ValidationResult(FormatErrorMessage(null));
        }
      }
      return null;
    }

    private Delegate CreateExpression(Type objectType, string expression)
    {
      // TODO - add caching
      var lambdaExpression = System.Linq.Dynamic.DynamicExpression.ParseLambda(objectType, typeof(bool), expression);
      var func = lambdaExpression.Compile();
      return func;
    }
  }
}