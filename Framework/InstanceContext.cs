using Aurora.Framework.Data.Entities;

namespace Aurora.Framework
{
    public class InstanceContext
    {
        public int Id { get; }
        public Guid Token { get; }

        public InstanceContext(Instance instance)
        {
            Id = instance.Id;
            Token = instance.Token;
        } 
    }
}
