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
    public class RequestTypeController : ApiControllerBase
    {
        [Route("RequestType/RequestTypeID/{RequestTypeID}", Name = "GetRequestTypeByID")]
        [HttpGet]
        public HttpResponseMessage GetRequestType(HttpRequestMessage request, int RequestTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                RequestTypeDTO result = BusinessLayer.Facades.Facade.RequestTypeFacade().GetRequestType(RequestTypeID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<RequestTypeDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("RequestTypes/data", Name = "GetRequestTypesDataTables")]
        [HttpPost]
        public DTResult<RequestTypeDTO> GetRequestTypes(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<RequestTypeDTO> result = BusinessLayer.Facades.Facade.RequestTypeFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("RequestTypes", Name = "GetRequestTypesAll")]
        [HttpGet]
        public HttpResponseMessage GetRequestTypes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<RequestTypeDTO> result = BusinessLayer.Facades.Facade.RequestTypeFacade().GetRequestTypes();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<RequestTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("RequestTypes/{page}/{pagesize}", Name = "GetRequestTypes")]
        [HttpGet]
        public HttpResponseMessage GetRequestTypes(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<RequestTypeDTO> result = BusinessLayer.Facades.Facade.RequestTypeFacade().GetRequestTypes(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<RequestTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("RequestType", Name = "RequestTypeInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.RequestTypeDTO objRequestType)
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
                    RequestTypeDTO result = BusinessLayer.Facades.Facade.RequestTypeFacade().Insert(objRequestType);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.RequestTypeDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("RequestType/RequestTypeID/{RequestTypeID}", Name = "DeleteRequestType")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int RequestTypeID)
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
                    bool result = BusinessLayer.Facades.Facade.RequestTypeFacade().Delete(RequestTypeID);
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


        [Route("RequestType/RequestTypeID/{RequestTypeID}", Name = "UpdateRequestType")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int RequestTypeID, RequestTypeDTO objRequestType)
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
                    bool result = BusinessLayer.Facades.Facade.RequestTypeFacade().Update(RequestTypeID,objRequestType);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.RequestTypeDTO>(HttpStatusCode.OK, objRequestType);
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