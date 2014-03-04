using System;
using System.IO;

namespace Britespokes.Core.Domain
{
  public class Asset : Entity
  {
    public Asset()
    {
      CreatedAt = UpdatedAt = DateTime.UtcNow;
    }

    public int Width { get; set; }
    public int Height { get; set; }
    public int FileSize { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public string AltText { get; set; }
    public byte[] Data { get; set; }
    public bool IsThumbnail { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string FileNameUrl
    {
      get
      {
        if (FileName == null)
          return null;

        return Path.GetFileNameWithoutExtension(FileName);
      }
    }
  }
}
