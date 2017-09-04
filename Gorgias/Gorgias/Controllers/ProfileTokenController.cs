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
    public class ProfileTokenController : ApiControllerBase
    {
        [Route("ProfileToken/ProfileTokenID/{ProfileTokenID}", Name = "GetProfileTokenByID")]
        [HttpGet]
        public HttpResponseMessage GetProfileToken(HttpRequestMessage request, int ProfileTokenID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProfileTokenDTO result = BusinessLayer.Facades.Facade.ProfileTokenFacade().GetProfileToken(ProfileTokenID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ProfileTokenDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("ProfileTokens/data", Name = "GetProfileTokensDataTables")]
        [HttpPost]
        public DTResult<ProfileTokenDTO> GetProfileTokens(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileTokenDTO> result = BusinessLayer.Facades.Facade.ProfileTokenFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ProfileTokens", Name = "GetProfileTokensAll")]
        [HttpGet]
        public HttpResponseMessage GetProfileTokens(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProfileTokenDTO> result = BusinessLayer.Facades.Facade.ProfileTokenFacade().GetProfileTokens();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ProfileTokenDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("ProfileTokens/{page}/{pagesize}", Name = "GetProfileTokens")]
        [HttpGet]
        public HttpResponseMessage GetProfileTokens(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileTokenDTO> result = BusinessLayer.Facades.Facade.ProfileTokenFacade().GetProfileTokens(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileTokenDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("ProfileTokens/Profile/{ProfileID}/data", Name = "GetProfileTokensDataTablesByProfileID}")]
        [HttpPost]
        public DTResult<ProfileTokenDTO> GetProfileTokensByProfileID(HttpRequestMessage request, int ProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileTokenDTO> result = BusinessLayer.Facades.Facade.ProfileTokenFacade().FilterResultByProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileID);
            return result;
        }
            
        [Route("ProfileTokens/Profile/{ProfileID}/{page}/{pagesize}", Name = "GetProfileTokensByProfile")]
        [HttpGet]
        public HttpResponseMessage GetProfileTokensByProfileID(HttpRequestMessage request, int ProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileTokenDTO> result = BusinessLayer.Facades.Facade.ProfileTokenFacade().GetProfileTokensByProfileID(ProfileID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileTokenDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("ProfileToken", Name = "ProfileTokenInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ProfileTokenDTO objProfileToken)
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
                    ProfileTokenDTO result = BusinessLayer.Facades.Facade.ProfileTokenFacade().Insert(objProfileToken);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileTokenDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("ProfileToken/ProfileTokenID/{ProfileTokenID}", Name = "DeleteProfileToken")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int ProfileTokenID)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileTokenFacade().Delete(ProfileTokenID);
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


        [Route("ProfileToken/ProfileTokenID/{ProfileTokenID}", Name = "UpdateProfileToken")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ProfileTokenID, ProfileTokenDTO objProfileToken)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileTokenFacade().Update(ProfileTokenID,objProfileToken);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileTokenDTO>(HttpStatusCode.OK, objProfileToken);
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