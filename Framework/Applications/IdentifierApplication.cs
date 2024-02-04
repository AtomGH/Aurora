using Microsoft.AspNetCore.StaticFiles;

namespace Aurora.Framework.Applications
{
    public class IdentifierApplication
    {
        private readonly InstanceContext _instance;

        private readonly bool _lock;
        private long _lastGenerateTime;
        private int _lastGenerateSequence;
        private long _lastGenerateId;
        private long _instanceId;

        private static readonly Int64 _timestampMask  = 0b01111111_11111111_11111111_11111111_11111111_11100000_00000000_00000000;
        private static readonly Int64 _instanceIdMask = 0b00000000_00000000_00000000_00000000_00000000_00011111_11111000_00000000;
        private static readonly Int64 _sequenceMask   = 0b00000000_00000000_00000000_00000000_00000000_00000000_00000111_11111111;

        private static readonly Int64 _timestampLengthMask = ~0b00000000_00000000_00000011_1111111_11111111_11111111_11111111_11111111;

        public IdentifierApplication(InstanceContext instance)
        {
            _instance = instance;
            _instanceId = _instance.Id << 11;
        }

        public long New()
        {
            long timestampComponent = 0;
            long sequenceComponent = 0;

            long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            lock (_instance)
            {
                if (timestamp < _lastGenerateTime)
                {
                    return 0;
                }

                if ((timestamp & _timestampLengthMask) != 0)
                {
                    return 0;
                }

                if (timestamp > _lastGenerateTime)
                {
                    _lastGenerateSequence = 0;
                    _lastGenerateTime = timestamp;
                }

                if (_lastGenerateSequence >= 2048)
                {
                    return 0;
                }

                timestampComponent = timestamp << 21;
                sequenceComponent = _lastGenerateSequence;
                _lastGenerateSequence++;
            }

            return timestampComponent | _instanceId | sequenceComponent;
        }

        public long Get()
        {
            long id = New();
            while (id == 0)
            {
                //Thread.Sleep(1);
                id = New();
            }
            return id;
        }
    }
}
