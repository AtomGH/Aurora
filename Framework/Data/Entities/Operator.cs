using Aurora.Library.Operators;
using System.Security.AccessControl;

namespace Aurora.Framework.Data.Entities
{
    /// <summary>
    /// The operator entity.
    /// </summary>
    public class Operator
    {
        /// <summary>
        /// The ID of the operator.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The name of the operator.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The descriptin that describes the operator.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The operator version that used to control API compatibility.
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// The resource types this operator can serve.
        /// </summary>
        public List<ResourceType> Services { get; set; }
        /// <summary>
        /// The options that can be configured for this operator.
        /// </summary>
        public List<OperatorConfigurationOption> Options { get; set; }
        /// <summary>
        /// The configuration value of the operator.
        /// </summary>
        public Dictionary<string, object> Configuration { get; set; }
        /// <summary>
        /// The URI address used to access the operator.
        /// </summary>
        public Uri Address { get; set; }

        /// <summary>
        /// Instantiate a operator.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="version"></param>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        /// <param name="address"></param>
        public Operator(string name, string description, int version, List<ResourceType> services, List<OperatorConfigurationOption> options, Dictionary<string, object> configuration, Uri address)
        {
            Name = name;
            Description = description;
            Version = version;
            Services = services;
            Options = options;
            Configuration = configuration;
            Address = address;
        }
    }

    /// <summary>
    /// The operator configuration option.
    /// </summary>
    public class OperatorConfigurationOption
    {
        /// <summary>
        /// The ID of the option.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The operator this option belongs to.
        /// </summary>
        public Operator Operator { get; set; }
        /// <summary>
        /// The configuration key.
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// The configuration value type.
        /// </summary>
        public OperatorConfigurationOptionType Type { get; set; }

        /// <summary>
        /// Instantiate an option.
        /// </summary>
        /// <param name="op"></param>
        /// <param name="key"></param>
        /// <param name="type"></param>
        public OperatorConfigurationOption(Operator op,  string key, OperatorConfigurationOptionType type)
        {
            Operator = op;
            Key = key;
            Type = type;
        }
    }
}
