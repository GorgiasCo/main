using Gorgias.Infrastruture.Core.Upload.Interface;
using Gorgias.Infrastruture.Core.Upload.Model;
using Gorgias.Infrastruture.Core.Upload.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Gorgias.Infrastruture.Core.Upload
{
    public class BlobService : IBlobService
    {
        public async Task<List<BlobUploadModel>> UploadBlobs(HttpContent httpContent, string MasterFileName)
        {
            var blobUploadProvider = new BlobStorageUploadProvider();
            blobUploadProvider.MasterFileName = MasterFileName;
            blobUploadProvider.ImageName = "";

            var list = await httpContent.ReadAsMultipartAsync(blobUploadProvider)
                .ContinueWith(task =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        throw task.Exception;
                    }

                    var provider = task.Result;
                    return provider.Uploads.ToList();
                });

            // TODO: Use data in the list to store blob info in your
            // database so that you can always retrieve it later.

            return list;
        }      

        public async Task<List<BlobUploadModel>> UploadBlobs(HttpContent httpContent, string MasterFileName, string ImageName)
        {
            var blobUploadProvider = new BlobStorageUploadProvider();
            blobUploadProvider.MasterFileName = MasterFileName;
            blobUploadProvider.ImageName = ImageName;

            var list = await httpContent.ReadAsMultipartAsync(blobUploadProvider)
                .ContinueWith(task =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        throw task.Exception;
                    }

                    var provider = task.Result;
                    return provider.Uploads.ToList();
                });

            // TODO: Use data in the list to store blob info in your
            // database so that you can always retrieve it later.

            return list;
        }


    }
}