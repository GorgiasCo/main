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
    public class ExternalLinkController : ApiControllerBase
    {
        [Route("ExternalLink/LinkTypeID/ProfileID/{LinkTypeID}/{ProfileID}", Name = "GetExternalLinkByID")]
        [HttpGet]
        public HttpResponseMessage GetExternalLink(HttpRequestMessage request, int LinkTypeID, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ExternalLinkDTO result = BusinessLayer.Facades.Facade.ExternalLinkFacade().GetExternalLink(LinkTypeID, ProfileID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ExternalLinkDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("ExternalLinks/data", Name = "GetExternalLinksDataTables")]
        [HttpPost]
        public DTResult<ExternalLinkDTO> GetExternalLinks(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ExternalLinkDTO> result = BusinessLayer.Facades.Facade.ExternalLinkFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ExternalLinks", Name = "GetExternalLinksAll")]
        [HttpGet]
        public HttpResponseMessage GetExternalLinks(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ExternalLinkDTO> result = BusinessLayer.Facades.Facade.ExternalLinkFacade().GetExternalLinks();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ExternalLinkDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("ExternalLinks/{page}/{pagesize}", Name = "GetExternalLinks")]
        [HttpGet]
        public HttpResponseMessage GetExternalLinks(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ExternalLinkDTO> result = BusinessLayer.Facades.Facade.ExternalLinkFacade().GetExternalLinks(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ExternalLinkDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("ExternalLinks/ProfileID/{ProfileID}/data", Name = "GetExternalLinksDataTablesByProfileID}")]
        [HttpPost]
        public DTResult<ExternalLinkDTO> GetExternalLinksByProfileID(HttpRequestMessage request, int ProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ExternalLinkDTO> result = BusinessLayer.Facades.Facade.ExternalLinkFacade().FilterResultByProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileID);
            return result;
        }
            
        [Route("ExternalLinks/ProfileID/{ProfileID}/{page}/{pagesize}", Name = "GetExternalLinksByProfileID")]
        [HttpGet]
        public HttpResponseMessage GetExternalLinksByProfileID(HttpRequestMessage request, int ProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ExternalLinkDTO> result = BusinessLayer.Facades.Facade.ExternalLinkFacade().GetExternalLinksByProfileID(ProfileID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ExternalLinkDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("ExternalLink", Name = "ExternalLinkInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ExternalLinkDTO objExternalLink)
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
                    ExternalLinkDTO result = BusinessLayer.Facades.Facade.ExternalLinkFacade().Insert(objExternalLink);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ExternalLinkDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("ExternalLink/LinkTypeID/ProfileID/{LinkTypeID}/{ProfileID}", Name = "DeleteExternalLink")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int LinkTypeID, int ProfileID)
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
                    bool result = BusinessLayer.Facades.Facade.ExternalLinkFacade().Delete(LinkTypeID, ProfileID);
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


        [Route("ExternalLink/LinkTypeID/ProfileID/{LinkTypeID}/{ProfileID}", Name = "UpdateExternalLink")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int LinkTypeID, int ProfileID, ExternalLinkDTO objExternalLink)
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
                    bool result = BusinessLayer.Facades.Facade.ExternalLinkFacade().Update(LinkTypeID, ProfileID,objExternalLink);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ExternalLinkDTO>(HttpStatusCode.OK, objExternalLink);
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