namespace Aurora.Core.Data.Entities
{
    public class AssetType
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Project Project { get; private set; }
        public Pipeline Pipeline { get; private set; }

        public AssetType(string name, string description, Pipeline pipeline, Project project)
        {
            Name = name;
            Description = description;
            Project = project;
            Pipeline = pipeline;
        }

        private AssetType()
        {
            Name = string.Empty;
            Description = string.Empty;
            Project = null!;
            Pipeline = null!;
        }
    }
}
