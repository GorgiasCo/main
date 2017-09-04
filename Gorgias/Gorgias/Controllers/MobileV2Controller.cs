﻿using Gorgias.Business.DataTransferObjects.Mobile.V2;
using Gorgias.Infrastruture.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gorgias.Controllers
{
    [RoutePrefix("api")]
    public class MobileV2Controller : ApiControllerBase
    {
        [Route("Mobile/V2/Categories", Name = "GetV2MobileCategories")]
        [HttpGet]
        public HttpResponseMessage GetCategories(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<CategoryMobileModel> result = BusinessLayer.Facades.Facade.CategoryFacade().getCategories(headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<CategoryMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Felts/{ActivityTypeParentID}", Name = "GetV2MobileActivities")]
        [HttpGet]
        public HttpResponseMessage GetActivityTypes(HttpRequestMessage request, int ActivityTypeParentID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<ActivityTypeMobileModel> result = BusinessLayer.Facades.Facade.ActivityTypeFacade().getActivityTypes(headerLanguage.First().Value, ActivityTypeParentID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ActivityTypeMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Content/Ratings", Name = "GetV2MobileContentRatings")]
        [HttpGet]
        public HttpResponseMessage GetContentRatings(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<ContentRatingMobileModel> result = BusinessLayer.Facades.Facade.ContentRatingFacade().getContentRatings(headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ContentRatingMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Languages", Name = "GetV2MobileLanguages")]
        [HttpGet]
        public HttpResponseMessage GetLanguages(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<LanguageMobileModel> result = BusinessLayer.Facades.Facade.LanguageFacade().getLanguages();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<LanguageMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Quotes", Name = "GetV2MobileQuotes")]
        [HttpGet]
        public HttpResponseMessage GetQuotes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<QuoteMobileModel> result = BusinessLayer.Facades.Facade.QuoteFacade().getQuotes(headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<QuoteMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Content/Types/{ContentTypeID}", Name = "GetV2MobileContentTypes")]
        [HttpGet]
        public HttpResponseMessage GetContentTypes(HttpRequestMessage request, int ContentTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<ContentTypeMobileModel> result = BusinessLayer.Facades.Facade.ContentTypeFacade().getContentTypes(ContentTypeID, headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ContentTypeMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Profile/Readings", Name = "GetV2MobileProfileReadings")]
        [HttpPost]
        public HttpResponseMessage UpdateProfileReadings(HttpRequestMessage request, Business.DataTransferObjects.Mobile.V2.ProfileReadingMobileModel profileReadingMobileModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.ProfileReadingFacade().updateProfileReadings(profileReadingMobileModel);
                if (!result)
                {
                    response = request.CreateResponse<bool>(HttpStatusCode.NotFound, false);
                }
                else
                {
                    response = request.CreateResponse<bool>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        //From V1
        [Route("Mobile/V2/Subscriber/{Mode}/{ProfileID}/{UserProfileID}", Name = "SubscriberMobileV2")]
        [HttpGet]
        public HttpResponseMessage Subscriber(HttpRequestMessage request, int Mode, int ProfileID, int UserProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.ConnectionFacade().InsertForMobile(new Business.DataTransferObjects.ConnectionDTO { ProfileID = ProfileID, RequestedProfileID = UserProfileID, RequestTypeID = Mode });

                if (!result)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<bool>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Comments/Content/{ContentID}/{pagesize}/{page}", Name = "GetMobileV2CommentsByContent")]
        [HttpGet]
        public HttpResponseMessage GetCommentsByContentID(HttpRequestMessage request, int ContentID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<Business.DataTransferObjects.Mobile.CommentModel> result = BusinessLayer.Facades.Facade.CommentFacade().GetMobileCommentsByContentID(ContentID, page, pagesize);

                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<Business.DataTransferObjects.Mobile.CommentModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Comment", Name = "CommentV2MobileInsert")]
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.Mobile.CommentCustomModel objComment)
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
                    Business.DataTransferObjects.CommentDTO result = BusinessLayer.Facades.Facade.CommentFacade().Insert(objComment);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.CommentDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<string>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });
        }

        [Route("Mobile/V2/Album/Comment/Status/{AlbumID}", Name = "UpdateV2AlbumCommentStatus")]
        [HttpGet]
        public HttpResponseMessage UpdateAlbumComment(HttpRequestMessage request, int AlbumID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.AlbumFacade().UpdateAlbumComment(AlbumID);

                response = request.CreateResponse<bool>(HttpStatusCode.OK, result);
                return response;
            });
        }

        [Route("Mobile/Profile/NewUser", Name = "CreateV2NewMobileUser")]
        [HttpGet]
        public HttpResponseMessage CreateNewMobileUser(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int result = BusinessLayer.Facades.Facade.WebFacade().createNewMobileUser();
                if (result == 0)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<int>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Album/Availability/{ProfileID}", Name = "GetMobileV2AlbumAvailability")]
        [HttpGet]
        public HttpResponseMessage GetAlbumAvailability(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<Business.DataTransferObjects.Mobile.AlbumAvailabilityModel> result = BusinessLayer.Facades.Facade.WebFacade().AlbumAvailability(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<Business.DataTransferObjects.Mobile.AlbumAvailabilityModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Content/Like/{ContentID}", Name = "UpdateV2ContentLike")]
        [HttpGet]
        public HttpResponseMessage UpdateContnetLike(HttpRequestMessage request, int ContentID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().updateContentLike(ContentID);
                response = request.CreateResponse<bool>(HttpStatusCode.OK, result);
                return response;
            });
        }

        [Route("Mobile/V2/Contents/Likes", Name = "UpdateV2ContentsLikes")]
        [HttpPost]
        public HttpResponseMessage UpdateContnetsLikes(HttpRequestMessage request, Business.DataTransferObjects.Mobile.ContentLikesModel[] ContentLikes)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().updateContentLike(ContentLikes);
                response = request.CreateResponse<bool>(HttpStatusCode.OK, result);
                return response;
            });
        }

        [Route("Mobile/V2/Album/View/{AlbumID}", Name = "UpdateV2AlbumView")]
        [HttpGet]
        public HttpResponseMessage UpdateAlbumView(HttpRequestMessage request, int AlbumID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().updateAlbumView(AlbumID);
                response = request.CreateResponse<bool>(HttpStatusCode.OK, result);
                return response;
            });
        }

        [Route("Mobile/V2/Profile/Update/", Name = "UpdateV2ProfileModelFullname")]
        [HttpPost]
        public HttpResponseMessage UpdateProfileModel(HttpRequestMessage request, Business.DataTransferObjects.Mobile.ProfileUpdateModel profileUpdateModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.ProfileFacade().Update(profileUpdateModel);
                if (!result)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<bool>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }
        //End From V1

        [Route("Mobile/V2/Register/Profile/Token", Name = "GetV2MobileRegisterProfileToken")]
        [HttpPost]
        public HttpResponseMessage LoginAttempt(HttpRequestMessage request, Business.DataTransferObjects.Mobile.V2.ProfileTokenMobileModel profileTokenMobileModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.ProfileTokenFacade().RegisterProfileToken(profileTokenMobileModel);
                if (!result)
                {
                    response = request.CreateResponse<bool>(HttpStatusCode.NotFound, false);
                }
                else
                {
                    response = request.CreateResponse<bool>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Login/Attempt", Name = "GetV2MobileLoginAttempt")]
        [HttpPost]
        public HttpResponseMessage LoginAttempt(HttpRequestMessage request, LoginAttempt loginAttempt)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                LoginAttempt result = BusinessLayer.Facades.Facade.ProfileFacade().getLoginAttempt(loginAttempt.ProfileEmail, loginAttempt.ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    if (result.ProfileEmail.Equals(loginAttempt.ProfileEmail))
                    {
                        result.isValid = true;
                    }
                    else
                    {
                        result.isValid = false;
                    }
                    response = request.CreateResponse<LoginAttempt>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }
    }
}