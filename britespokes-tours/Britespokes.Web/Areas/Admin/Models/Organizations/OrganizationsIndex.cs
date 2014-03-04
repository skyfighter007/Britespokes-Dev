using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Areas.Admin.Models.Organizations
{
  public class OrganizationsIndex
  {
    public IEnumerable<Organization> Organizations { get; set; }
    public int Count { get; set; }
  }
}