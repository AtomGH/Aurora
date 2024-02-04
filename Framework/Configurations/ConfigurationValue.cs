using System.Text.Json;

namespace Aurora.Framework.Configurations
{
    public class ConfigurationValue<T>
    {
        public T Value { get; set; } = default!;

        public ConfigurationValue(T value)
        {
            Value = value;
        }

        public JsonDocument ToJson()
        {
            return JsonSerializer.SerializeToDocument(this);
        }
    }
}
