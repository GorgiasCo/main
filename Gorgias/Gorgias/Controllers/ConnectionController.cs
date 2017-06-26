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
    public class ConnectionController : ApiControllerBase
    {
        [Route("Connection/ProfileID/RequestedProfileID/RequestTypeID/{ProfileID}/{RequestedProfileID}/{RequestTypeID}", Name = "GetConnectionByID")]
        [HttpGet]
        public HttpResponseMessage GetConnection(HttpRequestMessage request, int ProfileID, int RequestedProfileID, int RequestTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ConnectionDTO result = BusinessLayer.Facades.Facade.ConnectionFacade().GetConnection(ProfileID, RequestedProfileID, RequestTypeID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ConnectionDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Connections/data", Name = "GetConnectionsDataTables")]
        [HttpPost]
        public DTResult<ConnectionDTO> GetConnections(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ConnectionDTO> result = BusinessLayer.Facades.Facade.ConnectionFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Connections", Name = "GetConnectionsAll")]
        [HttpGet]
        public HttpResponseMessage GetConnections(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ConnectionDTO> result = BusinessLayer.Facades.Facade.ConnectionFacade().GetConnections();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ConnectionDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Connections/{page}/{pagesize}", Name = "GetConnections")]
        [HttpGet]
        public HttpResponseMessage GetConnections(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ConnectionDTO> result = BusinessLayer.Facades.Facade.ConnectionFacade().GetConnections(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ConnectionDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("Connections/RequestedProfileID/{RequestedProfileID}/data", Name = "GetConnectionsDataTablesByRequestedProfileID}")]
        [HttpPost]
        public DTResult<ConnectionDTO> GetConnectionsByRequestedProfileID(HttpRequestMessage request, int RequestedProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ConnectionDTO> result = BusinessLayer.Facades.Facade.ConnectionFacade().FilterResultByRequestedProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, RequestedProfileID);
            return result;
        }
            
        [Route("Connections/RequestedProfileID/{RequestedProfileID}/{page}/{pagesize}", Name = "GetConnectionsByRequestedProfileID")]
        [HttpGet]
        public HttpResponseMessage GetConnectionsByRequestedProfileID(HttpRequestMessage request, int RequestedProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ConnectionDTO> result = BusinessLayer.Facades.Facade.ConnectionFacade().GetConnectionsByRequestedProfileID(RequestedProfileID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ConnectionDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }

        [Route("Connections/RequestedProfileID/{RequestedProfileID}/{ConnectStatus}/{page}/{pagesize}", Name = "GetConnectionsByRequestedProfileIDANDStatus")]
        [HttpGet]
        public HttpResponseMessage GetConnectionsByRequestedProfileIDANDStatus(HttpRequestMessage request, int RequestedProfileID, bool ConnectStatus, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ConnectionDTO> result = BusinessLayer.Facades.Facade.ConnectionFacade().GetConnectionsByRequestedProfileID(RequestedProfileID, ConnectStatus, page, pagesize);

                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<ConnectionDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Connections/RequestTypeID/{RequestTypeID}/data", Name = "GetConnectionsDataTablesByRequestTypeID}")]
        [HttpPost]
        public DTResult<ConnectionDTO> GetConnectionsByRequestTypeID(HttpRequestMessage request, int RequestTypeID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ConnectionDTO> result = BusinessLayer.Facades.Facade.ConnectionFacade().FilterResultByRequestTypeID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, RequestTypeID);
            return result;
        }
            
        [Route("Connections/RequestTypeID/{RequestTypeID}/{page}/{pagesize}", Name = "GetConnectionsByRequestTypeID")]
        [HttpGet]
        public HttpResponseMessage GetConnectionsByRequestTypeID(HttpRequestMessage request, int RequestTypeID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ConnectionDTO> result = BusinessLayer.Facades.Facade.ConnectionFacade().GetConnectionsByRequestTypeID(RequestTypeID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ConnectionDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("Connection", Name = "ConnectionInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ConnectionDTO objConnection)
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
                    ConnectionDTO result = BusinessLayer.Facades.Facade.ConnectionFacade().Insert(objConnection);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ConnectionDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Connection/ProfileID/RequestedProfileID/RequestTypeID/{ProfileID}/{RequestedProfileID}/{RequestTypeID}", Name = "DeleteConnection")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int ProfileID, int RequestedProfileID, int RequestTypeID)
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
                    bool result = BusinessLayer.Facades.Facade.ConnectionFacade().Delete(ProfileID, RequestedProfileID, RequestTypeID);
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


        [Route("Connection/ProfileID/RequestedProfileID/RequestTypeID/{ProfileID}/{RequestedProfileID}/{RequestTypeID}", Name = "UpdateConnection")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ProfileID, int RequestedProfileID, int RequestTypeID, ConnectionDTO objConnection)
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
                    bool result = BusinessLayer.Facades.Facade.ConnectionFacade().Update(ProfileID, RequestedProfileID, RequestTypeID,objConnection);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ConnectionDTO>(HttpStatusCode.OK, objConnection);
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