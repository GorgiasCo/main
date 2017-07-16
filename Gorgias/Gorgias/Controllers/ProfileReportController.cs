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
    public class ProfileReportController : ApiControllerBase
    {
        [Route("ProfileReport/ProfileReportID/{ProfileReportID}", Name = "GetProfileReportByID")]
        [HttpGet]
        public HttpResponseMessage GetProfileReport(HttpRequestMessage request, int ProfileReportID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProfileReportDTO result = BusinessLayer.Facades.Facade.ProfileReportFacade().GetProfileReport(ProfileReportID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ProfileReportDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("ProfileReports/data", Name = "GetProfileReportsDataTables")]
        [HttpPost]
        public DTResult<ProfileReportDTO> GetProfileReports(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileReportDTO> result = BusinessLayer.Facades.Facade.ProfileReportFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ProfileReports", Name = "GetProfileReportsAll")]
        [HttpGet]
        public HttpResponseMessage GetProfileReports(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProfileReportDTO> result = BusinessLayer.Facades.Facade.ProfileReportFacade().GetProfileReports();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ProfileReportDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("ProfileReports/{page}/{pagesize}", Name = "GetProfileReports")]
        [HttpGet]
        public HttpResponseMessage GetProfileReports(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileReportDTO> result = BusinessLayer.Facades.Facade.ProfileReportFacade().GetProfileReports(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileReportDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("ProfileReports/ReportType/{ReportTypeID}/data", Name = "GetProfileReportsDataTablesByReportTypeID}")]
        [HttpPost]
        public DTResult<ProfileReportDTO> GetProfileReportsByReportTypeID(HttpRequestMessage request, int ReportTypeID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileReportDTO> result = BusinessLayer.Facades.Facade.ProfileReportFacade().FilterResultByReportTypeID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ReportTypeID);
            return result;
        }
            
        [Route("ProfileReports/ReportType/{ReportTypeID}/{page}/{pagesize}", Name = "GetProfileReportsByReportType")]
        [HttpGet]
        public HttpResponseMessage GetProfileReportsByReportTypeID(HttpRequestMessage request, int ReportTypeID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileReportDTO> result = BusinessLayer.Facades.Facade.ProfileReportFacade().GetProfileReportsByReportTypeID(ReportTypeID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileReportDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        [Route("ProfileReports/Profile/{ProfileID}/data", Name = "GetProfileReportsDataTablesByProfileID}")]
        [HttpPost]
        public DTResult<ProfileReportDTO> GetProfileReportsByProfileID(HttpRequestMessage request, int ProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileReportDTO> result = BusinessLayer.Facades.Facade.ProfileReportFacade().FilterResultByProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileID);
            return result;
        }
            
        [Route("ProfileReports/Profile/{ProfileID}/{page}/{pagesize}", Name = "GetProfileReportsByProfile")]
        [HttpGet]
        public HttpResponseMessage GetProfileReportsByProfileID(HttpRequestMessage request, int ProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileReportDTO> result = BusinessLayer.Facades.Facade.ProfileReportFacade().GetProfileReportsByProfileID(ProfileID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileReportDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        [Route("ProfileReports/Revenue/{RevenueID}/data", Name = "GetProfileReportsDataTablesByRevenueID}")]
        [HttpPost]
        public DTResult<ProfileReportDTO> GetProfileReportsByRevenueID(HttpRequestMessage request, int RevenueID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileReportDTO> result = BusinessLayer.Facades.Facade.ProfileReportFacade().FilterResultByRevenueID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, RevenueID);
            return result;
        }
            
        [Route("ProfileReports/Revenue/{RevenueID}/{page}/{pagesize}", Name = "GetProfileReportsByRevenue")]
        [HttpGet]
        public HttpResponseMessage GetProfileReportsByRevenueID(HttpRequestMessage request, int RevenueID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileReportDTO> result = BusinessLayer.Facades.Facade.ProfileReportFacade().GetProfileReportsByRevenueID(RevenueID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ProfileReportDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("ProfileReport", Name = "ProfileReportInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ProfileReportDTO objProfileReport)
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
                    ProfileReportDTO result = BusinessLayer.Facades.Facade.ProfileReportFacade().Insert(objProfileReport);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileReportDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("ProfileReport/ProfileReportID/{ProfileReportID}", Name = "DeleteProfileReport")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int ProfileReportID)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileReportFacade().Delete(ProfileReportID);
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


        [Route("ProfileReport/ProfileReportID/{ProfileReportID}", Name = "UpdateProfileReport")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ProfileReportID, ProfileReportDTO objProfileReport)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileReportFacade().Update(ProfileReportID,objProfileReport);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileReportDTO>(HttpStatusCode.OK, objProfileReport);
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