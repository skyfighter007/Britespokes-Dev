using System;

namespace Britespokes.Core.Domain
{
  public class Address : Entity
  {
    public Address()
    {
      CreatedAt = UpdatedAt = DateTime.UtcNow;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Phone { get; set; }
    public string MobilePhone { get; set; }
    public string Company { get; set; }

    public string StateOrProvince { get; set; }
    public int CountryId { get; set; }
    public virtual Country Country { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string FullName
    {
      get
      {
        return string.Format("{0} {1}", FirstName, LastName);
      }
    }
  }
}