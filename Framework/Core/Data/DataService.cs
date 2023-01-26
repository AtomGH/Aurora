using Framework.Core.Data.Services;

namespace Framework.Core.Data
{
    public class DataService
    {
        private readonly DatabaseContext _context;

        public AccountsService Accounts { get; }
        public ProjectsService Projects { get; }

        public DataService(DatabaseContext context)
        {
            _context = context;
            Accounts = new AccountsService(_context);
            Projects = new ProjectsService(_context);
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
