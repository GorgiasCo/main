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
using Gorgias.Business.DataTransferObjects.Web;

namespace Gorgias.Controllers
{   
    [RoutePrefix("api")]
    [Authorize]
    public class ProfileController : ApiControllerBase
    {
        [Route("Profile/ProfileID/{ProfileID}", Name = "GetProfileByID")]
        [HttpGet]
        public HttpResponseMessage GetProfile(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProfileDTO result = BusinessLayer.Facades.Facade.ProfileFacade().GetProfile(ProfileID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ProfileDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Profile/Listed/{ProfileID}", Name = "GetListedProfileByID")]
        [HttpGet]
        public HttpResponseMessage GetTestProfile(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().GetListedProfile(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Profiles/data", Name = "GetProfilesDataTables")]
        [HttpPost]
        public DTResult<ProfileDTO> GetProfiles(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Profiles/Country/{CountryID}/data", Name = "GetProfilesByCountryDataTables")]
        [HttpPost]
        public DTResult<ProfileDTO> GetProfilesByCountry(HttpRequestMessage request, DTParameters param, int CountryID)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, CountryID);
            return result;
        }

        [Route("Profiles", Name = "GetProfilesAll")]
        [HttpGet]
        public HttpResponseMessage GetProfiles(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().GetProfiles();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }

        [Route("Profiles/Country/{CountryID}", Name = "GetProfilesByCountryAll")]
        [HttpGet]
        public HttpResponseMessage GetProfiles(HttpRequestMessage request, int CountryID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().GetProfiles(CountryID);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<List<ProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Profiles/{page}/{pagesize}", Name = "GetProfiles")]
        [HttpGet]
        public HttpResponseMessage GetProfiles(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().GetProfiles(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("Profiles/Industry/{IndustryID}/data", Name = "GetProfilesDataTablesByIndustryID}")]
        [HttpPost]
        public DTResult<ProfileDTO> GetProfilesByIndustryID(HttpRequestMessage request, int IndustryID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().FilterResultByIndustryID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, IndustryID);
            return result;
        }
            
        [Route("Profiles/Industry/{IndustryID}/{page}/{pagesize}", Name = "GetProfilesByIndustry")]
        [HttpGet]
        public HttpResponseMessage GetProfilesByIndustryID(HttpRequestMessage request, int IndustryID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().GetProfilesByIndustryID(IndustryID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        [Route("Profiles/ProfileType/{ProfileTypeID}/data", Name = "GetProfilesDataTablesByProfileTypeID}")]
        [HttpPost]
        public DTResult<ProfileDTO> GetProfilesByProfileTypeID(HttpRequestMessage request, int ProfileTypeID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().FilterResultByProfileTypeID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileTypeID);
            return result;
        }
            
        [Route("Profiles/ProfileType/{ProfileTypeID}/{page}/{pagesize}", Name = "GetProfilesByProfileType")]
        [HttpGet]
        public HttpResponseMessage GetProfilesByProfileTypeID(HttpRequestMessage request, int ProfileTypeID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().GetProfilesByProfileTypeID(ProfileTypeID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        [Route("Profiles/Theme/{ThemeID}/data", Name = "GetProfilesDataTablesByThemeID}")]
        [HttpPost]
        public DTResult<ProfileDTO> GetProfilesByThemeID(HttpRequestMessage request, int ThemeID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().FilterResultByThemeID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ThemeID);
            return result;
        }
            
        [Route("Profiles/Theme/{ThemeID}/{page}/{pagesize}", Name = "GetProfilesByTheme")]
        [HttpGet]
        public HttpResponseMessage GetProfilesByThemeID(HttpRequestMessage request, int ThemeID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().GetProfilesByThemeID(ThemeID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        [Route("Profiles/SubscriptionType/{SubscriptionTypeID}/data", Name = "GetProfilesDataTablesBySubscriptionTypeID}")]
        [HttpPost]
        public DTResult<ProfileDTO> GetProfilesBySubscriptionTypeID(HttpRequestMessage request, int SubscriptionTypeID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().FilterResultBySubscriptionTypeID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, SubscriptionTypeID);
            return result;
        }
            
        [Route("Profiles/SubscriptionType/{SubscriptionTypeID}/{page}/{pagesize}", Name = "GetProfilesBySubscriptionType")]
        [HttpGet]
        public HttpResponseMessage GetProfilesBySubscriptionTypeID(HttpRequestMessage request, int SubscriptionTypeID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileDTO> result = BusinessLayer.Facades.Facade.ProfileFacade().GetProfilesBySubscriptionTypeID(SubscriptionTypeID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("Profile", Name = "ProfileInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ProfileDTO objProfile)
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
                    ProfileDTO result = BusinessLayer.Facades.Facade.ProfileFacade().Insert(objProfile);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Profile/ProfileID/{ProfileID}", Name = "DeleteProfile")]        
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
                    bool result = BusinessLayer.Facades.Facade.ProfileFacade().Delete(ProfileID);
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

        [Route("Profile/Confirm/ProfileID/{ProfileID}", Name = "UpdateProfileIsConfirm")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ProfileID, ProfileUpdate objProfile)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileFacade().Update(ProfileID, objProfile.Status, objProfile.UpdateMode);
                    if (result)
                    {
                        response = request.CreateResponse<bool>(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                    }                             
                }
                return response;
            });                        
        }

        [Route("Profile/ProfileID/{ProfileID}", Name = "UpdateProfile")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ProfileID, ProfileDTO objProfile)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileFacade().Update(ProfileID, objProfile);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileDTO>(HttpStatusCode.OK, objProfile);
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