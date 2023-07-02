using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Common
{
    /// <summary>
    /// Contains the result of a range query.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RangeQueryResult<T>
    {
        /// <summary>
        /// The total quantity of the resources that match the query.
        /// </summary>
        public long TotalQuantity { get; set; }
        /// <summary>
        /// A slice of the query result that is started from an index with limited quantity specified in the query.
        /// </summary>
        public List<T> Values { get; set; }

        /// <summary>
        /// Used to create a range query result.
        /// </summary>
        /// <param name="totalQuantity">The total quantity of the result without considering the specified query index and limit.</param>
        /// <param name="values">The query result with the query index and limit considered.</param>
        public RangeQueryResult(long totalQuantity, List<T> values)
        {
            TotalQuantity = totalQuantity;
            Values = values;
        }
    }
}
