using Aurora.Library.Common;
using Aurora.Library.Framework;

namespace Aurora.Framework.Applications
{
    public class IdentifierApplication
    {
        private readonly IdentifierGenerator _identifierGenerator;

        public IdentifierApplication(InstanceContext instance)
        {
            _identifierGenerator = new(instance);
        }

        public long Get()
        {
            long id = _identifierGenerator.New();
            while (id == 0)
            {
                Thread.Sleep(1);
                id = _identifierGenerator.New();
            }
            return id;
        }
    }
}
