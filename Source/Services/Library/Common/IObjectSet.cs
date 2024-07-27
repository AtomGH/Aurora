using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Common
{
    public interface IObjectSet
    {
        public Task UploadObjectAsync(Uri uri, Stream content);
        public Task<Uri> GetPresignedObjectUriAsync(Uri objectUri, ObjectStoragePermission permissions);
    }
}
