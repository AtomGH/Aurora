using Aurora.Framework.Data;
using Aurora.Framework.Data.Entities;
using Aurora.Framework.Data.Extensions;
using Aurora.Library.Framework;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Applications
{
    public class InstanceApplication
    {
        private readonly DataService _service;
        private readonly InstanceContext _instance;
        public InstanceApplication(DataService service, InstanceContext instance)
        {
            _service = service;
            _instance = instance;
        }

        public bool IsInitialized()
        {
            return _instance.Id != 0;
        }

        public async Task InitializeAsync()
        {
            InstanceInformation instance = await RegisterAsync();
            _instance.Initialize(instance);
        }

        public async Task<InstanceInformation> RegisterAsync()
        {
            List<int> listOfExistingIds = await _service.Instances.GetAllInstanceIdsAsync();
            HashSet<int> valueRanges = Enumerable.Range(1, 1024).ToHashSet();
            valueRanges.ExceptWith(listOfExistingIds);
            if (valueRanges.Count == 0)
            {
                throw new InvalidOperationException("There is no available instance ID.");
            }

            Random randomGenerator = new();
            int instanceId = valueRanges.OrderBy(i => randomGenerator.Next()).First();

            Instance instance = await _service.Instances.AddAsync(instanceId, Environment.MachineName);
            await _service.SaveAsync();

            return instance.ToInformation();
        }

        public async Task RefreshAsync(int id, InstanceRefreshParameter parameter)
        {
            Instance instance = await _service.Instances.GetAsync(id, parameter.Token);
            instance.Refresh();
            await _service.SaveAsync();
        }

        public async Task CleanupAsync()
        {
            List<Instance> listOfStaleInstances = await _service.GetContext().Instances.Where(i => i.LastRefreshTime < DateTime.UtcNow.AddMinutes(-5)).ToListAsync();
            _service.GetContext().Instances.RemoveRange(listOfStaleInstances);
            await _service.SaveAsync();
        }
    }
}
