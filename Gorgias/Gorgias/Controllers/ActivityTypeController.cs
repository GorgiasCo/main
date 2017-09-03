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
    public class ActivityTypeController : ApiControllerBase
    {
        [Route("ActivityType/ActivityTypeID/{ActivityTypeID}", Name = "GetActivityTypeByID")]
        [HttpGet]
        public HttpResponseMessage GetActivityType(HttpRequestMessage request, int ActivityTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ActivityTypeDTO result = BusinessLayer.Facades.Facade.ActivityTypeFacade().GetActivityType(ActivityTypeID);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<ActivityTypeDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("ActivityTypes/data", Name = "GetActivityTypesDataTables")]
        [HttpPost]
        public DTResult<ActivityTypeDTO> GetActivityTypes(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ActivityTypeDTO> result = BusinessLayer.Facades.Facade.ActivityTypeFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ActivityTypes", Name = "GetActivityTypesAll")]
        [HttpGet]
        public HttpResponseMessage GetActivityTypes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ActivityTypeDTO> result = BusinessLayer.Facades.Facade.ActivityTypeFacade().GetActivityTypes();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<List<ActivityTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("ActivityTypes/{page}/{pagesize}", Name = "GetActivityTypes")]
        [HttpGet]
        public HttpResponseMessage GetActivityTypes(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ActivityTypeDTO> result = BusinessLayer.Facades.Facade.ActivityTypeFacade().GetActivityTypes(page, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<ActivityTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }



        [Route("ActivityTypes/ActivityTypeParent/{ActivityTypeParentID}/data", Name = "GetActivityTypesDataTablesByActivityTypeParentID}")]
        [HttpPost]
        public DTResult<ActivityTypeDTO> GetActivityTypesByActivityTypeParentID(HttpRequestMessage request, int ActivityTypeParentID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ActivityTypeDTO> result = BusinessLayer.Facades.Facade.ActivityTypeFacade().FilterResultByActivityTypeParentID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ActivityTypeParentID);
            return result;
        }

        [Route("ActivityTypes/ActivityTypeParent/{ActivityTypeParentID}/{page}/{pagesize}", Name = "GetActivityTypesByActivityTypeParent")]
        [HttpGet]
        public HttpResponseMessage GetActivityTypesByActivityTypeParentID(HttpRequestMessage request, int ActivityTypeParentID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ActivityTypeDTO> result = BusinessLayer.Facades.Facade.ActivityTypeFacade().GetActivityTypesByActivityTypeParentID(ActivityTypeParentID, page, pagesize);

                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<ActivityTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("ActivityType", Name = "ActivityTypeInsert")]
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ActivityTypeDTO objActivityType)
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
                   ActivityTypeDTO result = BusinessLayer.Facades.Facade.ActivityTypeFacade().Insert(objActivityType);
                   if (result != null)
                   {
                       response = request.CreateResponse<Business.DataTransferObjects.ActivityTypeDTO>(HttpStatusCode.Created, result);
                   }
                   else
                   {
                       response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                   }
               }
               return response;
           });
        }


        [Route("ActivityType/ActivityTypeID/{ActivityTypeID}", Name = "DeleteActivityType")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int ActivityTypeID)
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
                    bool result = BusinessLayer.Facades.Facade.ActivityTypeFacade().Delete(ActivityTypeID);
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


        [Route("ActivityType/ActivityTypeID/{ActivityTypeID}", Name = "UpdateActivityType")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ActivityTypeID, ActivityTypeDTO objActivityType)
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
                    bool result = BusinessLayer.Facades.Facade.ActivityTypeFacade().Update(ActivityTypeID, objActivityType);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ActivityTypeDTO>(HttpStatusCode.OK, objActivityType);
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