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
    public class ProfileTagController : ApiControllerBase
    {
        [Route("ProfileTag/TagID/ProfileID/{TagID}/{ProfileID}", Name = "GetProfileTagByID")]
        [HttpGet]
        public HttpResponseMessage GetProfileTag(HttpRequestMessage request, int TagID, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProfileTagDTO result = BusinessLayer.Facades.Facade.ProfileTagFacade().GetProfileTag(TagID, ProfileID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ProfileTagDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("ProfileTags/data", Name = "GetProfileTagsDataTables")]
        [HttpPost]
        public DTResult<ProfileTagDTO> GetProfileTags(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileTagDTO> result = BusinessLayer.Facades.Facade.ProfileTagFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ProfileTags", Name = "GetProfileTagsAll")]
        [HttpGet]
        public HttpResponseMessage GetProfileTags(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProfileTagDTO> result = BusinessLayer.Facades.Facade.ProfileTagFacade().GetProfileTags();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ProfileTagDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("ProfileTags/{page}/{pagesize}", Name = "GetProfileTags")]
        [HttpGet]
        public HttpResponseMessage GetProfileTags(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileTagDTO> result = BusinessLayer.Facades.Facade.ProfileTagFacade().GetProfileTags(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileTagDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("ProfileTags/ProfileID/{ProfileID}/data", Name = "GetProfileTagsDataTablesByProfileID}")]
        [HttpPost]
        public DTResult<ProfileTagDTO> GetProfileTagsByProfileID(HttpRequestMessage request, int ProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileTagDTO> result = BusinessLayer.Facades.Facade.ProfileTagFacade().FilterResultByProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileID);
            return result;
        }
            
        [Route("ProfileTags/ProfileID/{ProfileID}/{page}/{pagesize}", Name = "GetProfileTagsByProfileID")]
        [HttpGet]
        public HttpResponseMessage GetProfileTagsByProfileID(HttpRequestMessage request, int ProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileTagDTO> result = BusinessLayer.Facades.Facade.ProfileTagFacade().GetProfileTagsByProfileID(ProfileID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileTagDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("ProfileTag", Name = "ProfileTagInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ProfileTagDTO objProfileTag)
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
                    ProfileTagDTO result = BusinessLayer.Facades.Facade.ProfileTagFacade().Insert(objProfileTag);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileTagDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("ProfileTag/TagID/ProfileID/{TagID}/{ProfileID}", Name = "DeleteProfileTag")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int TagID, int ProfileID)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileTagFacade().Delete(TagID, ProfileID);
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


        [Route("ProfileTag/TagID/ProfileID/{TagID}/{ProfileID}", Name = "UpdateProfileTag")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int TagID, int ProfileID, ProfileTagDTO objProfileTag)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileTagFacade().Update(TagID, ProfileID,objProfileTag);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileTagDTO>(HttpStatusCode.OK, objProfileTag);
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