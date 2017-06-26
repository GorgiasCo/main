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
    public class MessageController : ApiControllerBase
    {
        [Route("Message/MessageID/{MessageID}", Name = "GetMessageByID")]
        [HttpGet]
        public HttpResponseMessage GetMessage(HttpRequestMessage request, int MessageID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                MessageDTO result = BusinessLayer.Facades.Facade.MessageFacade().GetMessage(MessageID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<MessageDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Messages/data", Name = "GetMessagesDataTables")]
        [HttpPost]
        public DTResult<MessageDTO> GetMessages(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<MessageDTO> result = BusinessLayer.Facades.Facade.MessageFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Messages", Name = "GetMessagesAll")]
        [HttpGet]
        public HttpResponseMessage GetMessages(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<MessageDTO> result = BusinessLayer.Facades.Facade.MessageFacade().GetMessages();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<MessageDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Messages/{page}/{pagesize}", Name = "GetMessages")]
        [HttpGet]
        public HttpResponseMessage GetMessages(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<MessageDTO> result = BusinessLayer.Facades.Facade.MessageFacade().GetMessages(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<MessageDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("Messages/Profile/{ProfileID}/data", Name = "GetMessagesDataTablesByProfileID}")]
        [HttpPost]
        public DTResult<MessageDTO> GetMessagesByProfileID(HttpRequestMessage request, int ProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<MessageDTO> result = BusinessLayer.Facades.Facade.MessageFacade().FilterResultByProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileID);
            return result;
        }
            
        [Route("Messages/Profile/{ProfileID}/{page}/{pagesize}", Name = "GetMessagesByProfile")]
        [HttpGet]
        public HttpResponseMessage GetMessagesByProfileID(HttpRequestMessage request, int ProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<MessageDTO> result = BusinessLayer.Facades.Facade.MessageFacade().GetMessagesByProfileID(ProfileID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<MessageDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("Message", Name = "MessageInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.MessageDTO objMessage)
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
                    MessageDTO result = BusinessLayer.Facades.Facade.MessageFacade().Insert(objMessage);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.MessageDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Message/MessageID/{MessageID}", Name = "DeleteMessage")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int MessageID)
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
                    bool result = BusinessLayer.Facades.Facade.MessageFacade().Delete(MessageID);
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


        [Route("Message/MessageID/{MessageID}", Name = "UpdateMessage")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int MessageID, MessageDTO objMessage)
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
                    bool result = BusinessLayer.Facades.Facade.MessageFacade().Update(MessageID,objMessage);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.MessageDTO>(HttpStatusCode.OK, objMessage);
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