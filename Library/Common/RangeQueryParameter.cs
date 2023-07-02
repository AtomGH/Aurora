using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Common
{
    /// <summary>
    /// The parameters used to query a range of resources.
    /// </summary>
    public class RangeQueryParameter
    {
        private long _start = 1;
        /// <summary>
        /// Where to start to query.
        /// </summary>
        public long Start { get => _start; set => _start = value < 1 ? 1 : value; }

        private int _limit = 1000;
        /// <summary>
        /// How many items to query.
        /// </summary>
        public int Limit { get => _limit; set => _limit = value > 1000 ? 1000 : value; }
        /// <summary>
        /// The raw condition string.
        /// </summary>
        public string Condition { get; set; } = string.Empty;
        /// <summary>
        /// The field to sort.
        /// </summary>
        public string Sort { get; set; } = string.Empty;
        /// <summary>
        /// The sort order, true for ascending, false for descending.
        /// </summary>
        public bool Order { get; set; }

        // Use a regex to parse the following query string.
        // entities?condition=((k1=v1&k2=v2)|k3=v3)&sort=k1&order=asc&start=0&limit=10
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        public void ParseQueryString(string query)
        {
            // Use a regex to parse the following query string.
            // entities?condition=((k1=v1&k2=v2)|k3=v3)&sort=k1&order=asc&start=0&limit=10

        }


    }
}
