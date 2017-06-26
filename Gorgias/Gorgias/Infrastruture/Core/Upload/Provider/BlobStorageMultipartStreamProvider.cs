using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Gorgias.Infrastruture.Core.Upload.Provider
{
    public class BlobStorageMultipartStreamProvider : MultipartStreamProvider
    {
        public int ID;
        public String filename;

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            Stream stream = null;
            ContentDispositionHeaderValue contentDisposition = headers.ContentDisposition;
            if (contentDisposition != null)
            {
                if (!String.IsNullOrWhiteSpace(contentDisposition.FileName))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString;
                    string containerName = "images";
                    var extension = contentDisposition.FileName.Split('.');
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);

                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename.ToString() + "." + extension[1].Replace("\"", ""));
                    stream = blob.OpenWrite();
                }
            }
            return stream;
        }
    }
}