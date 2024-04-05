using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentifierResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<long> IDs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public IdentifierResult(List<long> id)
        {
            IDs = id;
        }
    }
}
