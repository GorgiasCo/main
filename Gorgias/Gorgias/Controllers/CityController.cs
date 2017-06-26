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
    public class CityController : ApiControllerBase
    {
        [Route("City/CityID/{CityID}", Name = "GetCityByID")]
        [HttpGet]
        public HttpResponseMessage GetCity(HttpRequestMessage request, int CityID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                CityDTO result = BusinessLayer.Facades.Facade.CityFacade().GetCity(CityID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<CityDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Cities/data", Name = "GetCitiesDataTables")]
        [HttpPost]
        public DTResult<CityDTO> GetCities(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<CityDTO> result = BusinessLayer.Facades.Facade.CityFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Cities", Name = "GetCitiesAll")]
        [HttpGet]
        public HttpResponseMessage GetCities(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<CityDTO> result = BusinessLayer.Facades.Facade.CityFacade().GetCities();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<CityDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Cities/{page}/{pagesize}", Name = "GetCities")]
        [HttpGet]
        public HttpResponseMessage GetCities(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<CityDTO> result = BusinessLayer.Facades.Facade.CityFacade().GetCities(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<CityDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("Cities/Country/{CountryID}/data", Name = "GetCitiesDataTablesByCountryID}")]
        [HttpPost]
        public DTResult<CityDTO> GetCitiesByCountryID(HttpRequestMessage request, int CountryID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<CityDTO> result = BusinessLayer.Facades.Facade.CityFacade().FilterResultByCountryID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, CountryID);
            return result;
        }
            
        [Route("Cities/Country/{CountryID}/{page}/{pagesize}", Name = "GetCitiesByCountry")]
        [HttpGet]
        public HttpResponseMessage GetCitiesByCountryID(HttpRequestMessage request, int CountryID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<CityDTO> result = BusinessLayer.Facades.Facade.CityFacade().GetCitiesByCountryID(CountryID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<CityDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("City", Name = "CityInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.CityDTO objCity)
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
                    CityDTO result = BusinessLayer.Facades.Facade.CityFacade().Insert(objCity);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.CityDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("City/CityID/{CityID}", Name = "DeleteCity")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int CityID)
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
                    bool result = BusinessLayer.Facades.Facade.CityFacade().Delete(CityID);
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


        [Route("City/CityID/{CityID}", Name = "UpdateCity")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int CityID, CityDTO objCity)
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
                    bool result = BusinessLayer.Facades.Facade.CityFacade().Update(CityID,objCity);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.CityDTO>(HttpStatusCode.OK, objCity);
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