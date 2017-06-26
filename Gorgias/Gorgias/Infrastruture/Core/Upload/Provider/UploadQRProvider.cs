using Gorgias.Infrastruture.Core.Upload.Helper;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Gorgias.Infrastruture.Core.Upload.Provider
{
    public static class UploadQRProvider
    {
        public static void UploadQR(Bitmap qr, string filename) {
            // Retrieve reference to a blob ;)
            var blobContainer = BlobHelper.GetBlobContainer();
            var blob = blobContainer.GetBlockBlobReference(filename);
            
            // Set the blob content type
            blob.Properties.ContentType = "image/png";

            MemoryStream newImageStream = new MemoryStream();
            qr.Save(newImageStream, System.Drawing.Imaging.ImageFormat.Png);

            // Reset the Stream to the Beginning before upload
            newImageStream.Seek(0, SeekOrigin.Begin);

            //blob.SetProperties();

            //BlobRequestOptions options = new BlobRequestOptions();
            //options.AccessCondition = AccessCondition.None;

            // Upload image from stream to Blob Storage
            blob.UploadFromStream(newImageStream);

            // Upload file into blob storage, basically copying it from local disk into Azure
            using (var fs = qr)
            {
                //blob.UploadFromStream(fs);
            }

        }
    }
}