using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Common
{
    [Flags]
    public enum ObjectStoragePermission
    {
        None = 0,
        Read = 1,
        Write = 2,
        Create = 4
    }
}
