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
using Gorgias.Business.DataTransferObjects.Report;

namespace Gorgias.Controllers
{   
    [RoutePrefix("api")]
    public class FBActivityController : ApiControllerBase
    {
        [Route("FBActivity/FBActivityID/{FBActivityID}", Name = "GetFBActivityByID")]
        [HttpGet]
        public HttpResponseMessage GetFBActivity(HttpRequestMessage request, int FBActivityID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FBActivityDTO result = BusinessLayer.Facades.Facade.FBActivityFacade().GetFBActivity(FBActivityID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<FBActivityDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("FBActivity/Current", Name = "GetFBActivityCurrent")]
        [HttpGet]
        public HttpResponseMessage GetFBActivityCurrent(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FBReport result = BusinessLayer.Facades.Facade.FBActivityFacade().GetFBReportCurrent();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<FBReport>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("FBActivity/Current/Month", Name = "GetFBActivityCurrentMonth")]
        [HttpGet]
        public HttpResponseMessage GetFBActivityCurrentMonth(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FBReport result = BusinessLayer.Facades.Facade.FBActivityFacade().GetFBReportCurrentMonth();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<FBReport>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("FBActivity/Current/Overall", Name = "GetFBActivityCurrentOverall")]
        [HttpGet]
        public HttpResponseMessage GetFBActivityCurrentOverall(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FBReport result = BusinessLayer.Facades.Facade.FBActivityFacade().GetFBReportCurrentOverall();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<FBReport>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("FBActivities/data", Name = "GetFBActivitiesDataTables")]
        [HttpPost]
        public DTResult<FBActivityDTO> GetFBActivities(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<FBActivityDTO> result = BusinessLayer.Facades.Facade.FBActivityFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("FBActivities", Name = "GetFBActivitiesAll")]
        [HttpGet]
        public HttpResponseMessage GetFBActivities(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FBActivityDTO> result = BusinessLayer.Facades.Facade.FBActivityFacade().GetFBActivities();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<FBActivityDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("FBActivities/{page}/{pagesize}", Name = "GetFBActivities")]
        [HttpGet]
        public HttpResponseMessage GetFBActivities(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<FBActivityDTO> result = BusinessLayer.Facades.Facade.FBActivityFacade().GetFBActivities(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<FBActivityDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("FBActivity", Name = "FBActivityInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.FBActivityDTO objFBActivity)
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
                    FBActivityDTO result = BusinessLayer.Facades.Facade.FBActivityFacade().Insert(objFBActivity);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.FBActivityDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("FBActivity/FBActivityID/{FBActivityID}", Name = "DeleteFBActivity")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int FBActivityID)
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
                    bool result = BusinessLayer.Facades.Facade.FBActivityFacade().Delete(FBActivityID);
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


        [Route("FBActivity/FBActivityID/{FBActivityID}", Name = "UpdateFBActivity")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int FBActivityID, FBActivityDTO objFBActivity)
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
                    bool result = BusinessLayer.Facades.Facade.FBActivityFacade().Update(FBActivityID,objFBActivity);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.FBActivityDTO>(HttpStatusCode.OK, objFBActivity);
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