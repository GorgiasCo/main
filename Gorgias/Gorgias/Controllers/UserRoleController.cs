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
    public class UserRoleController : ApiControllerBase
    {
        [Route("UserRole/UserRoleID/{UserRoleID}", Name = "GetUserRoleByID")]
        [HttpGet]
        public HttpResponseMessage GetUserRole(HttpRequestMessage request, int UserRoleID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserRoleDTO result = BusinessLayer.Facades.Facade.UserRoleFacade().GetUserRole(UserRoleID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<UserRoleDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("UserRoles/data", Name = "GetUserRolesDataTables")]
        [HttpPost]
        public DTResult<UserRoleDTO> GetUserRoles(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<UserRoleDTO> result = BusinessLayer.Facades.Facade.UserRoleFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("UserRoles", Name = "GetUserRolesAll")]
        [HttpGet]
        public HttpResponseMessage GetUserRoles(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<UserRoleDTO> result = BusinessLayer.Facades.Facade.UserRoleFacade().GetUserRoles();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<UserRoleDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }

        [Route("UserRoles/Country", Name = "GetUserRolesForCountryAll")]
        [HttpGet]
        public HttpResponseMessage GetUserRolesForCountry(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<UserRoleDTO> result = BusinessLayer.Facades.Facade.UserRoleFacade().GetUserRolesForCountry();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<List<UserRoleDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("UserRoles/{page}/{pagesize}", Name = "GetUserRoles")]
        [HttpGet]
        public HttpResponseMessage GetUserRoles(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<UserRoleDTO> result = BusinessLayer.Facades.Facade.UserRoleFacade().GetUserRoles(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<UserRoleDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("UserRole", Name = "UserRoleInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.UserRoleDTO objUserRole)
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
                    UserRoleDTO result = BusinessLayer.Facades.Facade.UserRoleFacade().Insert(objUserRole);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.UserRoleDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("UserRole/UserRoleID/{UserRoleID}", Name = "DeleteUserRole")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int UserRoleID)
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
                    bool result = BusinessLayer.Facades.Facade.UserRoleFacade().Delete(UserRoleID);
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


        [Route("UserRole/UserRoleID/{UserRoleID}", Name = "UpdateUserRole")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int UserRoleID, UserRoleDTO objUserRole)
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
                    bool result = BusinessLayer.Facades.Facade.UserRoleFacade().Update(UserRoleID,objUserRole);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.UserRoleDTO>(HttpStatusCode.OK, objUserRole);
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