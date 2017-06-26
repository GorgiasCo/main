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
    public class ProfileTypeController : ApiControllerBase
    {
        [Route("ProfileType/ProfileTypeID/{ProfileTypeID}", Name = "GetProfileTypeByID")]
        [HttpGet]
        public HttpResponseMessage GetProfileType(HttpRequestMessage request, int ProfileTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProfileTypeDTO result = BusinessLayer.Facades.Facade.ProfileTypeFacade().GetProfileType(ProfileTypeID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ProfileTypeDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("ProfileTypes/data", Name = "GetProfileTypesDataTables")]
        [HttpPost]
        public DTResult<ProfileTypeDTO> GetProfileTypes(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileTypeDTO> result = BusinessLayer.Facades.Facade.ProfileTypeFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ProfileTypes", Name = "GetProfileTypesAll")]
        [HttpGet]
        public HttpResponseMessage GetProfileTypes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProfileTypeDTO> result = BusinessLayer.Facades.Facade.ProfileTypeFacade().GetProfileTypes();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ProfileTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("ProfileTypes/{page}/{pagesize}", Name = "GetProfileTypes")]
        [HttpGet]
        public HttpResponseMessage GetProfileTypes(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileTypeDTO> result = BusinessLayer.Facades.Facade.ProfileTypeFacade().GetProfileTypes(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("ProfileType", Name = "ProfileTypeInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ProfileTypeDTO objProfileType)
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
                    ProfileTypeDTO result = BusinessLayer.Facades.Facade.ProfileTypeFacade().Insert(objProfileType);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileTypeDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("ProfileType/ProfileTypeID/{ProfileTypeID}", Name = "DeleteProfileType")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int ProfileTypeID)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileTypeFacade().Delete(ProfileTypeID);
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


        [Route("ProfileType/ProfileTypeID/{ProfileTypeID}", Name = "UpdateProfileType")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ProfileTypeID, ProfileTypeDTO objProfileType)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileTypeFacade().Update(ProfileTypeID,objProfileType);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileTypeDTO>(HttpStatusCode.OK, objProfileType);
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