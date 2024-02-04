using Aurora.Framework.Data.Services;

namespace Aurora.Framework.Data
{
    public class DataService
    {
        public InstanceService Instances { get; }

        public DataService(DatabaseContext context)
        {
            Instances = new(context);
        }
    }
}
