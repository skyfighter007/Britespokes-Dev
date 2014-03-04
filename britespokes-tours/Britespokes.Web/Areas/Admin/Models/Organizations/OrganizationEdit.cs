using System.ComponentModel.DataAnnotations;
using Britespokes.Web.Infrastructure.Validation;

namespace Britespokes.Web.Areas.Admin.Models.Organizations
{
    public class OrganizationEdit
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Subdomain { get; set; }
        [Display(Name = "Custom Domain")]
        public string CustomDomain { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public bool IsPasscodeRequired { get; set; }
        [RequiredIf("IsPasscodeRequired", ErrorMessage = "Required")]
        public string Passcode { get; set; }
        public bool RestrictedEmailDomains { get; set; }
        public string EmailDomainList { get; set; }
        [Required]
        [Display(Name ="Logo Image Url")]
        public string LogoImageURL { get; set; }
        [Required]
        public string LogoImageAltText { get; set; }

        public int UserCount { get; set; }
        public int TourCount { get; set; }
    }
}