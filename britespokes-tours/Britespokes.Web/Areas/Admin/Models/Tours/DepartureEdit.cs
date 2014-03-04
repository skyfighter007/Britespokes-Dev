using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Areas.Admin.Models.Tours
{
  public class DepartureEdit
  {
    public DepartureEdit()
    {
      DepartureDate = DateTime.Today;
      ReturnDate = DepartureDate.AddDays(3);
    }
    public int ProductId { get; set; }
    public int TourId { get; set; }
    public string TourName { get; set; }
    public string TourPermalink { get; set; }

    [Required]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

    [Display(Name = "Departure Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [Required]
    public DateTime DepartureDate { get; set; }

    [Display(Name = "Return Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [Required]
    public DateTime ReturnDate { get; set; }

    [Display(Name = "Availability")]
    [Required]
    public int AvailabilityStatusId { get; set; }
    public IEnumerable<AvailabilityStatus> AvailabilityStatusList { get; set; }

    [Display(Name = "Available On")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime? AvailableOn { get; set; }

    public List<ProductVariantEdit> ProductVariants { get; set; }

    public IEnumerable<Organization> OrganizationList { get; set; }
    public IEnumerable<Organization> SelectedOrganizationList { get; set; }
    public List<int> SelectedOrganizations { get; set; }
    [Required(ErrorMessage = "Organization is required")]
    public List<string> OrganizationModel { get; set; }
  }
}