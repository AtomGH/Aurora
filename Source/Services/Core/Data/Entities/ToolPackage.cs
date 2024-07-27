using Aurora.Library.Common;

namespace Aurora.Core.Data.Entities
{
    public class ToolPackage
    {
        public int Id { get; set; }
        public ToolVersion Version { get; set; }
        public string Platform { get; set; }
        public IStorageObject Content { get; set; }

        public ToolPackage(string platform, ToolVersion version, IStorageObject content)
        {
            Platform = platform;
            Version = version;
            Content = content;
        }

        private ToolPackage()
        {
            Version = null!;
            Platform = null!;
            Content = null!;
        }

        public async Task<Uri> GetUploadUrlAsync()
        {
            return await Content.GetPresignedAccessUriAsync(ObjectStoragePermission.Write);
        }

        public async Task<Uri> GetDownloadUrl()
        {
            return await Content.GetPresignedAccessUriAsync(ObjectStoragePermission.Read);
        }
    }
}
