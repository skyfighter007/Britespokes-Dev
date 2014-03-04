using System.ComponentModel.DataAnnotations;

namespace Britespokes.Web.Models.Users
{
  public class LoginInput
  {
    public LoginInput()
    {
      RememberMe = true;
    }

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "required")]
    [Display(Name = "email address")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "required")]
    [Display(Name = "password")]
    public string Password { get; set; }

    [Display(Name = "remember me?")]
    public bool RememberMe { get; set; }
  }
}