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
    public class ProfileActivityController : ApiControllerBase
    {
        [Route("ProfileActivity/ProfileID/AlbumID/{ProfileID}/{AlbumID}", Name = "GetProfileActivityByID")]
        [HttpGet]
        public HttpResponseMessage GetProfileActivity(HttpRequestMessage request, int ProfileID, int AlbumID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProfileActivityDTO result = BusinessLayer.Facades.Facade.ProfileActivityFacade().GetProfileActivity(ProfileID, AlbumID);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<ProfileActivityDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("ProfileActivities/data", Name = "GetProfileActivitiesDataTables")]
        [HttpPost]
        public DTResult<ProfileActivityDTO> GetProfileActivities(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileActivityDTO> result = BusinessLayer.Facades.Facade.ProfileActivityFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("ProfileActivities", Name = "GetProfileActivitiesAll")]
        [HttpGet]
        public HttpResponseMessage GetProfileActivities(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProfileActivityDTO> result = BusinessLayer.Facades.Facade.ProfileActivityFacade().GetProfileActivities();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<List<ProfileActivityDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("ProfileActivities/{page}/{pagesize}", Name = "GetProfileActivities")]
        [HttpGet]
        public HttpResponseMessage GetProfileActivities(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileActivityDTO> result = BusinessLayer.Facades.Facade.ProfileActivityFacade().GetProfileActivities(page, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<ProfileActivityDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }



        [Route("ProfileActivities/AlbumID/{AlbumID}/data", Name = "GetProfileActivitiesDataTablesByAlbumID}")]
        [HttpPost]
        public DTResult<ProfileActivityDTO> GetProfileActivitiesByAlbumID(HttpRequestMessage request, int AlbumID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileActivityDTO> result = BusinessLayer.Facades.Facade.ProfileActivityFacade().FilterResultByAlbumID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, AlbumID);
            return result;
        }

        [Route("ProfileActivities/AlbumID/{AlbumID}/{page}/{pagesize}", Name = "GetProfileActivitiesByAlbumID")]
        [HttpGet]
        public HttpResponseMessage GetProfileActivitiesByAlbumID(HttpRequestMessage request, int AlbumID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileActivityDTO> result = BusinessLayer.Facades.Facade.ProfileActivityFacade().GetProfileActivitiesByAlbumID(AlbumID, page, pagesize);

                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<ProfileActivityDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }
        [Route("ProfileActivities/ActivityType/{ActivityTypeID}/data", Name = "GetProfileActivitiesDataTablesByActivityTypeID}")]
        [HttpPost]
        public DTResult<ProfileActivityDTO> GetProfileActivitiesByActivityTypeID(HttpRequestMessage request, int ActivityTypeID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ProfileActivityDTO> result = BusinessLayer.Facades.Facade.ProfileActivityFacade().FilterResultByActivityTypeID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ActivityTypeID);
            return result;
        }

        [Route("ProfileActivities/ActivityType/{ActivityTypeID}/{page}/{pagesize}", Name = "GetProfileActivitiesByActivityType")]
        [HttpGet]
        public HttpResponseMessage GetProfileActivitiesByActivityTypeID(HttpRequestMessage request, int ActivityTypeID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ProfileActivityDTO> result = BusinessLayer.Facades.Facade.ProfileActivityFacade().GetProfileActivitiesByActivityTypeID(ActivityTypeID, page, pagesize);

                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<ProfileActivityDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("ProfileActivity", Name = "ProfileActivityInsert")]
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ProfileActivityDTO objProfileActivity)
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
                   ProfileActivityDTO result = BusinessLayer.Facades.Facade.ProfileActivityFacade().Insert(objProfileActivity);
                   if (result != null)
                   {
                       response = request.CreateResponse<Business.DataTransferObjects.ProfileActivityDTO>(HttpStatusCode.Created, result);
                   }
                   else
                   {
                       response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                   }
               }
               return response;
           });
        }


        [Route("ProfileActivity/ProfileID/AlbumID/{ProfileID}/{AlbumID}", Name = "DeleteProfileActivity")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int ProfileID, int AlbumID)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileActivityFacade().Delete(ProfileID, AlbumID);
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


        [Route("ProfileActivity/ProfileID/AlbumID/{ProfileID}/{AlbumID}", Name = "UpdateProfileActivity")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ProfileID, int AlbumID, ProfileActivityDTO objProfileActivity)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileActivityFacade().Update(ProfileID, AlbumID, objProfileActivity);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ProfileActivityDTO>(HttpStatusCode.OK, objProfileActivity);
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