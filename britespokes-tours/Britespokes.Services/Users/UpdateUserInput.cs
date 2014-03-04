using System.ComponentModel.DataAnnotations;

namespace Britespokes.Services.Users
{
    public class UpdateUserInput
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

    [Display(Name = "phone")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }

    [Display(Name = "city")]
    public string City { get; set; }


    public int? AddressId { get; set;} 
    public int Id { get; set; }
  }
}