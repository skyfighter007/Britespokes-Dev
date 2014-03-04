using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Britespokes.Services.Orders
{
  public class TravelerDetails
  {
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int Index { get; set; }

    [Required(ErrorMessage = "required")]
    [Display(Name = "email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "first name")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "last name")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "telephone")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = "required")]
    [Display(Name = "birthday")]
    public DateTime? Birthday { get; set; }

    public bool EmailItinerary { get; set; }
    [Display(Name = "special requests")]
    [UIHint("MultilineText")]
    public string SpecialInstructions { get; set; }
  }

  public class ConfirmTravelers
  {
    public int OrderId { get; set; }
    public List<TravelerDetails> Travelers { get; set; }
  }
}