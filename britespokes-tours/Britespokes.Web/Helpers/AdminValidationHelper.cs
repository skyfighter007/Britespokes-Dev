using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Britespokes.Web.Helpers
{
  public static class AdminValidationHelper
  {
    public static string AdminFieldBlockErrorClass<TModel, TProperty>(this HtmlHelper<TModel> html,
                                                                      Expression<Func<TModel, TProperty>> expression)
    {
      var elementName = ExpressionHelper.GetExpressionText(expression);
      var modelName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(elementName);
      var formContext = html.ViewContext.ClientValidationEnabled ? html.ViewContext.FormContext : null;

      if (!html.ViewData.ModelState.ContainsKey(modelName) && formContext == null)
      {
        return null;
      }

      var modelState = html.ViewData.ModelState[modelName];
      var modelErrors = (modelState == null) ? null : modelState.Errors;
      if (modelErrors != null && modelErrors.Any())
      {
        return "error";
      }

      return null;
    }

    public static MvcHtmlString AdminValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string errorClass = "alert-msg")
    {
      var elementName = ExpressionHelper.GetExpressionText(expression);
      var modelName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(elementName);

      if (!html.ViewData.ModelState.ContainsKey(modelName))
      {
        return null;
      }

      var modelState = html.ViewData.ModelState[modelName];
      var modelErrors = (modelState == null) ? null : modelState.Errors;
      var modelError = (((modelErrors == null) || (modelErrors.Count == 0)) ? null : modelErrors.FirstOrDefault(m => !String.IsNullOrEmpty(m.ErrorMessage)) ?? modelErrors[0]);

      if (modelError == null)
        return null;

      var errorMessage = GetUserErrorMessageOrDefault(modelError);
      var i = new TagBuilder("i");
      i.AddCssClass("icon-remove-sign");

      var builder = new TagBuilder("span");
      builder.AddCssClass(errorClass);
      builder.InnerHtml = i + " " + errorMessage;

      return MvcHtmlString.Create(builder.ToString());
    }

    private static string GetUserErrorMessageOrDefault(ModelError error)
    {
      return !String.IsNullOrEmpty(error.ErrorMessage) ? error.ErrorMessage : null;
    }
  }
}