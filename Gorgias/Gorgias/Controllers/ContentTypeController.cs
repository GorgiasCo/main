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
    public class ContentTypeController : ApiControllerBase
    {
        [Route("ContentType/ContentTypeID/{ContentTypeID}", Name = "GetContentTypeByID")]
        [HttpGet]
        public HttpResponseMessage GetContentType(HttpRequestMessage request, int ContentTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ContentTypeDTO result = BusinessLayer.Facades.Facade.ContentTypeFacade().GetContentType(ContentTypeID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ContentTypeDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("ContentTypes/data", Name = "GetContentTypesDataTables")]
        [HttpPost]
        public DTResult<ContentTypeDTO> GetContentTypes(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ContentTypeDTO> result = BusinessLayer.Facades.Facade.ContentTypeFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ContentTypes", Name = "GetContentTypesAll")]
        [HttpGet]
        public HttpResponseMessage GetContentTypes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ContentTypeDTO> result = BusinessLayer.Facades.Facade.ContentTypeFacade().GetContentTypes();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ContentTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("ContentTypes/{page}/{pagesize}", Name = "GetContentTypes")]
        [HttpGet]
        public HttpResponseMessage GetContentTypes(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ContentTypeDTO> result = BusinessLayer.Facades.Facade.ContentTypeFacade().GetContentTypes(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ContentTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("ContentTypes/ContentTypeParent/{ContentTypeParentID}/data", Name = "GetContentTypesDataTablesByContentTypeParentID}")]
        [HttpPost]
        public DTResult<ContentTypeDTO> GetContentTypesByContentTypeParentID(HttpRequestMessage request, int ContentTypeParentID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ContentTypeDTO> result = BusinessLayer.Facades.Facade.ContentTypeFacade().FilterResultByContentTypeParentID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ContentTypeParentID);
            return result;
        }
            
        [Route("ContentTypes/ContentTypeParent/{ContentTypeParentID}/{page}/{pagesize}", Name = "GetContentTypesByContentTypeParent")]
        [HttpGet]
        public HttpResponseMessage GetContentTypesByContentTypeParentID(HttpRequestMessage request, int ContentTypeParentID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ContentTypeDTO> result = BusinessLayer.Facades.Facade.ContentTypeFacade().GetContentTypesByContentTypeParentID(ContentTypeParentID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ContentTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("ContentType", Name = "ContentTypeInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ContentTypeDTO objContentType)
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
                    ContentTypeDTO result = BusinessLayer.Facades.Facade.ContentTypeFacade().Insert(objContentType);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ContentTypeDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("ContentType/ContentTypeID/{ContentTypeID}", Name = "DeleteContentType")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int ContentTypeID)
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
                    bool result = BusinessLayer.Facades.Facade.ContentTypeFacade().Delete(ContentTypeID);
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


        [Route("ContentType/ContentTypeID/{ContentTypeID}", Name = "UpdateContentType")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ContentTypeID, ContentTypeDTO objContentType)
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
                    bool result = BusinessLayer.Facades.Facade.ContentTypeFacade().Update(ContentTypeID,objContentType);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ContentTypeDTO>(HttpStatusCode.OK, objContentType);
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