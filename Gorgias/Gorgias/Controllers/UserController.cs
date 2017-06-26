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
    public class UserController : ApiControllerBase
    {
        [Route("User/UserID/{UserID}", Name = "GetUserByID")]
        [HttpGet]
        public HttpResponseMessage GetUser(HttpRequestMessage request, int UserID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserDTO result = BusinessLayer.Facades.Facade.UserFacade().GetUser(UserID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<UserDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("User/Profile/Validity/{UserID}/{ProfileID}", Name = "GetUserProfileValidity")]
        [HttpGet]
        public HttpResponseMessage GetUserProfileValidity(HttpRequestMessage request, int UserID, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.UserFacade().GetUserProfileValidity(UserID,ProfileID);
                if (!result)
                {
                    response = request.CreateResponse<bool>(HttpStatusCode.NotFound, false);
                }
                else
                {
                    response = request.CreateResponse<bool>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("User/Confirmation/{UserID}", Name = "UpdateConfirmationUserByID")]
        [HttpGet]
        public HttpResponseMessage UpdateConfirmation(HttpRequestMessage request, int UserID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.UserFacade().Update(UserID);
                if (!result)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<bool>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Users/data", Name = "GetUsersDataTables")]
        [HttpPost]
        public DTResult<UserDTO> GetUsers(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<UserDTO> result = BusinessLayer.Facades.Facade.UserFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Users/Country/{CountryID}/data", Name = "GetUsersByCountryDataTables")]
        [HttpPost]
        public DTResult<UserDTO> GetUsersByCountry(HttpRequestMessage request, DTParameters param, int CountryID)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<UserDTO> result = BusinessLayer.Facades.Facade.UserFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, CountryID);
            return result;
        }

        [Route("Users", Name = "GetUsersAll")]
        [HttpGet]
        public HttpResponseMessage GetUsers(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<UserDTO> result = BusinessLayer.Facades.Facade.UserFacade().GetUsers();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<UserDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }

        [Route("Users/Country/{CountryID}", Name = "GetUsersByCountryAll")]
        [HttpGet]
        public HttpResponseMessage GetUsers(HttpRequestMessage request, int CountryID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<UserDTO> result = BusinessLayer.Facades.Facade.UserFacade().GetUsers(CountryID);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<List<UserDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Users/{page}/{pagesize}", Name = "GetUsers")]
        [HttpGet]
        public HttpResponseMessage GetUsers(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<UserDTO> result = BusinessLayer.Facades.Facade.UserFacade().GetUsers(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<UserDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("User", Name = "UserInsert")]        
        [HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> Post(HttpRequestMessage request, Business.DataTransferObjects.UserDTO objUser)
        {
            //Edited ;)
            HttpResponseMessage response = null;

            if (!ModelState.IsValid)
            {
                response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                UserDTO result = await BusinessLayer.Facades.Facade.UserFacade().Insert(objUser);
                if (result.UserEmail != null)
                {
                    response = request.CreateResponse<Business.DataTransferObjects.UserDTO>(HttpStatusCode.Created, result);
                }
                else
                {
                    response = request.CreateResponse<String>(HttpStatusCode.BadRequest, null);
                }
            }
            return response;
        }
        

        [Route("User/UserID/{UserID}", Name = "DeleteUser")]        
        public async System.Threading.Tasks.Task<HttpResponseMessage> Delete(HttpRequestMessage request, int UserID)
        {
            //Edited ;)
            HttpResponseMessage response = null;

            if (!ModelState.IsValid)
            {
                response = request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
            }
            else
            {
                bool result = await BusinessLayer.Facades.Facade.UserFacade().Delete(UserID);
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


        [Route("User/UserID/{UserID}", Name = "UpdateUser")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int UserID, UserDTO objUser)
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
                    bool result = BusinessLayer.Facades.Facade.UserFacade().Update(UserID,objUser);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.UserDTO>(HttpStatusCode.OK, objUser);
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