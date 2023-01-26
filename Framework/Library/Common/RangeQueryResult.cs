using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Library.Common
{
    public class RangeQueryResult<T>
    {
        public long TotalQuantity { get; set; }
        public List<T> Values { get; set; }

        public RangeQueryResult(long totalQuantity, List<T> values)
        {
            TotalQuantity = totalQuantity;
            Values = values;
        }
    }
}
