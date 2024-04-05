using Aurora.Framework.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Data.Services
{
    public class InstanceService
    {
        private readonly DatabaseContext _context;

        public InstanceService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Instance> AddAsync(int id, string hostname)
        {
            Instance instance = new(id, hostname);
            await _context.Instances.AddAsync(instance);
            return instance;
        }

        public async Task<Instance> GetAsync(int id, Guid token)
        {
            Instance? targetInstance = await _context.Instances.Where(i => i.Id == id && i.Token == token).FirstOrDefaultAsync();
            if (targetInstance == null)
            {
                throw new InvalidOperationException("The instance does not exist.");
            }
            return targetInstance;
        }

        public async Task<List<int>> GetAllInstanceIdsAsync()
        {
            return await _context.Instances.Select(i => i.Id).ToListAsync();
        }
    }
}
