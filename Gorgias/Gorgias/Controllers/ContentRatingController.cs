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
    public class ContentRatingController : ApiControllerBase
    {
        [Route("ContentRating/ContentRatingID/{ContentRatingID}", Name = "GetContentRatingByID")]
        [HttpGet]
        public HttpResponseMessage GetContentRating(HttpRequestMessage request, int ContentRatingID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ContentRatingDTO result = BusinessLayer.Facades.Facade.ContentRatingFacade().GetContentRating(ContentRatingID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ContentRatingDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("ContentRatings/data", Name = "GetContentRatingsDataTables")]
        [HttpPost]
        public DTResult<ContentRatingDTO> GetContentRatings(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ContentRatingDTO> result = BusinessLayer.Facades.Facade.ContentRatingFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ContentRatings", Name = "GetContentRatingsAll")]
        [HttpGet]
        public HttpResponseMessage GetContentRatings(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ContentRatingDTO> result = BusinessLayer.Facades.Facade.ContentRatingFacade().GetContentRatings();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ContentRatingDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("ContentRatings/{page}/{pagesize}", Name = "GetContentRatings")]
        [HttpGet]
        public HttpResponseMessage GetContentRatings(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ContentRatingDTO> result = BusinessLayer.Facades.Facade.ContentRatingFacade().GetContentRatings(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ContentRatingDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("ContentRatings/ContentRatingParent/{ContentRatingParentID}/data", Name = "GetContentRatingsDataTablesByContentRatingParentID}")]
        [HttpPost]
        public DTResult<ContentRatingDTO> GetContentRatingsByContentRatingParentID(HttpRequestMessage request, int ContentRatingParentID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ContentRatingDTO> result = BusinessLayer.Facades.Facade.ContentRatingFacade().FilterResultByContentRatingParentID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ContentRatingParentID);
            return result;
        }
            
        [Route("ContentRatings/ContentRatingParent/{ContentRatingParentID}/{page}/{pagesize}", Name = "GetContentRatingsByContentRatingParent")]
        [HttpGet]
        public HttpResponseMessage GetContentRatingsByContentRatingParentID(HttpRequestMessage request, int ContentRatingParentID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ContentRatingDTO> result = BusinessLayer.Facades.Facade.ContentRatingFacade().GetContentRatingsByContentRatingParentID(ContentRatingParentID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ContentRatingDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("ContentRating", Name = "ContentRatingInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ContentRatingDTO objContentRating)
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
                    ContentRatingDTO result = BusinessLayer.Facades.Facade.ContentRatingFacade().Insert(objContentRating);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ContentRatingDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("ContentRating/ContentRatingID/{ContentRatingID}", Name = "DeleteContentRating")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int ContentRatingID)
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
                    bool result = BusinessLayer.Facades.Facade.ContentRatingFacade().Delete(ContentRatingID);
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


        [Route("ContentRating/ContentRatingID/{ContentRatingID}", Name = "UpdateContentRating")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ContentRatingID, ContentRatingDTO objContentRating)
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
                    bool result = BusinessLayer.Facades.Facade.ContentRatingFacade().Update(ContentRatingID,objContentRating);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ContentRatingDTO>(HttpStatusCode.OK, objContentRating);
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