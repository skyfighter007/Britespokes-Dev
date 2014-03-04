using Britespokes.Core.Domain;
using System;
using System.Collections.Generic;
namespace Britespokes.Services.Admin.Perspectives
{
  public class CommentUpdate
  {
      public int Id { get; set; }
      public int PerspectivePostId { get; set; }

      public string Name { get; set; }
      public string Email { get; set; }
      public string Content { get; set; }
      public DateTime? PostedOn { get; set; }
      public bool IsApproved { get; set; }
   
  }

  
}