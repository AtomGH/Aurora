using Aurora.Library.Common;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Data.Entities
{
    public class ToolVersion
    {
        public int Id { get; set; }
        public string Version { get; set; } = string.Empty;
        public Tool Tool { get; set; }
        public IQueryable<ToolPackage> Packages { get; set; }
        public List<int> PackageIds { get; set; }

        private readonly DatabaseContext _databaseContext;

        public ToolVersion(DatabaseContext context, string version, Tool tool)
        {
            _databaseContext = context;
            Version = version;
            Tool = tool;
            Packages = _databaseContext.ToolPackages.Where(p => p.Version.Id == Id);
            PackageIds = new();
        }

        private ToolVersion(DatabaseContext context)
        {
            _databaseContext = context;
            Tool = null!;
            Packages = null!;
            PackageIds = null!;
        }

        public async Task<ToolPackage> CreatePackageAsync(DatabaseContext context, string platform, IStorageObject content)
        {
            if (await context.ToolPackages.AnyAsync(p => p.Version.Id == Id && p.Platform == platform))
            {
                throw new InvalidOperationException("A package with same platform for the tool version is already exist.");
            }

            ToolPackage newPackage = new(platform, this, content);
            await context.ToolPackages.AddAsync(newPackage);
            return newPackage;
        }
    }
}
