using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Aurora.Library.Common
{
    /// <summary>
    /// An extension for IQueryable to support pagination with large quantity.
    /// </summary>
    public static class LargeQuantityPaginationQueryExtension
    {
        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="count"></param>
        /// <returns>An IQueryable that contains elements that occur after the specified index in the input sequence.</returns>
        public static IQueryable<T> LongSkip<T>(this IQueryable<T> query, long count)
        {
            long remainQuantity = count;
            while (remainQuantity > int.MaxValue)
            {
                query = query.Skip(int.MaxValue);
                remainQuantity -= int.MaxValue;
            }
            int whatsLeftToSkipWithinIntRange = Convert.ToInt32(remainQuantity);
            return query.Skip(whatsLeftToSkipWithinIntRange);
        }
    }
}
