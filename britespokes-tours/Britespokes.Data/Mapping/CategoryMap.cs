using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
    public class CategorytMap : EntityTypeConfiguration<Category>
    {
        public CategorytMap()
        {
            // Primary Key
            HasKey(p => p.Id);

            Property(p => p.Name)
              .IsRequired();

        }
    }
}
