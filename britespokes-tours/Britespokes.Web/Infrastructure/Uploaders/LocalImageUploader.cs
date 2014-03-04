using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace Britespokes.Web.Infrastructure.Uploaders
{
  public class LocalImageUploader : IImageUploader
  {
    private string _storageRoot;

    public ImageInfo UploadFile(string filename, HttpPostedFileBase file)
    {
      return UploadFile(filename, null, file);
    }

    public ImageInfo UploadFile(string filename, string prefix, HttpPostedFileBase file)
    {
      if (file == null || filename == null) return null;
      if (prefix == null) prefix = "";

      var fullPath = Path.Combine(StorageRoot, prefix, filename);
      var newName = GetUniqueName(fullPath);

      var directory = Path.GetDirectoryName(fullPath);
      Debug.Assert(directory != null, "directory != null");
      if (!Directory.Exists(directory))
        Directory.CreateDirectory(directory);

      file.SaveAs(newName);

      var url = "/Content/images/uploads";
      if (!string.IsNullOrWhiteSpace(prefix)) url += "/" + prefix;
      url += "/" + Path.GetFileName(newName);

      return new ImageInfo
      {
        Name = newName,
        Size = file.ContentLength,
        ContentType = file.ContentType,
        Url = url
      };
    }

    public string GetUniqueName(string filename, int maxAttempts = 1024)
    {
      var directory = Path.GetDirectoryName(filename);
      var baseFilename = Path.GetFileNameWithoutExtension(filename);
      var ext = Path.GetExtension(filename);

      Debug.Assert(directory != null, "directory != null");
      var files = new HashSet<string>(Directory.GetFiles(directory));

      for (var index = 0; index < maxAttempts; index++)
      {
        // first try with the original filename, else try incrementally adding an index
        var name = (index == 0)
          ? Path.GetFileName(filename)
          : string.Format("{0}-{1}{2}", baseFilename, index, ext);

        // check if exists
        var fullPath = Path.Combine(directory, name);
        if (files.Contains(fullPath))
          continue;

        return fullPath;
      }

      return null;
    }

    private string StorageRoot
    {
      get { return _storageRoot ?? (_storageRoot = HttpContext.Current.Server.MapPath("~/Content/images/uploads")); }
    }
  }
}