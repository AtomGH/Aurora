using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Library.Accounts
{
    public class AccountInformation
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public AccountType Type { get; set; }
    }
}
