using System.ComponentModel.DataAnnotations;

namespace Britespokes.Web.Models.College
{
  public class SchoolDetails
  {
    [Required(ErrorMessage = "Name  is required")]
    [Display(Name = "name")]
    public string Name { get; set; }
       [Required(ErrorMessage = "street address is required")]
    [Display(Name = "street address")]
    public string StreetAddress { get; set; }
      [Required(ErrorMessage = "city / state is required")]
    [Display(Name = "city / state")]
    public string City { get; set; }
    [Display(Name = "state")]
    public string State { get; set; }
    [Required(ErrorMessage = "zip code is required")]
    [Display(Name = "zip code")]
    public string ZipCode { get; set; }
    [Required(ErrorMessage = "country is required")]
    [Display(Name = "country")]
    public string Country { get; set; }
    [Required(ErrorMessage = "school contact name is required")]
    [Display(Name = "school contact name")]
    public string ContactName { get; set; }
    [Required(ErrorMessage = "school contact telephone is required")]
    [Display(Name = "school contact telephone")]
    public string ContactTelephone { get; set; }
    [Required(ErrorMessage = "school contact email required")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "school contact email")]
    public string ContactEmail { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "number of students travelling")]
    public string TravelingStudentCount { get; set; }
  }
}