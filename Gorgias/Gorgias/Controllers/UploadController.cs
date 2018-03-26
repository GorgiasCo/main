using Gorgias.Business.DataTransferObjects;
using Gorgias.Infrastruture.Core;
using Gorgias.Infrastruture.Core.Upload;
using Gorgias.Infrastruture.Core.Upload.Interface;
using Gorgias.Infrastruture.Core.Upload.Model;
using Gorgias.Infrastruture.Core.Upload.Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Gorgias.Controllers
{
    [RoutePrefix("api")]
    public class UploadController : ApiControllerBase
    {
        private readonly IBlobService _service = new BlobService();

        [ResponseType(typeof(List<BlobUploadModel>))]
        [Route("Images", Name = "BlobMultiUploadImages")]
        [HttpPost]
        public async Task<IHttpActionResult> PostBlobUpload(string MasterFileName)
        {
            try
            {
                // This endpoint only supports multipart form data
                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    return StatusCode(HttpStatusCode.UnsupportedMediaType);
                }

                // Call service to perform upload, then check result to return as content
                var result = await _service.UploadBlobs(Request.Content, MasterFileName);
                if (result != null && result.Count > 0)
                {
                    return Ok(result);
                }

                // Otherwise
                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(List<BlobUploadModel>))]
        [Route("Images/Name", Name = "BlobMultiUploadImageWithName")]
        [HttpPost]
        public async Task<IHttpActionResult> PostBlobUploadWithName(string MasterFileName, string ImageName)
        {
            try
            {
                // This endpoint only supports multipart form data
                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    return StatusCode(HttpStatusCode.UnsupportedMediaType);
                }

                // Call service to perform upload, then check result to return as content
                var result = await _service.UploadBlobs(Request.Content, MasterFileName, ImageName);
                if (result != null && result.Count > 0)
                {
                    return Ok(result);
                }

                // Otherwise
                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(List<BlobUploadModel>))]
        [Route("Files/Name", Name = "BlobMultiUploadAllFilesWithName")]
        [HttpPost]
        public async Task<IHttpActionResult> PostBlobUploadWithNameAllFiles(string MasterFileName, string ImageName)
        {
            try
            {
                // This endpoint only supports multipart form data
                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    return StatusCode(HttpStatusCode.UnsupportedMediaType);
                }

                // Call service to perform upload, then check result to return as content
                var result = await _service.UploadBlobsAllFiles(Request.Content, MasterFileName, ImageName);
                if (result != null && result.Count > 0)
                {
                    return Ok(result);
                }

                // Otherwise
                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(List<BlobUploadModel>))]
        [Route("Images/Album", Name = "BlobMultiUploadImagesForAlbums")]
        [HttpPost]
        public async Task<IHttpActionResult> PostBlobUploadAlbums(int AlbumID, string MasterFileName)
        {
            try
            {
                // This endpoint only supports multipart form data
                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    return StatusCode(HttpStatusCode.UnsupportedMediaType);
                }

                // Call service to perform upload, then check result to return as content
                var result = await _service.UploadBlobs(Request.Content, MasterFileName);
                if (result != null && result.Count > 0)
                {
                    foreach (BlobUploadModel obj in result) {
                        ContentDTO objContent = new ContentDTO();
                        objContent.AlbumID = AlbumID;
                        objContent.ContentIsDeleted = false;
                        objContent.ContentStatus = true;
                        objContent.ContentTitle = null;
                        objContent.ContentType = 1;
                        objContent.ContentCreatedDate = DateTime.UtcNow;
                        // System.Configuration.ConfigurationManager.AppSettings["ContentCDN"] Can be used for CDN main Path
                        objContent.ContentURL = obj.FileUrl;                        
                        BusinessLayer.Facades.Facade.ContentFacade().Insert(objContent);
                    }
                    return Ok(result);
                }
                // Otherwise
                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(List<BlobUploadModel>))]
        [Route("Images/Album/Contents", Name = "BlobMultiUploadImagesForAlbumContents")]
        [HttpPost]
        public async Task<IHttpActionResult> PostBlobUploadAlbumContents(int AlbumID, string MasterFileName)
        {
            try
            {
                // This endpoint only supports multipart form data
                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    return StatusCode(HttpStatusCode.UnsupportedMediaType);
                }

                // Call service to perform upload, then check result to return as content
                var result = await _service.UploadBlobs(Request.Content, MasterFileName);                

                if (result != null && result.Count > 0)
                {
                    foreach (BlobUploadModel obj in result)
                    {
                        ContentDTO objContent = new ContentDTO();
                        objContent.AlbumID = AlbumID;
                        objContent.ContentIsDeleted = false;
                        objContent.ContentStatus = true;
                        objContent.ContentTitle = "NA";
                        objContent.ContentType = 1;
                        objContent.ContentCreatedDate = DateTime.UtcNow;
                        // System.Configuration.ConfigurationManager.AppSettings["ContentCDN"] Can be used for CDN main Path
                        objContent.ContentURL = obj.FileUrl;
                        BusinessLayer.Facades.Facade.ContentFacade().Insert(objContent);
                    }
                    return Ok(result);
                }
                // Otherwise
                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [MimeMultipart]
        [Route("Image/Blob", Name = "BlobUploadImage")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostBlobImage(HttpRequestMessage request, string filename, string entityImage)
        {

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                BlobStorageMultipartStreamProvider provider = new BlobStorageMultipartStreamProvider();
                provider.filename = entityImage + "-" + filename;
                if (Request.Content != null)
                {
                    await Request.Content.ReadAsMultipartAsync(provider);
                    foreach (var key in provider.Contents)
                    {
                        if (key.IsFormData())
                        {
                            throw new HttpResponseException(HttpStatusCode.NotImplemented);
                        }                        
                    }
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotAcceptable);
                }

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            // Create response
            FileUploadResult fileUploadResult = new FileUploadResult
            {
                FileName = entityImage + "-" + filename
            };

            var response = request.CreateResponse(HttpStatusCode.OK, fileUploadResult);

            return response;
        }

        [MimeMultipart]
        [Route("Image/Upload")]
        public HttpResponseMessage PostUploadImage(HttpRequestMessage request, int itemID, string foldername)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var uploadPath = HttpContext.Current.Server.MapPath("~/Content/images");

                var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);

                // Read the MIME multipart asynchronously 
                Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

                string _localFileName = multipartFormDataStreamProvider
                    .FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

                // Create response
                FileUploadResult fileUploadResult = new FileUploadResult
                {
                    FileName = Path.GetFileName(_localFileName),
                };

                // update database

                response = request.CreateResponse(HttpStatusCode.OK, fileUploadResult);

                return response;
            });
        }
    }
}
