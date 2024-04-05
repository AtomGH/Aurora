using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Framework.Accounts
{
    public class CreateAccountParameter
    {
        public string Name { get; set; } = string.Empty;
        public AccountType Type { get; set; }
    }
}
