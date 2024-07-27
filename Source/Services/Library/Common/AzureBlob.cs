using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Common
{
    public class AzureBlob : IStorageObject
    {
        public string Path { get; private set; }

        private readonly AzureBlobSet _blobContainer;

        public AzureBlob(AzureBlobSet container, string path)
        {
            _blobContainer = container;
            Path = path;
        }

        public string GetFileName()
        {
            return Path.Split('/').Last();
        }

        public async Task<Uri> GetPresignedAccessUriAsync(ObjectStoragePermission permissions)
        {
            UserDelegationKey userDelegationKey = await _blobContainer.RequestUserDelegationKeyAsync();

            BlobSasBuilder sasBuilder = new()
            {
                BlobContainerName = _blobContainer.GetContainerName(),
                BlobName = GetFileName(),
                Resource = "b",
                StartsOn = userDelegationKey.SignedStartsOn,
                ExpiresOn = userDelegationKey.SignedExpiresOn
            };
            sasBuilder.SetPermissions(_blobContainer.ConvertObjectPermissionsToSasPermissions(permissions));

            BlobUriBuilder uriBuilder = new(new(Path))
            {
                Sas = sasBuilder.ToSasQueryParameters(userDelegationKey, _blobContainer.GetAccountName())
            };

            return uriBuilder.ToUri();
        }
    }
}
