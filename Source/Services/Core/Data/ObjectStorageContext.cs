using Aurora.Library.Common;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace Aurora.Core.Data
{
    public class ObjectStorageContext
    {
        public IObjectSet Tools { get; private set; }
        public ObjectStorageContext(Uri serviceUri)
        {
            BlobServiceClient azureBlobs = new(serviceUri);
            Tools = new AzureBlobSet(azureBlobs, "tools");
        }
    }
}
