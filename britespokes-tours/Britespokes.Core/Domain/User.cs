using System;
using System.Collections.Generic;
using System.Linq;

namespace Britespokes.Core.Domain
{
  public class User : Entity
  {
    public User()
    {
      IsActive = true;
      IsDeleted = false;
      IsSystemAccount = false;
      CreatedAt = UpdatedAt = LastActivityAt = DateTime.UtcNow;
    }

    public int OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }

    public int? AddressId { get; set; }
    public virtual Address Address { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordSalt { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IATA { get; set; }
    public string Affiliation  { get; set; }

    public string AdminComment { get; set; }
    public string TimeZoneId { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsSystemAccount { get; set; }
    public string LastIpAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public DateTime LastActivityAt { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    public string ConfirmationToken { get; set; }
    public DateTime? ConfirmationSentAt { get; set; }

    public virtual List<Role> Roles { get; set; }
    public virtual List<Order> Orders { get; set; }
    public virtual List<PerspectivePost> PerspectivePosts { get; set; }

    public bool IsConfirmed
    {
      get { return ConfirmedAt.HasValue; }
    }

    public void AddRole(Role role)
    {
      if (Roles == null)
        Roles = new List<Role>();
      Roles.Add(role);
    }

    public void RemoveRole(Role role)
    {
      if (Roles == null) return;
      if (Roles.Contains(role))
        Roles.Remove(role);
    }

    public void SetRole(Role role)
    {
      if (Roles == null)
        Roles = new List<Role>();
      Roles.Clear();
      Roles.Add(role);
    }

    public bool IsGuest
    {
      get
      {
        return RoleNames.Contains(Role.RoleGuestName);
      }
    }

    public bool IsAdmin
    {
      get
      {
        return RoleNames.Contains(Role.RoleAdminName);
      }
    }

    public string FullName
    {
      get
      {
        return string.Format("{0} {1}", FirstName, LastName);
      }
    }

    public string RoleNames
    {
      get { return string.Join(", ", Roles.Select(r => r.Name)); }
    }
  }
}