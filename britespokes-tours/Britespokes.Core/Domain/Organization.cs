using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Britespokes.Core.Domain
{
    public class Organization : Entity
    {
        public Organization()
        {
            CreatedAt = UpdatedAt = DateTime.UtcNow;
            IsActive = true;
            RestrictedEmailDomains = false;
            IsPublic = false;
        }

        public string Name { get; set; }
        public string Subdomain { get; set; }
        public string CustomDomain { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public bool IsPasscodeRequired { get; set; }
        public string Passcode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool RestrictedEmailDomains { get; set; }

        public string LogoImageURL { get; set; }
        public string LogoImageAltText { get; set; }


        public virtual List<Product> Products { get; set; }
        public virtual List<OrganizationAsset> Images { get; set; }
        public virtual List<EmailDomain> EmailDomains { get; set; }

        public virtual List<Departure> Departures { get; set; }

    }

}
