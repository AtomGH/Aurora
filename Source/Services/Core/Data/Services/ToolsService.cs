using Azure.Identity;
using Aurora.Core.Data.Entities;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Aurora.Library.Common;

namespace Aurora.Core.Data.Services
{
    public class ToolsService
    {
        private readonly DatabaseContext _context;
        private readonly BlobServiceClient _blobServiceClient;

        public ToolsService(DatabaseContext context)
        {
            _context = context;
            _blobServiceClient = new(new Uri(""), new ManagedIdentityCredential());
        }

        public async Task<Tool> GetAsync(int toolId)
        {
            Tool? targetTool = await _context.Tools.FindAsync(toolId);
            if (targetTool == null)
            {
                throw new Exception("The tool does not exist.");
            }
            return targetTool;
        }

        public async Task<Tool> AddAsync(string name, string description, string version)
        {
            // TODO: Example.
            AzureBlobSet blobSet = new(_blobServiceClient, "tools");
            AzureBlob contentBlob = new(blobSet, "/packages/id/filename");
            Tool newTool = new(_context, name, description, version, contentBlob);
            await _context.Tools.AddAsync(newTool);
            return newTool;
        }

        public async Task<Uri> GetPackageUploadUrlAsync(ToolPackage package)
        {
            UserDelegationKey userDelegationKey = await _blobServiceClient.GetUserDelegationKeyAsync(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddHours(1));
            BlobSasBuilder sasBuilder = new()
            {
                BlobContainerName = "",
                BlobName = "",
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Write);

            BlobUriBuilder uriBuilder = new(new Uri(""))
            {
                Sas = sasBuilder.ToSasQueryParameters(userDelegationKey, "")
            };

            return uriBuilder.ToUri();
        }

        public async Task<Uri> GetPackageDownloadUrlAsync(ToolPackage package)
        {
            UserDelegationKey userDelegationKey = await _blobServiceClient.GetUserDelegationKeyAsync(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddHours(1));
            BlobSasBuilder sasBuilder = new()
            {
                BlobContainerName = "",
                BlobName = "",
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            BlobUriBuilder uriBuilder = new(new Uri(""))
            {
                Sas = sasBuilder.ToSasQueryParameters(userDelegationKey, "")
            };

            return uriBuilder.ToUri();
        }
    }
}
