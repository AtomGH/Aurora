using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Framework.Library.Common
{
    public static class LargeQuantityPaginationQueryExtension
    {
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
