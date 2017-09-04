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
    public class QuoteController : ApiControllerBase
    {
        [Route("Quote/QuoteID/{QuoteID}", Name = "GetQuoteByID")]
        [HttpGet]
        public HttpResponseMessage GetQuote(HttpRequestMessage request, int QuoteID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                QuoteDTO result = BusinessLayer.Facades.Facade.QuoteFacade().GetQuote(QuoteID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<QuoteDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Quotes/data", Name = "GetQuotesDataTables")]
        [HttpPost]
        public DTResult<QuoteDTO> GetQuotes(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<QuoteDTO> result = BusinessLayer.Facades.Facade.QuoteFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Quotes", Name = "GetQuotesAll")]
        [HttpGet]
        public HttpResponseMessage GetQuotes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<QuoteDTO> result = BusinessLayer.Facades.Facade.QuoteFacade().GetQuotes();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<QuoteDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Quotes/{page}/{pagesize}", Name = "GetQuotes")]
        [HttpGet]
        public HttpResponseMessage GetQuotes(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<QuoteDTO> result = BusinessLayer.Facades.Facade.QuoteFacade().GetQuotes(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<QuoteDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("Quotes/Category/{CategoryID}/data", Name = "GetQuotesDataTablesByCategoryID}")]
        [HttpPost]
        public DTResult<QuoteDTO> GetQuotesByCategoryID(HttpRequestMessage request, int CategoryID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<QuoteDTO> result = BusinessLayer.Facades.Facade.QuoteFacade().FilterResultByCategoryID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, CategoryID);
            return result;
        }
            
        [Route("Quotes/Category/{CategoryID}/{page}/{pagesize}", Name = "GetQuotesByCategory")]
        [HttpGet]
        public HttpResponseMessage GetQuotesByCategoryID(HttpRequestMessage request, int CategoryID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<QuoteDTO> result = BusinessLayer.Facades.Facade.QuoteFacade().GetQuotesByCategoryID(CategoryID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<QuoteDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("Quote", Name = "QuoteInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.QuoteDTO objQuote)
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
                    QuoteDTO result = BusinessLayer.Facades.Facade.QuoteFacade().Insert(objQuote);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.QuoteDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Quote/QuoteID/{QuoteID}", Name = "DeleteQuote")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int QuoteID)
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
                    bool result = BusinessLayer.Facades.Facade.QuoteFacade().Delete(QuoteID);
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


        [Route("Quote/QuoteID/{QuoteID}", Name = "UpdateQuote")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int QuoteID, QuoteDTO objQuote)
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
                    bool result = BusinessLayer.Facades.Facade.QuoteFacade().Update(QuoteID,objQuote);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.QuoteDTO>(HttpStatusCode.OK, objQuote);
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