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
    public class SocialNetworkController : ApiControllerBase
    {
        [Route("SocialNetwork/SocialNetworkID/{SocialNetworkID}", Name = "GetSocialNetworkByID")]
        [HttpGet]
        public HttpResponseMessage GetSocialNetwork(HttpRequestMessage request, int SocialNetworkID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SocialNetworkDTO result = BusinessLayer.Facades.Facade.SocialNetworkFacade().GetSocialNetwork(SocialNetworkID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<SocialNetworkDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("SocialNetworks/data", Name = "GetSocialNetworksDataTables")]
        [HttpPost]
        public DTResult<SocialNetworkDTO> GetSocialNetworks(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<SocialNetworkDTO> result = BusinessLayer.Facades.Facade.SocialNetworkFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("SocialNetworks", Name = "GetSocialNetworksAll")]
        [HttpGet]
        public HttpResponseMessage GetSocialNetworks(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SocialNetworkDTO> result = BusinessLayer.Facades.Facade.SocialNetworkFacade().GetSocialNetworks();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<SocialNetworkDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("SocialNetworks/{page}/{pagesize}", Name = "GetSocialNetworks")]
        [HttpGet]
        public HttpResponseMessage GetSocialNetworks(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<SocialNetworkDTO> result = BusinessLayer.Facades.Facade.SocialNetworkFacade().GetSocialNetworks(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<SocialNetworkDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("SocialNetwork", Name = "SocialNetworkInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.SocialNetworkDTO objSocialNetwork)
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
                    SocialNetworkDTO result = BusinessLayer.Facades.Facade.SocialNetworkFacade().Insert(objSocialNetwork);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.SocialNetworkDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("SocialNetwork/SocialNetworkID/{SocialNetworkID}", Name = "DeleteSocialNetwork")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int SocialNetworkID)
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
                    bool result = BusinessLayer.Facades.Facade.SocialNetworkFacade().Delete(SocialNetworkID);
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


        [Route("SocialNetwork/SocialNetworkID/{SocialNetworkID}", Name = "UpdateSocialNetwork")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int SocialNetworkID, SocialNetworkDTO objSocialNetwork)
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
                    bool result = BusinessLayer.Facades.Facade.SocialNetworkFacade().Update(SocialNetworkID,objSocialNetwork);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.SocialNetworkDTO>(HttpStatusCode.OK, objSocialNetwork);
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