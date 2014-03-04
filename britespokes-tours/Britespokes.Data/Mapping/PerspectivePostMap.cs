using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
    public class PerspectivePostMap : EntityTypeConfiguration<PerspectivePost>
    {
        public PerspectivePostMap()
        {
            // Primary Key
            HasKey(p => p.Id);

            HasMany(o => o.Comments)
                   .WithRequired(t => t.PerspectivePost)
                   .WillCascadeOnDelete(true);
            //    HasMany(p => p.Categories)
            //.WithMany(r => r.PerspectivePosts)
            //.Map(m =>
            //{
            //    m.MapLeftKey("PerspectivePostId");
            //    m.MapRightKey("CategoryId");
            //    m.ToTable("CategoryPerspectivePosts");
            //});
        }
    }
}
