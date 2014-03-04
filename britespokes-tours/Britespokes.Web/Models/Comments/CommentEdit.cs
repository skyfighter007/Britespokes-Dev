using System.ComponentModel.DataAnnotations;
using Britespokes.Web.Infrastructure.Validation;
using System.Collections.Generic;
using Britespokes.Core.Domain;
using System;

namespace Britespokes.Web.Models.Comments
{
    public class CommentEdit
    {
        public int Id { get; set; }
       
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage="Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime? PostedOn { get; set; }
        public bool IsApproved { get; set; }

    }
}