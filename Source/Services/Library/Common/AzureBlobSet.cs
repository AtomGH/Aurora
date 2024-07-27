using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;

namespace Aurora.Library.Common
{
    public class AzureBlobSet : IObjectSet
    {
        private readonly BlobContainerClient _blobContainerClient;

        public AzureBlobSet(BlobServiceClient serviceClient, string containerName)
        {
            _blobContainerClient = serviceClient.GetBlobContainerClient(containerName);
        }
        
        public async Task UploadObjectAsync(Uri objectUri, Stream content)
        {
            throw new NotImplementedException();
        }

        public string GetContainerName() { return _blobContainerClient.Name; }

        public string GetAccountName() { return _blobContainerClient.AccountName; }

        public async Task<Uri> GetPresignedObjectUriAsync(Uri objectUri, ObjectStoragePermission permissions)
        {
            UserDelegationKey userDelegationKey = await RequestUserDelegationKeyAsync();

            BlobSasBuilder sasBuilder = new()
            {
                BlobContainerName = _blobContainerClient.Name,
                BlobName = "",
                Resource = "b",
                StartsOn = userDelegationKey.SignedStartsOn,
                ExpiresOn = userDelegationKey.SignedExpiresOn
            };
            sasBuilder.SetPermissions(ConvertObjectPermissionsToSasPermissions(permissions));

            BlobUriBuilder uriBuilder = new(objectUri)
            {
                Sas = sasBuilder.ToSasQueryParameters(userDelegationKey, _blobContainerClient.AccountName)
            };

            return uriBuilder.ToUri();
        }

        public async Task<UserDelegationKey> RequestUserDelegationKeyAsync()
        {
            BlobServiceClient serviceClient = _blobContainerClient.GetParentBlobServiceClient();
            return await serviceClient.GetUserDelegationKeyAsync(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddHours(1));
        }

        public BlobSasPermissions ConvertObjectPermissionsToSasPermissions(ObjectStoragePermission permissions)
        {
            BlobSasPermissions sasPermissions = 0;
            if (permissions.HasFlag(ObjectStoragePermission.Read))
            {
                sasPermissions = BlobSasPermissions.Read;
            }
            if (permissions.HasFlag(ObjectStoragePermission.Write))
            {
                sasPermissions |= BlobSasPermissions.Write;
            }
            if (permissions.HasFlag(ObjectStoragePermission.Create))
            {
                sasPermissions |= BlobSasPermissions.Create;
            }

            return sasPermissions;
        }
    }
}
