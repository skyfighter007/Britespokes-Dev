using System.Data.Entity.ModelConfiguration;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Mapping
{
  public class AssetMap : EntityTypeConfiguration<Asset>
  {
    public AssetMap()
    {
      // Primary Key
      HasKey(a => a.Id);
    }
  }
}
