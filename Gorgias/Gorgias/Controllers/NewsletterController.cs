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
    public class NewsletterController : ApiControllerBase
    {
        [Route("Newsletter/NewsletterID/{NewsletterID}", Name = "GetNewsletterByID")]
        [HttpGet]
        public HttpResponseMessage GetNewsletter(HttpRequestMessage request, int NewsletterID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                NewsletterDTO result = BusinessLayer.Facades.Facade.NewsletterFacade().GetNewsletter(NewsletterID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<NewsletterDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Newsletters/data", Name = "GetNewslettersDataTables")]
        [HttpPost]
        public DTResult<NewsletterDTO> GetNewsletters(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<NewsletterDTO> result = BusinessLayer.Facades.Facade.NewsletterFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Newsletters", Name = "GetNewslettersAll")]
        [HttpGet]
        public HttpResponseMessage GetNewsletters(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<NewsletterDTO> result = BusinessLayer.Facades.Facade.NewsletterFacade().GetNewsletters();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<NewsletterDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Newsletters/{page}/{pagesize}", Name = "GetNewsletters")]
        [HttpGet]
        public HttpResponseMessage GetNewsletters(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<NewsletterDTO> result = BusinessLayer.Facades.Facade.NewsletterFacade().GetNewsletters(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<NewsletterDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("Newsletter", Name = "NewsletterInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.NewsletterDTO objNewsletter)
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
                    NewsletterDTO result = BusinessLayer.Facades.Facade.NewsletterFacade().Insert(objNewsletter);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.NewsletterDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Newsletter/NewsletterID/{NewsletterID}", Name = "DeleteNewsletter")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int NewsletterID)
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
                    bool result = BusinessLayer.Facades.Facade.NewsletterFacade().Delete(NewsletterID);
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


        [Route("Newsletter/NewsletterID/{NewsletterID}", Name = "UpdateNewsletter")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int NewsletterID, NewsletterDTO objNewsletter)
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
                    bool result = BusinessLayer.Facades.Facade.NewsletterFacade().Update(NewsletterID,objNewsletter);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.NewsletterDTO>(HttpStatusCode.OK, objNewsletter);
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