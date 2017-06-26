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
    public class ProfileSocialNetworkController : ApiControllerBase
    {
        [Route("ProfileSocialNetwork/SocialNetworkID/ProfileID/{SocialNetworkID}/{ProfileID}", Name = "GetProfileSocialNetworkByID")]
        [HttpGet]
        public HttpResponseMessage GetProfileSocialNetwork(HttpRequestMessage request, int SocialNetworkID, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProfileSocialNetworkDTO result = BusinessLayer.Facades.Facade.ProfileSocialNetworkFacade().GetProfileSocialNetwork(SocialNetworkID, ProfileID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ProfileSocialNetworkDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("ProfileSocialNetworks/data", Name = "GetProfileSocialNetworksDataTables")]
        [HttpPost]
        public DTResult<ProfileSocialNetworkDTO> GetProfileSocialNetworks(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileSocialNetworkDTO> result = BusinessLayer.Facades.Facade.ProfileSocialNetworkFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ProfileSocialNetworks", Name = "GetProfileSocialNetworksAll")]
        [HttpGet]
        public HttpResponseMessage GetProfileSocialNetworks(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProfileSocialNetworkDTO> result = BusinessLayer.Facades.Facade.ProfileSocialNetworkFacade().GetProfileSocialNetworks();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ProfileSocialNetworkDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("ProfileSocialNetworks/{page}/{pagesize}", Name = "GetProfileSocialNetworks")]
        [HttpGet]
        public HttpResponseMessage GetProfileSocialNetworks(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileSocialNetworkDTO> result = BusinessLayer.Facades.Facade.ProfileSocialNetworkFacade().GetProfileSocialNetworks(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileSocialNetworkDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("ProfileSocialNetworks/ProfileID/{ProfileID}/data", Name = "GetProfileSocialNetworksDataTablesByProfileID}")]
        [HttpPost]
        public DTResult<ProfileSocialNetworkDTO> GetProfileSocialNetworksByProfileID(HttpRequestMessage request, int ProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileSocialNetworkDTO> result = BusinessLayer.Facades.Facade.ProfileSocialNetworkFacade().FilterResultByProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileID);
            return result;
        }
            
        [Route("ProfileSocialNetworks/ProfileID/{ProfileID}/{page}/{pagesize}", Name = "GetProfileSocialNetworksByProfileID")]
        [HttpGet]
        public HttpResponseMessage GetProfileSocialNetworksByProfileID(HttpRequestMessage request, int ProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileSocialNetworkDTO> result = BusinessLayer.Facades.Facade.ProfileSocialNetworkFacade().GetProfileSocialNetworksByProfileID(ProfileID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileSocialNetworkDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("ProfileSocialNetwork", Name = "ProfileSocialNetworkInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ProfileSocialNetworkDTO objProfileSocialNetwork)
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
                    ProfileSocialNetworkDTO result = BusinessLayer.Facades.Facade.ProfileSocialNetworkFacade().Insert(objProfileSocialNetwork);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileSocialNetworkDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("ProfileSocialNetwork/SocialNetworkID/ProfileID/{SocialNetworkID}/{ProfileID}", Name = "DeleteProfileSocialNetwork")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int SocialNetworkID, int ProfileID)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileSocialNetworkFacade().Delete(SocialNetworkID, ProfileID);
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


        [Route("ProfileSocialNetwork/SocialNetworkID/ProfileID/{SocialNetworkID}/{ProfileID}", Name = "UpdateProfileSocialNetwork")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int SocialNetworkID, int ProfileID, ProfileSocialNetworkDTO objProfileSocialNetwork)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileSocialNetworkFacade().Update(SocialNetworkID, ProfileID,objProfileSocialNetwork);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileSocialNetworkDTO>(HttpStatusCode.OK, objProfileSocialNetwork);
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