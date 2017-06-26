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
    public class ProfileAttributeController : ApiControllerBase
    {
        [Route("ProfileAttribute/AttributeID/ProfileID/{AttributeID}/{ProfileID}", Name = "GetProfileAttributeByID")]
        [HttpGet]
        public HttpResponseMessage GetProfileAttribute(HttpRequestMessage request, int AttributeID, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProfileAttributeDTO result = BusinessLayer.Facades.Facade.ProfileAttributeFacade().GetProfileAttribute(AttributeID, ProfileID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ProfileAttributeDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("ProfileAttributes/data", Name = "GetProfileAttributesDataTables")]
        [HttpPost]
        public DTResult<ProfileAttributeDTO> GetProfileAttributes(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileAttributeDTO> result = BusinessLayer.Facades.Facade.ProfileAttributeFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ProfileAttributes", Name = "GetProfileAttributesAll")]
        [HttpGet]
        public HttpResponseMessage GetProfileAttributes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProfileAttributeDTO> result = BusinessLayer.Facades.Facade.ProfileAttributeFacade().GetProfileAttributes();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ProfileAttributeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("ProfileAttributes/{page}/{pagesize}", Name = "GetProfileAttributes")]
        [HttpGet]
        public HttpResponseMessage GetProfileAttributes(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileAttributeDTO> result = BusinessLayer.Facades.Facade.ProfileAttributeFacade().GetProfileAttributes(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileAttributeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("ProfileAttributes/ProfileID/{ProfileID}/data", Name = "GetProfileAttributesDataTablesByProfileID}")]
        [HttpPost]
        public DTResult<ProfileAttributeDTO> GetProfileAttributesByProfileID(HttpRequestMessage request, int ProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileAttributeDTO> result = BusinessLayer.Facades.Facade.ProfileAttributeFacade().FilterResultByProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileID);
            return result;
        }
            
        [Route("ProfileAttributes/ProfileID/{ProfileID}/{page}/{pagesize}", Name = "GetProfileAttributesByProfileID")]
        [HttpGet]
        public HttpResponseMessage GetProfileAttributesByProfileID(HttpRequestMessage request, int ProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileAttributeDTO> result = BusinessLayer.Facades.Facade.ProfileAttributeFacade().GetProfileAttributesByProfileID(ProfileID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileAttributeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("ProfileAttribute", Name = "ProfileAttributeInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ProfileAttributeDTO objProfileAttribute)
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
                    ProfileAttributeDTO result = BusinessLayer.Facades.Facade.ProfileAttributeFacade().Insert(objProfileAttribute);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileAttributeDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("ProfileAttribute/AttributeID/ProfileID/{AttributeID}/{ProfileID}", Name = "DeleteProfileAttribute")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int AttributeID, int ProfileID)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileAttributeFacade().Delete(AttributeID, ProfileID);
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


        [Route("ProfileAttribute/AttributeID/ProfileID/{AttributeID}/{ProfileID}", Name = "UpdateProfileAttribute")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int AttributeID, int ProfileID, ProfileAttributeDTO objProfileAttribute)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileAttributeFacade().Update(AttributeID, ProfileID,objProfileAttribute);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileAttributeDTO>(HttpStatusCode.OK, objProfileAttribute);
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