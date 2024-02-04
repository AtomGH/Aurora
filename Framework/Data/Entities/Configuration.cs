using Aurora.Framework.Configurations;
using System.Text.Json;

namespace Aurora.Framework.Data.Entities
{
    public class Configuration
    {
        public ConfigurationKey Key { get; }
        public JsonDocument Value { get; private set; }

        public Configuration(ConfigurationKey key, JsonDocument value)
        {
            Key = key;
            Value = value;
        }

        public void Update(JsonDocument value)
        {
            Value = value;
        }
    }
}
