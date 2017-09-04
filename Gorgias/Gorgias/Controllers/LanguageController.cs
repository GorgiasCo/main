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
    public class LanguageController : ApiControllerBase
    {
        [Route("Language/LanguageID/{LanguageID}", Name = "GetLanguageByID")]
        [HttpGet]
        public HttpResponseMessage GetLanguage(HttpRequestMessage request, int LanguageID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                LanguageDTO result = BusinessLayer.Facades.Facade.LanguageFacade().GetLanguage(LanguageID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<LanguageDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Languages/data", Name = "GetLanguagesDataTables")]
        [HttpPost]
        public DTResult<LanguageDTO> GetLanguages(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<LanguageDTO> result = BusinessLayer.Facades.Facade.LanguageFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Languages", Name = "GetLanguagesAll")]
        [HttpGet]
        public HttpResponseMessage GetLanguages(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<LanguageDTO> result = BusinessLayer.Facades.Facade.LanguageFacade().GetLanguages();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<LanguageDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Languages/{page}/{pagesize}", Name = "GetLanguages")]
        [HttpGet]
        public HttpResponseMessage GetLanguages(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<LanguageDTO> result = BusinessLayer.Facades.Facade.LanguageFacade().GetLanguages(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<LanguageDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("Language", Name = "LanguageInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.LanguageDTO objLanguage)
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
                    LanguageDTO result = BusinessLayer.Facades.Facade.LanguageFacade().Insert(objLanguage);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.LanguageDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Language/LanguageID/{LanguageID}", Name = "DeleteLanguage")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int LanguageID)
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
                    bool result = BusinessLayer.Facades.Facade.LanguageFacade().Delete(LanguageID);
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


        [Route("Language/LanguageID/{LanguageID}", Name = "UpdateLanguage")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int LanguageID, LanguageDTO objLanguage)
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
                    bool result = BusinessLayer.Facades.Facade.LanguageFacade().Update(LanguageID,objLanguage);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.LanguageDTO>(HttpStatusCode.OK, objLanguage);
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