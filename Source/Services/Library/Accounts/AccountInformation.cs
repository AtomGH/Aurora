using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Accounts
{
    /// <summary>
    /// Account information.
    /// </summary>
    public class AccountInformation
    {
        /// <summary>
        /// The ID of the account.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The name of the account.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The account type.
        /// </summary>
        public AccountType Type { get; set; }
    }
}
