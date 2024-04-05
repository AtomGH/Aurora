using Aurora.Core.Data;
using Aurora.Library.Framework;

namespace Aurora.Core.Applications
{
    /// <summary>
    /// 
    /// </summary>
    public class InstanceApplication
    {
        private readonly DataService _dataService;
        private readonly FrameworkClient _framework;
        private readonly InstanceContext _instance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataService"></param>
        /// <param name="framework"></param>
        /// <param name="instance"></param>
        public InstanceApplication(DataService dataService, FrameworkClient framework, InstanceContext instance)
        {
            _dataService = dataService;
            _framework = framework;
            _instance = instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<InstanceInformation> RegisterAsync()
        {
            InstanceInformation instance = await _framework.RegisterAsync();
            _instance.Initialize(instance);
            return instance;
        }
    }
}
