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
    public class AttributeController : ApiControllerBase
    {
        [Route("Attribute/AttributeID/{AttributeID}", Name = "GetAttributeByID")]
        [HttpGet]
        public HttpResponseMessage GetAttribute(HttpRequestMessage request, int AttributeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AttributeDTO result = BusinessLayer.Facades.Facade.AttributeFacade().GetAttribute(AttributeID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<AttributeDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Attributes/data", Name = "GetAttributesDataTables")]
        [HttpPost]
        public DTResult<AttributeDTO> GetAttributes(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<AttributeDTO> result = BusinessLayer.Facades.Facade.AttributeFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Attributes", Name = "GetAttributesAll")]
        [HttpGet]
        public HttpResponseMessage GetAttributes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<AttributeDTO> result = BusinessLayer.Facades.Facade.AttributeFacade().GetAttributes();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<AttributeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Attributes/{page}/{pagesize}", Name = "GetAttributes")]
        [HttpGet]
        public HttpResponseMessage GetAttributes(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<AttributeDTO> result = BusinessLayer.Facades.Facade.AttributeFacade().GetAttributes(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<AttributeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("Attribute", Name = "AttributeInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.AttributeDTO objAttribute)
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
                    AttributeDTO result = BusinessLayer.Facades.Facade.AttributeFacade().Insert(objAttribute);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AttributeDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Attribute/AttributeID/{AttributeID}", Name = "DeleteAttribute")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int AttributeID)
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
                    bool result = BusinessLayer.Facades.Facade.AttributeFacade().Delete(AttributeID);
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


        [Route("Attribute/AttributeID/{AttributeID}", Name = "UpdateAttribute")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int AttributeID, AttributeDTO objAttribute)
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
                    bool result = BusinessLayer.Facades.Facade.AttributeFacade().Update(AttributeID,objAttribute);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AttributeDTO>(HttpStatusCode.OK, objAttribute);
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