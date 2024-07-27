namespace Aurora.Core.Data.Entities
{
    public class AssetVersion
    {
        public int Id { get; private set; }
        public int Version { get; private set; }
        public string Description { get; private set; }
        public string Preview { get; private set; }
        public int AssetId { get; private set; }
        public Asset Asset { get; private set; }

        public AssetVersion(int version, string description, string preview, Asset asset)
        {
            Description = description;
            Preview = preview;
            Asset = asset;
            Version = version;
            AssetId = asset.Id;
        }

        private AssetVersion()
        {
            // Required by EF Core
            Description = null!;
            Preview = null!;
            Asset = null!;
        }
    }
}
