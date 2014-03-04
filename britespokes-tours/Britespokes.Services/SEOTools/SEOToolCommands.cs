using Britespokes.Core.Domain;
using System;
using System.Collections.Generic;
namespace Britespokes.Services.Admin.SEOTools
{
  public class SEOToolUpdate
  {
      public int Id { get; set; }
      public string FocusKeyword { get; set; }
      public string SEOTitle { get; set; }
      public string MetaDescription { get; set; }
  }
}