using Britespokes.Core.Domain;
using System;
using System.Collections.Generic;
namespace Britespokes.Services.Admin.SEOToolStaticPages
{
    public class SEOToolStaticPageUpdate
  {
      public int Id { get; set; }
      public string FocusKeyword { get; set; }
      public string SEOTitle { get; set; }
      public string MetaDescription { get; set; }
      public string Permalink { get; set; }
  }
}