using Gorgias.Infrastruture.Core.Upload.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Gorgias.Infrastruture.Core.Upload.Interface
{
    public interface IBlobService
    {
        Task<List<BlobUploadModel>> UploadBlobs(HttpContent httpContent, string MasterFileName);
        Task<List<BlobUploadModel>> UploadBlobs(HttpContent httpContent, string MasterFileName, string ImageName);
    }
}