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
    public class CountryController : ApiControllerBase
    {
        [Route("Country/CountryID/{CountryID}", Name = "GetCountryByID")]
        [HttpGet]
        public HttpResponseMessage GetCountry(HttpRequestMessage request, int CountryID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                CountryDTO result = BusinessLayer.Facades.Facade.CountryFacade().GetCountry(CountryID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<CountryDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Countries/data", Name = "GetCountriesDataTables")]
        [HttpPost]
        public DTResult<CountryDTO> GetCountries(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<CountryDTO> result = BusinessLayer.Facades.Facade.CountryFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Countries", Name = "GetCountriesAll")]
        [HttpGet]
        public HttpResponseMessage GetCountries(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<CountryDTO> result = BusinessLayer.Facades.Facade.CountryFacade().GetCountries();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<CountryDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Countries/{page}/{pagesize}", Name = "GetCountries")]
        [HttpGet]
        public HttpResponseMessage GetCountries(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<CountryDTO> result = BusinessLayer.Facades.Facade.CountryFacade().GetCountries(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<CountryDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("Country", Name = "CountryInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.CountryDTO objCountry)
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
                    CountryDTO result = BusinessLayer.Facades.Facade.CountryFacade().Insert(objCountry);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.CountryDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Country/CountryID/{CountryID}", Name = "DeleteCountry")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int CountryID)
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
                    bool result = BusinessLayer.Facades.Facade.CountryFacade().Delete(CountryID);
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


        [Route("Country/CountryID/{CountryID}", Name = "UpdateCountry")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int CountryID, CountryDTO objCountry)
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
                    bool result = BusinessLayer.Facades.Facade.CountryFacade().Update(CountryID,objCountry);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.CountryDTO>(HttpStatusCode.OK, objCountry);
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