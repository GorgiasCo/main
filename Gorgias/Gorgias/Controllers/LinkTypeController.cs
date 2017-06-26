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
    public class LinkTypeController : ApiControllerBase
    {
        [Route("LinkType/LinkTypeID/{LinkTypeID}", Name = "GetLinkTypeByID")]
        [HttpGet]
        public HttpResponseMessage GetLinkType(HttpRequestMessage request, int LinkTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                LinkTypeDTO result = BusinessLayer.Facades.Facade.LinkTypeFacade().GetLinkType(LinkTypeID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<LinkTypeDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("LinkTypes/data", Name = "GetLinkTypesDataTables")]
        [HttpPost]
        public DTResult<LinkTypeDTO> GetLinkTypes(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<LinkTypeDTO> result = BusinessLayer.Facades.Facade.LinkTypeFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("LinkTypes", Name = "GetLinkTypesAll")]
        [HttpGet]
        public HttpResponseMessage GetLinkTypes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<LinkTypeDTO> result = BusinessLayer.Facades.Facade.LinkTypeFacade().GetLinkTypes();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<LinkTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("LinkTypes/{page}/{pagesize}", Name = "GetLinkTypes")]
        [HttpGet]
        public HttpResponseMessage GetLinkTypes(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<LinkTypeDTO> result = BusinessLayer.Facades.Facade.LinkTypeFacade().GetLinkTypes(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<LinkTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("LinkType", Name = "LinkTypeInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.LinkTypeDTO objLinkType)
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
                    LinkTypeDTO result = BusinessLayer.Facades.Facade.LinkTypeFacade().Insert(objLinkType);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.LinkTypeDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("LinkType/LinkTypeID/{LinkTypeID}", Name = "DeleteLinkType")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int LinkTypeID)
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
                    bool result = BusinessLayer.Facades.Facade.LinkTypeFacade().Delete(LinkTypeID);
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


        [Route("LinkType/LinkTypeID/{LinkTypeID}", Name = "UpdateLinkType")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int LinkTypeID, LinkTypeDTO objLinkType)
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
                    bool result = BusinessLayer.Facades.Facade.LinkTypeFacade().Update(LinkTypeID,objLinkType);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.LinkTypeDTO>(HttpStatusCode.OK, objLinkType);
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