namespace Assets.Data.Entities
{
    public class AssetType
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public AssetType(string name, string description)
        {
            Name = name;
            Description = description;
        }

        private AssetType()
        {
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}
