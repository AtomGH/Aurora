using Aurora.Framework.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Data
{
    public class DataService
    {
        public InstanceService Instances { get; }
        private DatabaseContext _context;

        public DataService(DatabaseContext context)
        {
            Instances = new(context);
            _context = context;
        }

        internal DatabaseContext GetContext()
        {
            return _context;
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
