namespace Aurora.Core.Data.Entities
{
    public class Asset
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int KindId { get; private set; }
        public AssetKind Kind { get; private set; }
        public int ProjectId { get; private set; }
        public Project Project { get; private set; }
        public int VersionCounter { get; private set; }
        public IQueryable<AssetVersion> Versions { get; private set; }

        private readonly DatabaseContext _databaseContext;

        public Asset(DatabaseContext context, string name, string description, AssetKind kind, Project project)
        {
            _databaseContext = context;
            Name = name;
            Description = description;
            Kind = kind;
            Project = project;
            VersionCounter = 0;
            Versions = context.AssetVersions.Where(v => v.Asset.Id == Id);
        }

        private Asset(DatabaseContext context)
        {
            // Required by EF Core
            _databaseContext = context;
            Name = null!;
            Description = null!;
            Kind = null!;
            Project = null!;
            Versions = null!;
        }

        public int IncreaseVersionCounter()
        {
            return VersionCounter++;
        }
    }
}
