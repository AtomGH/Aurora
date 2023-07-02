using Aurora.Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Aurora.Library.Assets
{
    /// <summary>
    /// The parameters used to query assets.
    /// </summary>
    public class QueryAssetParameters : RangeQueryParameter
    {
        /// <summary>
        /// The name as a filter condition to query, empty string means no filter.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The type ID as a filter condition to query, 0 means no filter.
        /// </summary>
        public long TypeId { get; set; } = 0;
        /// <summary>
        /// The project ID as a filter condition to query, 0 means no filter.
        /// </summary>
        public long ProjectId { get; set; } = 0;
        /// <summary>
        /// The operator ID as a filter condition to query, 0 means no filter.
        /// </summary>
        public long OperatorId { get; set; } = 0;

        /// <summary>
        /// Convert to a HTTP query string.
        /// </summary>
        /// <returns></returns>
        public string ToQueryString()
        {
            string queryString = string.Empty;
            queryString += $"start={Start}&";
            queryString += $"limit={Limit}&";
            queryString += $"name={Name}&";
            queryString += $"typeId={TypeId}&";
            queryString += $"projectId={ProjectId}&";
            queryString += $"operatorId={OperatorId}&";
            queryString = queryString.TrimEnd('&');
            return HttpUtility.UrlEncode(queryString) ?? string.Empty;
        }
    }
}
