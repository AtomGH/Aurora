using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Accounts
{
    /// <summary>
    /// Account type.
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Invalid.
        /// </summary>
        None = 0,
        /// <summary>
        /// The account is for human user.
        /// </summary>
        User = 1,
        /// <summary>
        /// The account if for program user.
        /// </summary>
        Service = 2,
    }
}
