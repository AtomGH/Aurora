using Aurora.Core.Data.Services;
using Aurora.Library.Common;

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
        /// <param name="idGenerator"></param>
        public DataService(DatabaseContext context, IdentifierGenerator idGenerator)
        {
            _context = context;
            Accounts = new AccountsService(_context, idGenerator);
            Projects = new ProjectsService(_context, idGenerator);
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
