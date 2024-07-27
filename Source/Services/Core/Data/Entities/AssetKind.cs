namespace Aurora.Core.Data.Entities
{
    public class AssetKind
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int ProjectId { get; private set; }
        public Project Project { get; private set; }
        public Dictionary<int, string> Pipeline { get; private set; }

        public AssetKind(string name, string description, Pipeline pipeline, Project project)
        {
            Name = name;
            Description = description;
            Project = project;
            Pipeline = new();
        }

        private AssetKind()
        {
            // Required by EF Core.
            Name = string.Empty;
            Description = string.Empty;
            Project = null!;
            Pipeline = null!;
        }
    }
}
