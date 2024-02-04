using Aurora.Framework.Data;
using Aurora.Framework.Data.Entities;

namespace Aurora.Framework.Applications
{
    public class InstanceApplication
    {
        private readonly DataService _service;
        public InstanceApplication(DataService service)
        {
            _service = service;
        }

        public async Task Register(IServiceCollection services)
        {
            List<int> listOfExistingIds = await _service.Instances.GetAllInstanceIdsAsync();
            HashSet<int> valueRanges = Enumerable.Range(1, 1024).ToHashSet();
            valueRanges.ExceptWith(listOfExistingIds);
            if (valueRanges.Count == 0)
            {
                throw new InvalidOperationException("There are no available instance IDs.");
            }

            Random randomGenerator = new();
            int instanceId = valueRanges.OrderBy(i => randomGenerator.Next()).First();

            Instance instance = await _service.Instances.AddAsync(instanceId, Environment.MachineName);
            InstanceContext context = new(instance);
            services.AddSingleton(context);
        }
    }
}
