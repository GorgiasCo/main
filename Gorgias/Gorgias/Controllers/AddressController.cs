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
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Geocoding.Response;

namespace Gorgias.Controllers
{   
    [RoutePrefix("api")]
    [Authorize]
    public class AddressController : ApiControllerBase
    {
        [Route("Address/AddressID/{AddressID}", Name = "GetAddressByID")]
        [HttpGet]
        public HttpResponseMessage GetAddress(HttpRequestMessage request, int AddressID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AddressDTO result = BusinessLayer.Facades.Facade.AddressFacade().GetAddress(AddressID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<AddressDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        //[Authorize]
        [Route("Addresses/data", Name = "GetAddressesDataTables")]
        [HttpPost]
        public DTResult<AddressDTO> GetAddresses(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<AddressDTO> result = BusinessLayer.Facades.Facade.AddressFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Addresses", Name = "GetAddressesAll")]
        [HttpGet]
        public HttpResponseMessage GetAddresses(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<AddressDTO> result = BusinessLayer.Facades.Facade.AddressFacade().GetAddresses();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<AddressDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Addresses/{page}/{pagesize}", Name = "GetAddresses")]
        [HttpGet]
        public HttpResponseMessage GetAddresses(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<AddressDTO> result = BusinessLayer.Facades.Facade.AddressFacade().GetAddresses(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<AddressDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("Addresses/City/{CityID}/data", Name = "GetAddressesDataTablesByCityID}")]
        [HttpPost]
        public DTResult<AddressDTO> GetAddressesByCityID(HttpRequestMessage request, int CityID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<AddressDTO> result = BusinessLayer.Facades.Facade.AddressFacade().FilterResultByCityID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, CityID);
            return result;
        }
            
        [Route("Addresses/City/{CityID}/{page}/{pagesize}", Name = "GetAddressesByCity")]
        [HttpGet]
        public HttpResponseMessage GetAddressesByCityID(HttpRequestMessage request, int CityID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<AddressDTO> result = BusinessLayer.Facades.Facade.AddressFacade().GetAddressesByCityID(CityID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<AddressDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        [Route("Addresses/Profile/{ProfileID}/data", Name = "GetAddressesDataTablesByProfileID}")]
        [HttpPost]
        public DTResult<AddressDTO> GetAddressesByProfileID(HttpRequestMessage request, int ProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<AddressDTO> result = BusinessLayer.Facades.Facade.AddressFacade().FilterResultByProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileID);
            return result;
        }
            
        [Route("Addresses/Profile/{ProfileID}/{page}/{pagesize}", Name = "GetAddressesByProfile")]
        [HttpGet]
        public HttpResponseMessage GetAddressesByProfileID(HttpRequestMessage request, int ProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<AddressDTO> result = BusinessLayer.Facades.Facade.AddressFacade().GetAddressesByProfileID(ProfileID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<AddressDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        [Route("Addresses/AddressType/{AddressTypeID}/data", Name = "GetAddressesDataTablesByAddressTypeID}")]
        [HttpPost]
        public DTResult<AddressDTO> GetAddressesByAddressTypeID(HttpRequestMessage request, int AddressTypeID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<AddressDTO> result = BusinessLayer.Facades.Facade.AddressFacade().FilterResultByAddressTypeID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, AddressTypeID);
            return result;
        }
            
        [Route("Addresses/AddressType/{AddressTypeID}/{page}/{pagesize}", Name = "GetAddressesByAddressType")]
        [HttpGet]
        public HttpResponseMessage GetAddressesByAddressTypeID(HttpRequestMessage request, int AddressTypeID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<AddressDTO> result = BusinessLayer.Facades.Facade.AddressFacade().GetAddressesByAddressTypeID(AddressTypeID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<AddressDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("Address", Name = "AddressInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.AddressDTO objAddress)
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
                    AddressDTO result = BusinessLayer.Facades.Facade.AddressFacade().Insert(objAddress);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AddressDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Address/AddressID/{AddressID}", Name = "DeleteAddress")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int AddressID)
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
                    bool result = BusinessLayer.Facades.Facade.AddressFacade().Delete(AddressID);
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


        [Route("Address/AddressID/{AddressID}", Name = "UpdateAddress")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int AddressID, AddressDTO objAddress)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                //Instance class use (Geocode)  (Can be made from static/instance class)
                GeocodingRequest geocodeRequest = new GeocodingRequest()
                {
                    Address = objAddress.AddressAddress,
                };
                var geocodingEngine = GoogleMaps.Geocode;
                GeocodingResponse geocode = geocodingEngine.Query(geocodeRequest);
                Console.WriteLine(geocode);

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
                }
                else
                {
                    bool result = BusinessLayer.Facades.Facade.AddressFacade().Update(AddressID,objAddress);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AddressDTO>(HttpStatusCode.OK, objAddress);
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