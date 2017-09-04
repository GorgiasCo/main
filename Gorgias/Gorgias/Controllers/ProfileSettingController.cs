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
    public class ProfileSettingController : ApiControllerBase
    {
        [Route("ProfileSetting/ProfileID/{ProfileID}", Name = "GetProfileSettingByID")]
        [HttpGet]
        public HttpResponseMessage GetProfileSetting(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProfileSettingDTO result = BusinessLayer.Facades.Facade.ProfileSettingFacade().GetProfileSetting(ProfileID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ProfileSettingDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("ProfileSettings/data", Name = "GetProfileSettingsDataTables")]
        [HttpPost]
        public DTResult<ProfileSettingDTO> GetProfileSettings(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileSettingDTO> result = BusinessLayer.Facades.Facade.ProfileSettingFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ProfileSettings", Name = "GetProfileSettingsAll")]
        [HttpGet]
        public HttpResponseMessage GetProfileSettings(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProfileSettingDTO> result = BusinessLayer.Facades.Facade.ProfileSettingFacade().GetProfileSettings();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ProfileSettingDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("ProfileSettings/{page}/{pagesize}", Name = "GetProfileSettings")]
        [HttpGet]
        public HttpResponseMessage GetProfileSettings(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileSettingDTO> result = BusinessLayer.Facades.Facade.ProfileSettingFacade().GetProfileSettings(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileSettingDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("ProfileSetting", Name = "ProfileSettingInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ProfileSettingDTO objProfileSetting)
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
                    ProfileSettingDTO result = BusinessLayer.Facades.Facade.ProfileSettingFacade().Insert(objProfileSetting);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileSettingDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("ProfileSetting/ProfileID/{ProfileID}", Name = "DeleteProfileSetting")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int ProfileID)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileSettingFacade().Delete(ProfileID);
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


        [Route("ProfileSetting/ProfileID/{ProfileID}", Name = "UpdateProfileSetting")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ProfileID, ProfileSettingDTO objProfileSetting)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileSettingFacade().Update(ProfileID,objProfileSetting);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileSettingDTO>(HttpStatusCode.OK, objProfileSetting);
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