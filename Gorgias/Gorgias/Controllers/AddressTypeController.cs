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
    public class AddressTypeController : ApiControllerBase
    {
        [Route("AddressType/AddressTypeID/{AddressTypeID}", Name = "GetAddressTypeByID")]
        [HttpGet]
        public HttpResponseMessage GetAddressType(HttpRequestMessage request, int AddressTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AddressTypeDTO result = BusinessLayer.Facades.Facade.AddressTypeFacade().GetAddressType(AddressTypeID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<AddressTypeDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("AddressTypes/data", Name = "GetAddressTypesDataTables")]
        [HttpPost]
        public DTResult<AddressTypeDTO> GetAddressTypes(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<AddressTypeDTO> result = BusinessLayer.Facades.Facade.AddressTypeFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("AddressTypes", Name = "GetAddressTypesAll")]
        [HttpGet]
        public HttpResponseMessage GetAddressTypes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<AddressTypeDTO> result = BusinessLayer.Facades.Facade.AddressTypeFacade().GetAddressTypes();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<AddressTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("AddressTypes/{page}/{pagesize}", Name = "GetAddressTypes")]
        [HttpGet]
        public HttpResponseMessage GetAddressTypes(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<AddressTypeDTO> result = BusinessLayer.Facades.Facade.AddressTypeFacade().GetAddressTypes(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<AddressTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("AddressType", Name = "AddressTypeInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.AddressTypeDTO objAddressType)
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
                    AddressTypeDTO result = BusinessLayer.Facades.Facade.AddressTypeFacade().Insert(objAddressType);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AddressTypeDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("AddressType/AddressTypeID/{AddressTypeID}", Name = "DeleteAddressType")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int AddressTypeID)
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
                    bool result = BusinessLayer.Facades.Facade.AddressTypeFacade().Delete(AddressTypeID);
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


        [Route("AddressType/AddressTypeID/{AddressTypeID}", Name = "UpdateAddressType")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int AddressTypeID, AddressTypeDTO objAddressType)
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
                    bool result = BusinessLayer.Facades.Facade.AddressTypeFacade().Update(AddressTypeID,objAddressType);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AddressTypeDTO>(HttpStatusCode.OK, objAddressType);
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