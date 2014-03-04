using System.ComponentModel.DataAnnotations;

namespace Britespokes.Web.Models.Users
{
  public class RegisterInput
  {
    [Required(ErrorMessage = "required")]
    [Display(Name = "first name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "required")]
    [Display(Name = "last name")]
    public string LastName { get; set; }


    [Display(Name = "IATA # (if applicable)")]
    public string IATA { get; set; }


    [Display(Name = "Affiliation (if applicable)")]
    public string Affiliation { get; set; }

    [Required(ErrorMessage = "required")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "required")]
    [DataType(DataType.Password)]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "must be at least 8 characters")]
    [Display(Name = "password")]
    public string Password { get; set; }

    [Required(ErrorMessage = "required")]
    [DataType(DataType.Password)]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "must be at least 8 characters")]
    [Display(Name = "confirm password")]
    public string ConfirmPassword { get; set; }

    [Display(Name = "passcode")]
    public string Passcode { get; set; }
  }
}