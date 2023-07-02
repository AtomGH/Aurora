using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Accounts
{
    /// <summary>
    /// The parameters used to create an account.
    /// </summary>
    public class CreateAccountParameters
    {
        /// <summary>
        /// The name of the new account.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The type of the new account.
        /// </summary>
        public AccountType Type { get; set; }
    }
}
