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
    public class ReportTypeController : ApiControllerBase
    {
        [Route("ReportType/ReportTypeID/{ReportTypeID}", Name = "GetReportTypeByID")]
        [HttpGet]
        public HttpResponseMessage GetReportType(HttpRequestMessage request, int ReportTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ReportTypeDTO result = BusinessLayer.Facades.Facade.ReportTypeFacade().GetReportType(ReportTypeID);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<ReportTypeDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("ReportTypes/data", Name = "GetReportTypesDataTables")]
        [HttpPost]
        public DTResult<ReportTypeDTO> GetReportTypes(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ReportTypeDTO> result = BusinessLayer.Facades.Facade.ReportTypeFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ReportTypes", Name = "GetReportTypesAll")]
        [HttpGet]
        public HttpResponseMessage GetReportTypes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ReportTypeDTO> result = BusinessLayer.Facades.Facade.ReportTypeFacade().GetReportTypes();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<List<ReportTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("ReportTypes/{page}/{pagesize}", Name = "GetReportTypes")]
        [HttpGet]
        public HttpResponseMessage GetReportTypes(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ReportTypeDTO> result = BusinessLayer.Facades.Facade.ReportTypeFacade().GetReportTypes(page, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<ReportTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }




        [Route("ReportType", Name = "ReportTypeInsert")]
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ReportTypeDTO objReportType)
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
                   ReportTypeDTO result = BusinessLayer.Facades.Facade.ReportTypeFacade().Insert(objReportType);
                   if (result != null)
                   {
                       response = request.CreateResponse<Business.DataTransferObjects.ReportTypeDTO>(HttpStatusCode.Created, result);
                   }
                   else
                   {
                       response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                   }
               }
               return response;
           });
        }


        [Route("ReportType/ReportTypeID/{ReportTypeID}", Name = "DeleteReportType")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int ReportTypeID)
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
                    bool result = BusinessLayer.Facades.Facade.ReportTypeFacade().Delete(ReportTypeID);
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


        [Route("ReportType/ReportTypeID/{ReportTypeID}", Name = "UpdateReportType")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ReportTypeID, ReportTypeDTO objReportType)
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
                    bool result = BusinessLayer.Facades.Facade.ReportTypeFacade().Update(ReportTypeID, objReportType);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ReportTypeDTO>(HttpStatusCode.OK, objReportType);
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