using Aurora.Core.Data.Services;

namespace Aurora.Core.Data
{
    /// <summary>
    /// Data service.
    /// </summary>
    public class DataService
    {
        private readonly DatabaseContext _context;

        /// <summary>
        /// 
        /// </summary>
        public AccountsService Accounts { get; }
        /// <summary>
        /// 
        /// </summary>
        public ProjectsService Projects { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public DataService(DatabaseContext context)
        {
            _context = context;
            Accounts = new AccountsService(_context);
            Projects = new ProjectsService(_context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
