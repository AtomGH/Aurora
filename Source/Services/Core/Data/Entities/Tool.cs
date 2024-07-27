using Aurora.Library.Common;
using Aurora.Library.Tools;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Data.Entities
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IStorageObject Icon { get; set; }
        public string Vendor { get; set; } = string.Empty;
        public IQueryable<ToolVersion> Versions { get; set; }
        public List<int> VersionIds { get; set; }

        private readonly DatabaseContext _databaseContext;

        public Tool(DatabaseContext context, string name, string description, string vender, IStorageObject icon)
        {
            _databaseContext = context;
            Name = name;
            Description = description;
            Vendor = vender;
            Versions = _databaseContext.ToolVersions.Where(v => v.Tool.Id == Id);
            VersionIds = new();
            Icon = icon;
        }

        private Tool(DatabaseContext context)
        {
            _databaseContext = context;
            Versions = null!;
            Icon = null!;
            VersionIds = null!;
        }

        public async Task<ToolVersion> CreateVersionAsync(DatabaseContext context, string version)
        {
            if (await context.ToolVersions.AnyAsync(v => v.Tool.Id == Id && v.Version == version))
            {
                throw new InvalidOperationException("Same version already exist.");
            }
            ToolVersion newVersion = new(_databaseContext, version, this);
            await context.ToolVersions.AddAsync(newVersion);
            return newVersion;
        }
    }
}
