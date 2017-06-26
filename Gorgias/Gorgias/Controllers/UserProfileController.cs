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
    public class UserProfileController : ApiControllerBase
    {
        [Route("UserProfile/ProfileID/UserRoleID/UserID/{ProfileID}/{UserRoleID}/{UserID}", Name = "GetUserProfileByID")]
        [HttpGet]
        public HttpResponseMessage GetUserProfile(HttpRequestMessage request, int ProfileID, int UserRoleID, int UserID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserProfileDTO result = BusinessLayer.Facades.Facade.UserProfileFacade().GetUserProfile(ProfileID, UserRoleID, UserID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<UserProfileDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("UserProfiles/data", Name = "GetUserProfilesDataTables")]
        [HttpPost]
        public DTResult<UserProfileDTO> GetUserProfiles(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<UserProfileDTO> result = BusinessLayer.Facades.Facade.UserProfileFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("UserProfiles/Country/{CountryID}/data", Name = "GetUserProfilesByCountryDataTables")]
        [HttpPost]
        public DTResult<UserProfileDTO> GetUserProfilesByCountry(HttpRequestMessage request, DTParameters param, int CountryID)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<UserProfileDTO> result = BusinessLayer.Facades.Facade.UserProfileFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, CountryID);
            return result;
        }

        [Route("UserProfiles", Name = "GetUserProfilesAll")]
        [HttpGet]
        public HttpResponseMessage GetUserProfiles(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<UserProfileDTO> result = BusinessLayer.Facades.Facade.UserProfileFacade().GetUserProfiles();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<UserProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("UserProfiles/{page}/{pagesize}", Name = "GetUserProfiles")]
        [HttpGet]
        public HttpResponseMessage GetUserProfiles(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<UserProfileDTO> result = BusinessLayer.Facades.Facade.UserProfileFacade().GetUserProfiles(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<UserProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("UserProfiles/UserRoleID/{UserRoleID}/data", Name = "GetUserProfilesDataTablesByUserRoleID}")]
        [HttpPost]
        public DTResult<UserProfileDTO> GetUserProfilesByUserRoleID(HttpRequestMessage request, int UserRoleID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<UserProfileDTO> result = BusinessLayer.Facades.Facade.UserProfileFacade().FilterResultByUserRoleID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, UserRoleID);
            return result;
        }
            
        [Route("UserProfiles/UserRoleID/{UserRoleID}/{page}/{pagesize}", Name = "GetUserProfilesByUserRoleID")]
        [HttpGet]
        public HttpResponseMessage GetUserProfilesByUserRoleID(HttpRequestMessage request, int UserRoleID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<UserProfileDTO> result = BusinessLayer.Facades.Facade.UserProfileFacade().GetUserProfilesByUserRoleID(UserRoleID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<UserProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        [Route("UserProfiles/UserID/{UserID}/data", Name = "GetUserProfilesDataTablesByUserID}")]
        [HttpPost]
        public DTResult<UserProfileDTO> GetUserProfilesByUserID(HttpRequestMessage request, int UserID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<UserProfileDTO> result = BusinessLayer.Facades.Facade.UserProfileFacade().FilterResultByUserID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, UserID);
            return result;
        }
            
        [Route("UserProfiles/UserID/{UserID}/{page}/{pagesize}", Name = "GetUserProfilesByUserID")]
        [HttpGet]
        public HttpResponseMessage GetUserProfilesByUserID(HttpRequestMessage request, int UserID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<UserProfileDTO> result = BusinessLayer.Facades.Facade.UserProfileFacade().GetUserProfilesByUserID(UserID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<UserProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("UserProfile", Name = "UserProfileInsert")]        
        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> Post(HttpRequestMessage request, Business.DataTransferObjects.UserProfileDTO objUserProfile)
        {
            // return CreateHttpResponse(request, () =>
            //{
            //    HttpResponseMessage response = null;

            //    if (!ModelState.IsValid)
            //    {
            //        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            //    }
            //    else
            //    {
            //        UserProfileDTO result = await BusinessLayer.Facades.Facade.UserProfileFacade().Insert(objUserProfile);
            //        if (result != null)
            //        {
            //            response = request.CreateResponse<Business.DataTransferObjects.UserProfileDTO>(HttpStatusCode.Created, result);
            //        }
            //        else
            //        {
            //            response = request.CreateResponse<String>(HttpStatusCode.Found, null);
            //        }
            //    }
            //    return response;
            //});

            if (!ModelState.IsValid)
            {
                return NotFound();
                //response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                UserProfileDTO result = await BusinessLayer.Facades.Facade.UserProfileFacade().Insert(objUserProfile);
                if (result != null)
                {
                    return Ok();
                    //response = request.CreateResponse<Business.DataTransferObjects.UserProfileDTO>(HttpStatusCode.Created, result);
                }
                else
                {
                    return NotFound();
                    //response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                }
            }

        }


        [Route("UserProfile/ProfileID/UserRoleID/UserID/{ProfileID}/{UserRoleID}/{UserID}", Name = "DeleteUserProfile")]        
        public async System.Threading.Tasks.Task<HttpResponseMessage> Delete(HttpRequestMessage request, int ProfileID, int UserRoleID, int UserID)
        {
            HttpResponseMessage response = null;

            if (!ModelState.IsValid)
            {
                response = request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
            }
            else
            {
                bool result = await BusinessLayer.Facades.Facade.UserProfileFacade().Delete(ProfileID, UserRoleID, UserID);
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
        }


        [Route("UserProfile/ProfileID/UserRoleID/UserID/{ProfileID}/{UserRoleID}/{UserID}", Name = "UpdateUserProfile")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ProfileID, int UserRoleID, int UserID, UserProfileDTO objUserProfile)
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
                    bool result = BusinessLayer.Facades.Facade.UserProfileFacade().Update(ProfileID, UserRoleID, UserID,objUserProfile);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.UserProfileDTO>(HttpStatusCode.OK, objUserProfile);
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