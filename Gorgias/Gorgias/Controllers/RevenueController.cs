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
    public class RevenueController : ApiControllerBase
    {
        [Route("Revenue/RevenueID/{RevenueID}", Name = "GetRevenueByID")]
        [HttpGet]
        public HttpResponseMessage GetRevenue(HttpRequestMessage request, int RevenueID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                RevenueDTO result = BusinessLayer.Facades.Facade.RevenueFacade().GetRevenue(RevenueID);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<RevenueDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Revenues/data", Name = "GetRevenuesDataTables")]
        [HttpPost]
        public DTResult<RevenueDTO> GetRevenues(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<RevenueDTO> result = BusinessLayer.Facades.Facade.RevenueFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Revenues", Name = "GetRevenuesAll")]
        [HttpGet]
        public HttpResponseMessage GetRevenues(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<RevenueDTO> result = BusinessLayer.Facades.Facade.RevenueFacade().GetRevenues();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<List<RevenueDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Revenues/{page}/{pagesize}", Name = "GetRevenues")]
        [HttpGet]
        public HttpResponseMessage GetRevenues(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<RevenueDTO> result = BusinessLayer.Facades.Facade.RevenueFacade().GetRevenues(page, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<RevenueDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Revenue", Name = "RevenueInsert")]
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.RevenueDTO objRevenue)
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
                   RevenueDTO result = BusinessLayer.Facades.Facade.RevenueFacade().Insert(objRevenue);
                   if (result != null)
                   {
                       response = request.CreateResponse<Business.DataTransferObjects.RevenueDTO>(HttpStatusCode.Created, result);
                   }
                   else
                   {
                       response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                   }
               }
               return response;
           });
        }

        //Must Run First for Generating Revenue and Profile Reports ;) FIRST
        [Route("Revenue/Auto", Name = "RevenueInsertAuto")]
        [HttpGet]
        public HttpResponseMessage RevenueInsertAuto(HttpRequestMessage request)
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
                    RevenueDTO result = BusinessLayer.Facades.Facade.RevenueFacade().Insert();
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.RevenueDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });
        }


        [Route("Revenue/RevenueID/{RevenueID}", Name = "DeleteRevenue")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int RevenueID)
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
                    bool result = BusinessLayer.Facades.Facade.RevenueFacade().Delete(RevenueID);
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


        [Route("Revenue/RevenueID/{RevenueID}", Name = "UpdateRevenue")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int RevenueID, RevenueDTO objRevenue)
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
                    bool result = BusinessLayer.Facades.Facade.RevenueFacade().Update(RevenueID, objRevenue);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.RevenueDTO>(HttpStatusCode.OK, objRevenue);
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