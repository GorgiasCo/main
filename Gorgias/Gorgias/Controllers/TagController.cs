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
    public class TagController : ApiControllerBase
    {
        [Route("Tag/TagID/{TagID}", Name = "GetTagByID")]
        [HttpGet]
        public HttpResponseMessage GetTag(HttpRequestMessage request, int TagID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                TagDTO result = BusinessLayer.Facades.Facade.TagFacade().GetTag(TagID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<TagDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Tags/data", Name = "GetTagsDataTables")]
        [HttpPost]
        public DTResult<TagDTO> GetTags(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<TagDTO> result = BusinessLayer.Facades.Facade.TagFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Tags", Name = "GetTagsAll")]
        [HttpGet]
        public HttpResponseMessage GetTags(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<TagDTO> result = BusinessLayer.Facades.Facade.TagFacade().GetTags();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<TagDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Tags/{page}/{pagesize}", Name = "GetTags")]
        [HttpGet]
        public HttpResponseMessage GetTags(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<TagDTO> result = BusinessLayer.Facades.Facade.TagFacade().GetTags(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<TagDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("Tag", Name = "TagInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.TagDTO objTag)
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
                    TagDTO result = BusinessLayer.Facades.Facade.TagFacade().Insert(objTag);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.TagDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Tag/TagID/{TagID}", Name = "DeleteTag")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int TagID)
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
                    bool result = BusinessLayer.Facades.Facade.TagFacade().Delete(TagID);
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


        [Route("Tag/TagID/{TagID}", Name = "UpdateTag")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int TagID, TagDTO objTag)
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
                    bool result = BusinessLayer.Facades.Facade.TagFacade().Update(TagID,objTag);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.TagDTO>(HttpStatusCode.OK, objTag);
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