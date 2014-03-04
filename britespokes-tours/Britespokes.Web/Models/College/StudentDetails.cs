using System.ComponentModel.DataAnnotations;

namespace Britespokes.Web.Models.College
{
  public class StudentDetails
  
  {
    [Required(ErrorMessage = "required")]
    [Display(Name = "name")]
    public string Name { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "grade")]
    public string Grade { get; set; }
    [Display(Name = "birthdate")]
    [Required(ErrorMessage = "required")]
    public string Birthdate { get; set; }
    [Display(Name = "gender")]
    [Required(ErrorMessage = "required")]
    public string Gender { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "parent / guardian name")]
    public string Guardian1Name { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "cell phone number")]
    public string Guardian1MobilePhoneNumber { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "business phone number")]
    public string Guardian1BusinessPhoneNumber { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "home phone number")]
   
    public string Guardian1HomePhoneNumber { get; set; }
    [Display(Name = "email")]
    [Required(ErrorMessage = "required")]
    [DataType(DataType.EmailAddress)]
    public string Guardian1Email { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "parent / guardian name")]
    public string Guardian2Name { get; set; }
    [Display(Name = "cell phone number")]
    [Required(ErrorMessage = "required")]
    public string Guardian2MobilePhoneNumber { get; set; }
    [Display(Name = "business phone number")]
    [Required(ErrorMessage = "required")]
    public string Guardian2BusinessPhoneNumber { get; set; }
    [Display(Name = "home phone number")]
    [Required(ErrorMessage = "required")]
    public string Guardian2HomePhoneNumber { get; set; }
    [Display(Name = "email")]
    [Required(ErrorMessage = "required")]
    public string Guardian2Email { get; set; }
    [Display(Name = "additional notes")]
    [UIHint("MultilineText")]
    [Required(ErrorMessage = "required")]
    public string Notes { get; set; }
  }
}