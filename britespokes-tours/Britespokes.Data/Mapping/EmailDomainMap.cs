using Britespokes.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Britespokes.Data.Mapping
{
  public class EmailDomainMap : EntityTypeConfiguration<EmailDomain>
  {
    public EmailDomainMap()
    {
      Property(d => d.Domain)
        .IsRequired();
    }
  }
}
