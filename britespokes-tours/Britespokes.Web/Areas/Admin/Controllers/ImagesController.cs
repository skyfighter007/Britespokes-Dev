using System.Collections.Generic;
using System.Web.Mvc;
using Britespokes.Web.Infrastructure.Uploaders;

namespace Britespokes.Web.Areas.Admin.Controllers
{
  public class ImagesController : AdminBaseController
  {
    private readonly IImageUploader _imageUploader;

    public ImagesController(IImageUploader imageUploader)
    {
      _imageUploader = imageUploader;
    }

    [HttpPost]
    public ActionResult Create(string prefix)
    {
      var results = new List<ImageInfo>();

      // only supporting uploading one image at a time for now
      if (Request.Files.Count > 0)
      {
        var file = Request.Files [ 0 ];
        if (file != null) results.Add(_imageUploader.UploadFile(file.FileName, prefix, file));
      }

      return Json(new { files = results });
    }
  }
}