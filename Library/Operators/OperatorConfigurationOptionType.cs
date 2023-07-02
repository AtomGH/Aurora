using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Operators
{
    /// <summary>
    /// Used to specify the types of each field in the operator token.
    /// </summary>
    public enum OperatorConfigurationOptionType
    {
        /// <summary>
        /// The field is a numberic integer.
        /// </summary>
        Integer = 1,
        /// <summary>
        /// The field is a string text.
        /// </summary>
        Text = 2,
    }
}
