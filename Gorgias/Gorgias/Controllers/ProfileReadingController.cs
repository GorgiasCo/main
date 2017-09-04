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
    public class ProfileReadingController : ApiControllerBase
    {
        [Route("ProfileReading/ProfileReadingID/{ProfileReadingID}", Name = "GetProfileReadingByID")]
        [HttpGet]
        public HttpResponseMessage GetProfileReading(HttpRequestMessage request, int ProfileReadingID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProfileReadingDTO result = BusinessLayer.Facades.Facade.ProfileReadingFacade().GetProfileReading(ProfileReadingID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ProfileReadingDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("ProfileReadings/data", Name = "GetProfileReadingsDataTables")]
        [HttpPost]
        public DTResult<ProfileReadingDTO> GetProfileReadings(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileReadingDTO> result = BusinessLayer.Facades.Facade.ProfileReadingFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ProfileReadings", Name = "GetProfileReadingsAll")]
        [HttpGet]
        public HttpResponseMessage GetProfileReadings(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProfileReadingDTO> result = BusinessLayer.Facades.Facade.ProfileReadingFacade().GetProfileReadings();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ProfileReadingDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("ProfileReadings/{page}/{pagesize}", Name = "GetProfileReadings")]
        [HttpGet]
        public HttpResponseMessage GetProfileReadings(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileReadingDTO> result = BusinessLayer.Facades.Facade.ProfileReadingFacade().GetProfileReadings(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileReadingDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("ProfileReadings/Profile/{ProfileID}/data", Name = "GetProfileReadingsDataTablesByProfileID}")]
        [HttpPost]
        public DTResult<ProfileReadingDTO> GetProfileReadingsByProfileID(HttpRequestMessage request, int ProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileReadingDTO> result = BusinessLayer.Facades.Facade.ProfileReadingFacade().FilterResultByProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileID);
            return result;
        }
            
        [Route("ProfileReadings/Profile/{ProfileID}/{page}/{pagesize}", Name = "GetProfileReadingsByProfile")]
        [HttpGet]
        public HttpResponseMessage GetProfileReadingsByProfileID(HttpRequestMessage request, int ProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileReadingDTO> result = BusinessLayer.Facades.Facade.ProfileReadingFacade().GetProfileReadingsByProfileID(ProfileID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileReadingDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("ProfileReading", Name = "ProfileReadingInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ProfileReadingDTO objProfileReading)
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
                    ProfileReadingDTO result = BusinessLayer.Facades.Facade.ProfileReadingFacade().Insert(objProfileReading);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileReadingDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("ProfileReading/ProfileReadingID/{ProfileReadingID}", Name = "DeleteProfileReading")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int ProfileReadingID)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileReadingFacade().Delete(ProfileReadingID);
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


        [Route("ProfileReading/ProfileReadingID/{ProfileReadingID}", Name = "UpdateProfileReading")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ProfileReadingID, ProfileReadingDTO objProfileReading)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileReadingFacade().Update(ProfileReadingID,objProfileReading);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileReadingDTO>(HttpStatusCode.OK, objProfileReading);
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