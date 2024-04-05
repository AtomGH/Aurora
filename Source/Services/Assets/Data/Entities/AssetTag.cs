namespace Assets.Data.Entities
{
    public class AssetTag
    {
        public long Id { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public Asset Asset { get; private set; }

        public AssetTag(Asset asset, string key, string value)
        {
            Asset = asset;
            Key = key;
            Value = value;
        }

        private AssetTag()
        {
            Key = string.Empty;
            Value = string.Empty;
            Asset = null!;
        }
    }
}
