using Gorgias.Business.DataTransferObjects.Mobile.V2;
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
        [Route("Mobile/V2/Album/Story/{AlbumID}", Name = "GetV2MobileAlbumStory")]
        [HttpGet]
        public HttpResponseMessage GetAlbumStory(HttpRequestMessage request, int AlbumID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;                
                AlbumUpdateMobileModel result = BusinessLayer.Facades.Facade.AlbumFacade().GetAlbumV2(AlbumID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<AlbumUpdateMobileModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Profile/Preferences/{ProfileID}", Name = "GetV2MobileProfilePreferences")]
        [HttpGet]
        public HttpResponseMessage GetProfilePreferences(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Business.DataTransferObjects.ProfileDTO result = BusinessLayer.Facades.Facade.ProfileFacade().GetV2Profile(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<Business.DataTransferObjects.ProfileDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Album/Repost/{AlbumID}", Name = "GetV2MobileAlbumRepost")]
        [HttpGet]
        public HttpResponseMessage RepostAlbum(HttpRequestMessage request, int AlbumID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.AlbumFacade().RepostAlbum(AlbumID);
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

        [Route("Mobile/V2/Album/Publish/Upcoming/{AlbumID}", Name = "GetV2MobileAlbumPublishUpcoming")]
        [HttpGet]
        public HttpResponseMessage PublishAlbum(HttpRequestMessage request, int AlbumID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.AlbumFacade().PublishAlbum(AlbumID);
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

        [Route("Mobile/V2/ProfileTypes", Name = "GetV2MobileProfileTypes")]
        [HttpGet]
        public HttpResponseMessage GetProfileTypes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<ProfileTypeMobileModel> result = BusinessLayer.Facades.Facade.ProfileTypeFacade().getProfileTypesByLanguageCode(headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ProfileTypeMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Industries", Name = "GetV2MobileIndustries")]
        [HttpGet]
        public HttpResponseMessage GetIndustries(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<IndustryMobileModel> result = BusinessLayer.Facades.Facade.IndustryFacade().getIndustries(headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<IndustryMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Cities/{CountryID}", Name = "GetV2MobileCitiesByCountryID")]
        [HttpGet]
        public HttpResponseMessage GetCities(HttpRequestMessage request, int CountryID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<CityMobileModel> result = BusinessLayer.Facades.Facade.CityFacade().getCities(CountryID, headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<CityMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Countries/", Name = "GetV2MobileCountries")]
        [HttpGet]
        public HttpResponseMessage GetCountries(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<CountryMobileModel> result = BusinessLayer.Facades.Facade.CountryFacade().getCountries(headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<CountryMobileModel>>(HttpStatusCode.OK, result);
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

        [Route("Mobile/V2/Album/{AlbumID}/{ProfileID}", Name = "GetV2MobileAlbumDetail")]
        [HttpGet]
        public HttpResponseMessage GetAlbum(HttpRequestMessage request, int AlbumID, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;                
                AlbumMobileModel result = BusinessLayer.Facades.Facade.AlbumFacade().getAlbum(AlbumID,ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<AlbumMobileModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Profile/Mini/{ProfileID}/{RequestedProfileID}", Name = "GetV2MobileMiniProfile")]
        [HttpGet]
        public HttpResponseMessage GetMiniProfile(HttpRequestMessage request, int ProfileID, int RequestedProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                MiniProfileMobileModel result = BusinessLayer.Facades.Facade.ProfileFacade().GetV2MiniMobileProfile(ProfileID, RequestedProfileID, headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<MiniProfileMobileModel>(HttpStatusCode.OK, result);
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

        //Need Review ;)
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

        //Need to Review How ;)
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

        //Need to Review How ;)
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

        //Need to Review How ;)
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

        [Route("Mobile/V2/Profile/Register", Name = "GetV2MobileRegisterNewUser")]
        [HttpPost]
        public HttpResponseMessage ProfileRegister(HttpRequestMessage request, ProfileRegisterMobileModel profileRegisterMobileModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.ProfileFacade().registerProfile(profileRegisterMobileModel);
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

        [Route("Mobile/V2/Albums/Filter", Name = "GetV2MobileAlbumFilter")]
        [HttpPost]
        public HttpResponseMessage AlbumFilter(HttpRequestMessage request, AlbumFilterMobileModel albumFilterMobileModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<AlbumMobileModel> result = BusinessLayer.Facades.Facade.AlbumFacade().getAlbums(albumFilterMobileModel);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<AlbumMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Profile/Activity", Name = "MobileV2ProfileActivityInsert")]
        [HttpPost]
        public HttpResponseMessage ProfileActivityInsert(HttpRequestMessage request, Business.DataTransferObjects.Mobile.V2.ProfileActivityUpdateMobileModel objProfileActivity)
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
                    Business.DataTransferObjects.ProfileActivityDTO result = BusinessLayer.Facades.Facade.ProfileActivityFacade().Insert(objProfileActivity);
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

        [Route("Mobile/V2/Album/New", Name = "MobileV2AlbumInsert")]
        [HttpPost]
        public HttpResponseMessage AlbumInsert(HttpRequestMessage request, AlbumUpdateMobileModel objAlbum)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Business.DataTransferObjects.AlbumDTO result = BusinessLayer.Facades.Facade.AlbumFacade().InsertV2(objAlbum);
                if (result != null)
                {
                    response = request.CreateResponse<Business.DataTransferObjects.AlbumDTO>(HttpStatusCode.Created, result);
                }
                else
                {
                    response = request.CreateResponse<bool>(HttpStatusCode.NotAcceptable, false);
                }
                return response;
            });
        }
    }
}
