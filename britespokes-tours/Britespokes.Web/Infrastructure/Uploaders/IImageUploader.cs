using System.Web;

namespace Britespokes.Web.Infrastructure.Uploaders
{
  public interface IImageUploader
  {
    ImageInfo UploadFile(string filename, string prefix, HttpPostedFileBase file);
    ImageInfo UploadFile(string filename, HttpPostedFileBase file);
    string GetUniqueName(string filename, int maxAttempts = 1024);
  }
}