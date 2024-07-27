using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Common
{
    public interface IStorageObject
    {
        public string Path { get; }
        public Task<Uri> GetPresignedAccessUriAsync(ObjectStoragePermission permissions);
    }
}
