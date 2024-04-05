using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public class InstanceInformation
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid Token { get; set; }
        public string Hostname { get; set; }
        public DateTime LastRefreshTime { get; set; }
    }
}
