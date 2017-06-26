using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Gorgias.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Gorgias.Infrastruture.Core;
using Gorgias.Business.DataTransferObjects.Helper;


namespace Gorgias.Controllers
{   
    [RoutePrefix("api")]
    public class ContentController : ApiControllerBase
    {
        [Route("Content/ContentID/{ContentID}", Name = "GetContentByID")]
        [HttpGet]
        public HttpResponseMessage GetContent(HttpRequestMessage request, int ContentID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ContentDTO result = BusinessLayer.Facades.Facade.ContentFacade().GetContent(ContentID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ContentDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Contents/data", Name = "GetContentsDataTables")]
        [HttpPost]
        public DTResult<ContentDTO> GetContents(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ContentDTO> result = BusinessLayer.Facades.Facade.ContentFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Contents", Name = "GetContentsAll")]
        [HttpGet]
        public HttpResponseMessage GetContents(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ContentDTO> result = BusinessLayer.Facades.Facade.ContentFacade().GetContents();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ContentDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Contents/{page}/{pagesize}", Name = "GetContents")]
        [HttpGet]
        public HttpResponseMessage GetContents(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ContentDTO> result = BusinessLayer.Facades.Facade.ContentFacade().GetContents(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ContentDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("Contents/Album/{AlbumID}/data", Name = "GetContentsDataTablesByAlbumID}")]
        [HttpPost]
        public DTResult<ContentDTO> GetContentsByAlbumID(HttpRequestMessage request, int AlbumID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ContentDTO> result = BusinessLayer.Facades.Facade.ContentFacade().FilterResultByAlbumID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, AlbumID);
            return result;
        }
            
        [Route("Contents/Album/{AlbumID}/{page}/{pagesize}", Name = "GetContentsByAlbum")]
        [HttpGet]
        public HttpResponseMessage GetContentsByAlbumID(HttpRequestMessage request, int AlbumID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ContentDTO> result = BusinessLayer.Facades.Facade.ContentFacade().GetContentsByAlbumID(AlbumID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ContentDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("Content", Name = "ContentInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ContentDTO objContent)
        {
             return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    ContentDTO result = BusinessLayer.Facades.Facade.ContentFacade().Insert(objContent);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ContentDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Content/ContentID/{ContentID}", Name = "DeleteContent")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int ContentID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
                }
                else
                {
                    bool result = BusinessLayer.Facades.Facade.ContentFacade().Delete(ContentID);
                    if (result)
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.OK, "Done");
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                    }                    
                }
                return response;
            });            
        }


        [Route("Content/ContentID/{ContentID}", Name = "UpdateContent")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ContentID, ContentDTO objContent)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
                }
                else
                {
                    bool result = BusinessLayer.Facades.Facade.ContentFacade().Update(ContentID,objContent);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ContentDTO>(HttpStatusCode.OK, objContent);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                    }                             
                }
                return response;
            });                        
        }
    }
}