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
    public class IndustryController : ApiControllerBase
    {
        [Route("Industry/IndustryID/{IndustryID}", Name = "GetIndustryByID")]
        [HttpGet]
        public HttpResponseMessage GetIndustry(HttpRequestMessage request, int IndustryID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IndustryDTO result = BusinessLayer.Facades.Facade.IndustryFacade().GetIndustry(IndustryID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<IndustryDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Industries/data", Name = "GetIndustriesDataTables")]
        [HttpPost]
        public DTResult<IndustryDTO> GetIndustries(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<IndustryDTO> result = BusinessLayer.Facades.Facade.IndustryFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Industries", Name = "GetIndustriesAll")]
        [HttpGet]
        public HttpResponseMessage GetIndustries(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<IndustryDTO> result = BusinessLayer.Facades.Facade.IndustryFacade().GetIndustries();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<IndustryDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Industries/{page}/{pagesize}", Name = "GetIndustries")]
        [HttpGet]
        public HttpResponseMessage GetIndustries(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<IndustryDTO> result = BusinessLayer.Facades.Facade.IndustryFacade().GetIndustries(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<IndustryDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("Industry", Name = "IndustryInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.IndustryDTO objIndustry)
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
                    IndustryDTO result = BusinessLayer.Facades.Facade.IndustryFacade().Insert(objIndustry);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.IndustryDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Industry/IndustryID/{IndustryID}", Name = "DeleteIndustry")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int IndustryID)
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
                    bool result = BusinessLayer.Facades.Facade.IndustryFacade().Delete(IndustryID);
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


        [Route("Industry/IndustryID/{IndustryID}", Name = "UpdateIndustry")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int IndustryID, IndustryDTO objIndustry)
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
                    bool result = BusinessLayer.Facades.Facade.IndustryFacade().Update(IndustryID,objIndustry);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.IndustryDTO>(HttpStatusCode.OK, objIndustry);
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