using Aurora.Core.Data.Services;
using Aurora.Library.Common;

namespace Aurora.Core.Data
{
    /// <summary>
    /// Data service.
    /// </summary>
    public class DataService
    {
        public DatabaseContext Database { get; }

        /// <summary>
        /// 
        /// </summary>
        public AccountsService Accounts { get; }
        /// <summary>
        /// 
        /// </summary>
        public ProjectsService Projects { get; }
        public PipelinesService Pipelines { get; }
        public AssetsService Assets { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="idGenerator"></param>
        public DataService(DatabaseContext context, IdentifierGenerator idGenerator)
        {
            Database = context;
            Accounts = new AccountsService(Database);
            Projects = new ProjectsService(Database);
            Pipelines = new PipelinesService(Database);
            Assets = new AssetsService(Database);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await Database.SaveChangesAsync(cancellationToken);
        }
    }
}
