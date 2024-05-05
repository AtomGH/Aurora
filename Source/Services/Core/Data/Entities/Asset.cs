namespace Aurora.Core.Data.Entities
{
    public class Asset
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public AssetType Type { get; private set; }
        public Project Project { get; private set; }
        public int VersionCounter { get; private set; }

        public Asset(string name, string description, AssetType type, Project project)
        {
            Name = name;
            Description = description;
            Type = type;
            Project = project;
            VersionCounter = 0;
        }

        private Asset()
        {
            // Required by EF Core
            Name = null!;
            Description = null!;
            Type = null!;
            Project = null!;
        }

        public int IncreaseVersionCounter()
        {
            return VersionCounter++;
        }
    }
}
