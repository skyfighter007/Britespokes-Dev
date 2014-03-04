using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Britespokes.Web.Infrastructure.Uploaders
{
  public class AzureImageUploader : IImageUploader
  {
    private readonly CloudBlobClient _blobClient;
    private readonly CloudBlobContainer _container;
    private readonly CloudBlobContainer _filecontainer;
    private const string ContainerName = "images";
    private const string FileContainerName = "files";

    public AzureImageUploader()
    {
      var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
      _blobClient = storageAccount.CreateCloudBlobClient();
      _container = FindOrCreateContainer(ContainerName);
      _filecontainer = FindOrCreateContainer(FileContainerName);
    }

    public ImageInfo UploadFile(string filename, HttpPostedFileBase file)
    {
      return UploadFile(filename, "", file);
    }

    public ImageInfo UploadFile(string filename, string prefix, HttpPostedFileBase file)
    {
      if (!string.IsNullOrWhiteSpace(prefix)) filename = string.Format("{0}/{1}", prefix, filename);
      if (file.ContentType == "application/pdf" || file.ContentType == "application/doc")
      {

          filename = GetUniqueName(FileContainer, filename);

          var blob = FileContainer.GetBlockBlobReference(filename);
          blob.Properties.ContentType = file.ContentType;
          blob.UploadFromStream(file.InputStream);

          return new ImageInfo
          {
              Name = blob.Name,
              ContentType = blob.Properties.ContentType,
              Size = blob.Properties.Length,
              Url = blob.Uri.ToString()
          };
      }
      else
      {
          filename = GetUniqueName(Container, filename);

          var blob = Container.GetBlockBlobReference(filename);
          blob.Properties.ContentType = "image/jpeg";
          blob.UploadFromStream(file.InputStream);

          return new ImageInfo
          {
              Name = blob.Name,
              ContentType = blob.Properties.ContentType,
              Size = blob.Properties.Length,
              Url = blob.Uri.ToString()
          };
      }

      
    }

    public string GetUniqueName(CloudBlobContainer container, string filename, int maxAttempts = 1024)
    {
      var ext = Path.GetExtension(filename);
      var baseFilename = ext != null ? filename.Replace(ext, "") : filename;
      var prefix = filename.Replace(Path.GetFileName(filename), "");

      var files = new HashSet<string>(BlobNamesInContainer(Container, prefix));
      for (var index = 0; index < maxAttempts; index++)
      {
        // first try with the original filename, else try incrementally adding an index
        var name = (index == 0)
            ? filename
            : string.Format("{0}-{1}{2}", baseFilename, index, ext);

        // check if exists
        if (files.Contains(name))
          continue;

        return name;
      }

      return filename;
    }

    public string GetUniqueName(string filename, int maxAttempts = 1024)
    {
      return GetUniqueName(Container, filename, maxAttempts);
    }

    private CloudBlobContainer Container
    {
      get { return _container; }
    }

    private CloudBlobContainer FileContainer
    {
        get { return _filecontainer; }
    }

    private CloudBlobClient BlobClient
    {
      get { return _blobClient; }
    }

    private IEnumerable<string> BlobNamesInContainer(CloudBlobContainer container, string prefix = null)
    {
      return container.ListBlobs(prefix, true)
        .OfType<ICloudBlob>()
        .Where(b => !string.IsNullOrWhiteSpace(b.Name))
        .Select(blockBlob => blockBlob.Name);
    }

    private CloudBlobContainer FindOrCreateContainer(string containerName)
    {
      var container = BlobClient.GetContainerReference(containerName);
      if (!container.Exists())
      {
        container.Create();
        container.SetPermissions(new BlobContainerPermissions
        {
          PublicAccess = BlobContainerPublicAccessType.Container
        });
      }
      return container;
    }
  }
}