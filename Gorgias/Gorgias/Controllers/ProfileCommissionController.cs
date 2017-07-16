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
    public class ProfileCommissionController : ApiControllerBase
    {
        [Route("ProfileCommission/ProfileCommissionID/{ProfileCommissionID}", Name = "GetProfileCommissionByID")]
        [HttpGet]
        public HttpResponseMessage GetProfileCommission(HttpRequestMessage request, int ProfileCommissionID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProfileCommissionDTO result = BusinessLayer.Facades.Facade.ProfileCommissionFacade().GetProfileCommission(ProfileCommissionID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ProfileCommissionDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("ProfileCommissions/data", Name = "GetProfileCommissionsDataTables")]
        [HttpPost]
        public DTResult<ProfileCommissionDTO> GetProfileCommissions(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileCommissionDTO> result = BusinessLayer.Facades.Facade.ProfileCommissionFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ProfileCommissions", Name = "GetProfileCommissionsAll")]
        [HttpGet]
        public HttpResponseMessage GetProfileCommissions(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProfileCommissionDTO> result = BusinessLayer.Facades.Facade.ProfileCommissionFacade().GetProfileCommissions();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ProfileCommissionDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("ProfileCommissions/{page}/{pagesize}", Name = "GetProfileCommissions")]
        [HttpGet]
        public HttpResponseMessage GetProfileCommissions(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileCommissionDTO> result = BusinessLayer.Facades.Facade.ProfileCommissionFacade().GetProfileCommissions(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileCommissionDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("ProfileCommissions/Profile/{ProfileID}/data", Name = "GetProfileCommissionsDataTablesByProfileID}")]
        [HttpPost]
        public DTResult<ProfileCommissionDTO> GetProfileCommissionsByProfileID(HttpRequestMessage request, int ProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileCommissionDTO> result = BusinessLayer.Facades.Facade.ProfileCommissionFacade().FilterResultByProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileID);
            return result;
        }
            
        [Route("ProfileCommissions/Profile/{ProfileID}/{page}/{pagesize}", Name = "GetProfileCommissionsByProfile")]
        [HttpGet]
        public HttpResponseMessage GetProfileCommissionsByProfileID(HttpRequestMessage request, int ProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileCommissionDTO> result = BusinessLayer.Facades.Facade.ProfileCommissionFacade().GetProfileCommissionsByProfileID(ProfileID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileCommissionDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        [Route("ProfileCommissions/User/{UserID}/data", Name = "GetProfileCommissionsDataTablesByUserID}")]
        [HttpPost]
        public DTResult<ProfileCommissionDTO> GetProfileCommissionsByUserID(HttpRequestMessage request, int UserID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileCommissionDTO> result = BusinessLayer.Facades.Facade.ProfileCommissionFacade().FilterResultByUserID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, UserID);
            return result;
        }
            
        [Route("ProfileCommissions/User/{UserID}/{page}/{pagesize}", Name = "GetProfileCommissionsByUser")]
        [HttpGet]
        public HttpResponseMessage GetProfileCommissionsByUserID(HttpRequestMessage request, int UserID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileCommissionDTO> result = BusinessLayer.Facades.Facade.ProfileCommissionFacade().GetProfileCommissionsByUserID(UserID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileCommissionDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        [Route("ProfileCommissions/UserRole/{UserRoleID}/data", Name = "GetProfileCommissionsDataTablesByUserRoleID}")]
        [HttpPost]
        public DTResult<ProfileCommissionDTO> GetProfileCommissionsByUserRoleID(HttpRequestMessage request, int UserRoleID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileCommissionDTO> result = BusinessLayer.Facades.Facade.ProfileCommissionFacade().FilterResultByUserRoleID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, UserRoleID);
            return result;
        }
            
        [Route("ProfileCommissions/UserRole/{UserRoleID}/{page}/{pagesize}", Name = "GetProfileCommissionsByUserRole")]
        [HttpGet]
        public HttpResponseMessage GetProfileCommissionsByUserRoleID(HttpRequestMessage request, int UserRoleID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileCommissionDTO> result = BusinessLayer.Facades.Facade.ProfileCommissionFacade().GetProfileCommissionsByUserRoleID(UserRoleID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileCommissionDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("ProfileCommission", Name = "ProfileCommissionInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ProfileCommissionDTO objProfileCommission)
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
                    ProfileCommissionDTO result = BusinessLayer.Facades.Facade.ProfileCommissionFacade().Insert(objProfileCommission);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileCommissionDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("ProfileCommission/ProfileCommissionID/{ProfileCommissionID}", Name = "DeleteProfileCommission")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int ProfileCommissionID)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileCommissionFacade().Delete(ProfileCommissionID);
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


        [Route("ProfileCommission/ProfileCommissionID/{ProfileCommissionID}", Name = "UpdateProfileCommission")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ProfileCommissionID, ProfileCommissionDTO objProfileCommission)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileCommissionFacade().Update(ProfileCommissionID,objProfileCommission);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileCommissionDTO>(HttpStatusCode.OK, objProfileCommission);
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