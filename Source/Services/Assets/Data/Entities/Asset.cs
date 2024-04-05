namespace Assets.Data.Entities
{
    public class Asset
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public AssetType Type { get; private set; }
        public long ProjectId { get; private set; }
        public ICollection<AssetTag> Tags { get; private set; }
        public long OperatorId { get; private set; }
        public Dictionary<string, string> Token { get; set; }

        public Asset(string name, string description, AssetType type, long projectId, long providerId, Dictionary<string, string> token)
        {
            Name = name;
            Description = description;
            ProjectId = projectId;
            OperatorId = providerId;
            Token = token;
            Tags = new List<AssetTag>();
            Type = type;
        }

        private Asset()
        {
            Name = string.Empty;
            Description = string.Empty;
            Type = null!;
            Tags = new List<AssetTag>();
            Token = new();
        }
    }
}
