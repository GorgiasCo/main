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
    public class SubscriptionTypeController : ApiControllerBase
    {
        [Route("SubscriptionType/SubscriptionTypeID/{SubscriptionTypeID}", Name = "GetSubscriptionTypeByID")]
        [HttpGet]
        public HttpResponseMessage GetSubscriptionType(HttpRequestMessage request, int SubscriptionTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SubscriptionTypeDTO result = BusinessLayer.Facades.Facade.SubscriptionTypeFacade().GetSubscriptionType(SubscriptionTypeID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<SubscriptionTypeDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("SubscriptionTypes/data", Name = "GetSubscriptionTypesDataTables")]
        [HttpPost]
        public DTResult<SubscriptionTypeDTO> GetSubscriptionTypes(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<SubscriptionTypeDTO> result = BusinessLayer.Facades.Facade.SubscriptionTypeFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("SubscriptionTypes", Name = "GetSubscriptionTypesAll")]
        [HttpGet]
        public HttpResponseMessage GetSubscriptionTypes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubscriptionTypeDTO> result = BusinessLayer.Facades.Facade.SubscriptionTypeFacade().GetSubscriptionTypes();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<SubscriptionTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("SubscriptionTypes/{page}/{pagesize}", Name = "GetSubscriptionTypes")]
        [HttpGet]
        public HttpResponseMessage GetSubscriptionTypes(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<SubscriptionTypeDTO> result = BusinessLayer.Facades.Facade.SubscriptionTypeFacade().GetSubscriptionTypes(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<SubscriptionTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("SubscriptionType", Name = "SubscriptionTypeInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.SubscriptionTypeDTO objSubscriptionType)
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
                    SubscriptionTypeDTO result = BusinessLayer.Facades.Facade.SubscriptionTypeFacade().Insert(objSubscriptionType);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.SubscriptionTypeDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("SubscriptionType/SubscriptionTypeID/{SubscriptionTypeID}", Name = "DeleteSubscriptionType")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int SubscriptionTypeID)
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
                    bool result = BusinessLayer.Facades.Facade.SubscriptionTypeFacade().Delete(SubscriptionTypeID);
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


        [Route("SubscriptionType/SubscriptionTypeID/{SubscriptionTypeID}", Name = "UpdateSubscriptionType")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int SubscriptionTypeID, SubscriptionTypeDTO objSubscriptionType)
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
                    bool result = BusinessLayer.Facades.Facade.SubscriptionTypeFacade().Update(SubscriptionTypeID,objSubscriptionType);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.SubscriptionTypeDTO>(HttpStatusCode.OK, objSubscriptionType);
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