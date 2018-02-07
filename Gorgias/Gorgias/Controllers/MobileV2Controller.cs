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
        [Route("Mobile/V2/Profiles/Keyword/{keyword}", Name = "GetV2MobileProfilesByKeyword")]
        [HttpGet]
        public HttpResponseMessage GetProfilesByKeyword(HttpRequestMessage request, string keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<ProfileMobileModel> result = BusinessLayer.Facades.Facade.ProfileFacade().getProfilesByKeyword(keyword);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ProfileMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

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

        [Route("Mobile/V2/Album/Story/Edit/{AlbumID}/{ProfileID}/{DeviceWidth}", Name = "GetV2MobileAlbumEditStory")]
        [HttpGet]
        public HttpResponseMessage GetAlbumEditStory(HttpRequestMessage request, int AlbumID, int ProfileID, int DeviceWidth)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AlbumUpdateV2MobileModel result = BusinessLayer.Facades.Facade.AlbumFacade().getAlbumForEdit(AlbumID, ProfileID, DeviceWidth);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<AlbumUpdateV2MobileModel>(HttpStatusCode.OK, result);
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

        [Route("Mobile/V2/Album/Request/Repost/{AlbumID}", Name = "GetV2MobileAlbumRequestToRepost")]
        [HttpGet]
        public HttpResponseMessage RequestRepostAlbum(HttpRequestMessage request, int AlbumID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.AlbumFacade().RequestRepostAlbum(AlbumID);
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

        [Route("Mobile/V2/Categories/Main/{ProfileID}", Name = "GetV2MobileCategoriesMain")]
        [HttpGet]
        public HttpResponseMessage GetCategoriesMain(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<CategoryMobileModel> result;

                if (ProfileID > 0)
                {
                    result = BusinessLayer.Facades.Facade.CategoryFacade().getCategoriesAvailableByProfile(ProfileID, headerLanguage.First().Value);
                } else
                {
                    result = BusinessLayer.Facades.Facade.CategoryFacade().getCategories(headerLanguage.First().Value);
                }
                
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

        [Route("Mobile/V2/Categories/Search/{Keyword}", Name = "GetV2MobileCategoriesBySearch")]
        [HttpGet]
        public HttpResponseMessage GetCategories(HttpRequestMessage request, string Keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.CategoryFacade().getCategoriesBySearch(Keyword);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<KeyValueMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Categories/Profile/{ProfileID}", Name = "GetV2MobileCategoriesByProfileID")]
        [HttpGet]
        public HttpResponseMessage GetCategoriesByProfileID(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.CategoryFacade().getCategoriesByProfile(ProfileID, headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<KeyValueMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Categories/{CategoryParentID}", Name = "GetV2MobileCategoriesByParentID")]
        [HttpGet]
        public HttpResponseMessage GetCategoriesByCategoryParentID(HttpRequestMessage request, int CategoryParentID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                //IEnumerable<CategoryMobileModel> result = BusinessLayer.Facades.Facade.CategoryFacade().getCategories(CategoryParentID, headerLanguage.First().Value);
                IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.CategoryFacade().getCategories(CategoryParentID, headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<KeyValueMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Profile/Subscribes/{ProfileID}", Name = "GetV2MobileProfileSubscribes")]
        [HttpGet]
        public HttpResponseMessage GetProfileConnectionsByProfileID(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;                
                IEnumerable<ProfileSubscribeMobileModel> result = BusinessLayer.Facades.Facade.ConnectionFacade().getProfileSubscribes(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ProfileSubscribeMobileModel>>(HttpStatusCode.OK, result);
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
                IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.ProfileTypeFacade().getProfileTypesByLanguageCodeByKeyValue(headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<KeyValueMobileModel>>(HttpStatusCode.OK, result);
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
                IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.IndustryFacade().getIndustriesByKeyValue(headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<KeyValueMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Industries/{Keyword}", Name = "GetV2MobileIndustriesByKeyword")]
        [HttpGet]
        public HttpResponseMessage GetIndustriesByKeyword(HttpRequestMessage request, string Keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.IndustryFacade().getIndustriesByKeywordByKeyValue(Keyword);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<KeyValueMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Cities/{searchKey}", Name = "GetV2MobileCitiesByCountryID")]
        [HttpGet]
        public HttpResponseMessage GetCities(HttpRequestMessage request, string searchKey)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.CityFacade().getCities(searchKey, headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<KeyValueMobileModel>>(HttpStatusCode.OK, result);
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
                IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.CountryFacade().getCountries(headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<KeyValueMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Countries/{keyword}", Name = "GetV2MobileCountriesByKeyword")]
        [HttpGet]
        public HttpResponseMessage GetCountriesByKeyword(HttpRequestMessage request, string keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.CountryFacade().getCountries(headerLanguage.First().Value, keyword);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<KeyValueMobileModel>>(HttpStatusCode.OK, result);
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
                //IEnumerable<ContentRatingMobileModel> result = BusinessLayer.Facades.Facade.ContentRatingFacade().getContentRatings(headerLanguage.First().Value);
                IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.ContentRatingFacade().getContentRatingsByKeyValue(headerLanguage.First().Value);

                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<KeyValueMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Story/Settings/{ProfileID}/{CategoryParentID}/{ProfileIsConfirmed}", Name = "GetV2MobileStorySettingsNima")]
        [HttpGet]
        public HttpResponseMessage GetStorySettings(HttpRequestMessage request, int ProfileID, int CategoryParentID, bool ProfileIsConfirmed)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                //IEnumerable<ContentRatingMobileModel> result = BusinessLayer.Facades.Facade.ContentRatingFacade().getContentRatings(headerLanguage.First().Value);
                IEnumerable<SettingMobileModel> result = BusinessLayer.Facades.Facade.MobileFacade().getSettings(ProfileID, headerLanguage.First().Value, CategoryParentID, ProfileIsConfirmed);

                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<SettingMobileModel>>(HttpStatusCode.OK, result);
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
                //IEnumerable<LanguageMobileModel> result = BusinessLayer.Facades.Facade.LanguageFacade().getLanguages();
                IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.LanguageFacade().getLanguagesByKeyValue();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<KeyValueMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Profile/Reader/Languages/{ProfileID}", Name = "GetV2MobileProfileReaderLanguages")]
        [HttpGet]
        public HttpResponseMessage GetProfileReaderLanguages(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<ProfileReadingLanguageMobileModel> result = BusinessLayer.Facades.Facade.LanguageFacade().getReadingLanguages(ProfileID);
                //IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.LanguageFacade().getLanguagesByKeyValue();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Profile/Update/Reader/Languages/{ProfileID}", Name = "UpdateV2MobileProfileReaderLanguages")]
        [HttpPost]
        public HttpResponseMessage UpdateProfileReaderLanguages(HttpRequestMessage request, int ProfileID, IEnumerable<ProfileReadingLanguageMobileModel> languages)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.ProfileReadingFacade().updateProfileLanguageReadings(languages, ProfileID);
                //IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.LanguageFacade().getLanguagesByKeyValue();
                if (!result)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.OK, result);
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

        [Route("Mobile/V2/Album/{AlbumID}/{ProfileID}/{DeviceWidth}", Name = "GetV2MobileAlbumDetail")]
        [HttpGet]
        public HttpResponseMessage GetAlbum(HttpRequestMessage request, int AlbumID, int ProfileID, int DeviceWidth)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AlbumMobileModel result = BusinessLayer.Facades.Facade.AlbumFacade().getAlbum(AlbumID, ProfileID, DeviceWidth);
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

        [Route("Mobile/V2/Album/{AlbumID}/{ProfileID}/{DeviceWidth}", Name = "GetV2MobileAlbumDetailWithActivity")]
        [HttpPost]
        public HttpResponseMessage GetAlbum(HttpRequestMessage request, int AlbumID, int ProfileID, int DeviceWidth, ProfileActivityUpdateMobileModel Activity)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AlbumMobileModel result = BusinessLayer.Facades.Facade.AlbumFacade().getAlbum(AlbumID, ProfileID, DeviceWidth, Activity);
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

        [Route("Mobile/V2/Profile/Setting/{ProfileID}", Name = "GetV2MobileSettingProfile")]
        [HttpGet]
        public HttpResponseMessage GetSettingProfile(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                SettingProfileMobileModel result = BusinessLayer.Facades.Facade.ProfileFacade().GetV2SettingMobileProfile(ProfileID, headerLanguage.First().Value);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<SettingProfileMobileModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Profile/MicroApp/{ProfileID}", Name = "GetV2MobileMicroAppProfile")]
        [HttpGet]
        public HttpResponseMessage GetMicroAppProfile(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string result = BusinessLayer.Facades.Facade.ProfileFacade().getProfileFullname(ProfileID);
                if (result == "")
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<string>(HttpStatusCode.OK, result);
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

        [Route("Mobile/V2/Profile/Account/Setting/{ProfileID}", Name = "GetProfileAccountSettingMobileV2")]
        [HttpGet]
        public HttpResponseMessage ProfileAccountSetting(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var headerLanguage = request.Headers.AcceptLanguage;
                LoginProfileMobileModel result = BusinessLayer.Facades.Facade.ProfileFacade().getProfileSetting(ProfileID, headerLanguage.First().Value);

                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<LoginProfileMobileModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Profile/Management/{UserID}", Name = "GetProfileManagementByUserIDMobileV2")]
        [HttpGet]
        public HttpResponseMessage Subscriber(HttpRequestMessage request, int UserID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<UserProfileMobileModel> result = BusinessLayer.Facades.Facade.UserProfileFacade().getUserProfiles(UserID);

                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<UserProfileMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Profile/MicroApp/Subscribe/Status/{ProfileID}/{MicroAppProfileID}", Name = "SubscriberBookmarkFromMicroAppMobileV2")]
        [HttpGet]
        public HttpResponseMessage SubscriberFromMicroApp(HttpRequestMessage request, int ProfileID, int MicroAppProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string result = BusinessLayer.Facades.Facade.ConnectionFacade().InsertBookmarkFromMicroApp(ProfileID, MicroAppProfileID);
                response = request.CreateResponse<string>(HttpStatusCode.OK, result);
                return response;
            });
        }

        [Route("Mobile/V2/Subscriber/Bookmark/{ProfileID}/{UserProfileID}", Name = "SubscriberBookmarkMobileV2")]
        [HttpGet]
        public HttpResponseMessage Subscriber(HttpRequestMessage request, int ProfileID, int UserProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.ConnectionFacade().Update(ProfileID, UserProfileID);

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

        //From V1
        [Route("Mobile/V2/Subscriber/InApp/{Mode}/{ProfileID}/{UserProfileID}", Name = "SubscriberInAppMobileV2")]
        [HttpGet]
        public HttpResponseMessage SubscriberInApp(HttpRequestMessage request, int Mode, int ProfileID, int UserProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.ConnectionFacade().InsertInAppForMobile(new Business.DataTransferObjects.ConnectionDTO { ProfileID = ProfileID, RequestedProfileID = UserProfileID, RequestTypeID = Mode });

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

        [Route("Mobile/V2/Profile/NewUser", Name = "CreateV2NewMobileUser")]
        [HttpGet]
        public HttpResponseMessage CreateNewMobileUser(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //int result = BusinessLayer.Facades.Facade.WebFacade().createNewMobileUser();
                NewUserRegisterMobileModel result = BusinessLayer.Facades.Facade.ProfileFacade().createNewMobileUser();
                //NewUserRegisterMobileModel
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<NewUserRegisterMobileModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/V2/Profile/New/User/PhoneSetting", Name = "CreateV2NewMobileUserWithDeviceID")]
        [HttpPost]
        public HttpResponseMessage CreateNewMobileUser(HttpRequestMessage request, PhoneConfigMobileModel phoneConfig)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int result = BusinessLayer.Facades.Facade.ProfileFacade().createNewMobileUser(phoneConfig);
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

        [Route("Mobile/V2/Profile/User/PhoneSetting/{DeviceLanguage}/{ProfileID}", Name = "UpdateV2ProfileDeviceLanguage")]
        [HttpGet]
        public HttpResponseMessage UpdateProfileDeviceLanguage(HttpRequestMessage request, string deviceLanguage, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.ProfileFacade().updateProfileDeviceLanguage(deviceLanguage, ProfileID);
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

        [Route("Mobile/V2/Album/Availability/{ProfileID}", Name = "GetMobileV2AlbumAvailability")]
        [HttpGet]
        public HttpResponseMessage GetAlbumAvailability(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //IEnumerable<Business.DataTransferObjects.Mobile.AlbumAvailabilityModel> result = BusinessLayer.Facades.Facade.WebFacade().AlbumAvailability(ProfileID);
                IEnumerable<KeyValueMobileModel> result = BusinessLayer.Facades.Facade.AlbumTypeFacade().getAvailabilities();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<KeyValueMobileModel>>(HttpStatusCode.OK, result);
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
        [Route("Mobile/V2/Contents/Likes/Old", Name = "UpdateV2ContentsLikes")]
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
                    if (result.ProfileEmail.ToLower().Equals(loginAttempt.ProfileEmail.ToLower()))
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

        [Route("Mobile/V2/Profile/Register/Full", Name = "GetV2MobileRegisterNewUserFull")]
        [HttpPost]
        public HttpResponseMessage ProfileRegisterFull(HttpRequestMessage request, RegisterProfileMobileModel profileRegisterMobileModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.ProfileFacade().registerProfile(profileRegisterMobileModel);
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

        [Route("Mobile/V2/Albums/Full/Filter", Name = "GetV2MobileAlbumFullFilter")]
        [HttpPost]
        public HttpResponseMessage AlbumFullFilter(HttpRequestMessage request, AlbumFilterMobileModel albumFilterMobileModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<AlbumMobileModel> result = BusinessLayer.Facades.Facade.AlbumFacade().getAlbumsFull(albumFilterMobileModel);
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

        [Route("Mobile/V2/Contents/Likes/{ProfileID}/{AlbumID}", Name = "MobileV2ContentsLikesInsert")]
        [HttpPost]
        public HttpResponseMessage ContentLikesInsert(HttpRequestMessage request, ContentLikeMobileModel[] contentLikes, int ProfileID, int AlbumID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                bool result = BusinessLayer.Facades.Facade.ContentFacade().UpdateContentsLikes(contentLikes, ProfileID, AlbumID);
                if (result)
                {
                    response = request.CreateResponse<bool>(HttpStatusCode.Created, result);
                }
                else
                {
                    response = request.CreateResponse<bool>(HttpStatusCode.NotAcceptable, false);
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

        [Route("Mobile/V2/Album/New/Topic", Name = "MobileV2AlbumInsertWithNewTopic")]
        [HttpPost]
        public HttpResponseMessage AlbumWithTopicInsert(HttpRequestMessage request, AlbumUpdateV2MobileModel objAlbum)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Business.DataTransferObjects.AlbumDTO result = BusinessLayer.Facades.Facade.AlbumFacade().InsertV2Topic(objAlbum);
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

        [Route("Mobile/V2/Album/Edit/Topic", Name = "MobileV2AlbumUpdateWithNewTopic")]
        [HttpPost]
        public HttpResponseMessage AlbumWithTopicUpdate(HttpRequestMessage request, AlbumUpdateV2MobileModel objAlbum)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Business.DataTransferObjects.AlbumDTO result = BusinessLayer.Facades.Facade.AlbumFacade().UpdateV2Topic(objAlbum);
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

        [Route("Mobile/V2/Album/Story/Manage/{TypeName}", Name = "MobileV2AlbumInsertUpdateWithNewTopic")]
        [HttpPost]
        public HttpResponseMessage AlbumWithTopicInsertUpdate(HttpRequestMessage request, AlbumUpdateV2MobileModel objAlbum, string TypeName)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Business.DataTransferObjects.AlbumDTO result;
                if (TypeName.Equals("Insert"))
                {
                    result = BusinessLayer.Facades.Facade.AlbumFacade().InsertV2Topic(objAlbum);
                } else
                {
                    result = BusinessLayer.Facades.Facade.AlbumFacade().UpdateV2Topic(objAlbum);
                }
                
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
