using Aurora.Framework.Data.Entities;
using Aurora.Library.Framework;

namespace Aurora.Framework.Data.Extensions
{
    public static class InstanceExtension
    {
        public static InstanceInformation ToInformation(this Instance instance)
        {
            return new InstanceInformation
            {
                Id = instance.Id,
                Token = instance.Token,
                Hostname = instance.Hostname,
                LastRefreshTime = instance.LastRefreshTime
            };
        }
    }
}
