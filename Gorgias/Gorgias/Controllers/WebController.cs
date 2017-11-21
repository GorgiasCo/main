using Gorgias.Business.DataTransferObjects;
using Gorgias.Business.DataTransferObjects.Web;
using Gorgias.Business.DataTransferObjects.Web.List;
using Gorgias.DataLayer.Repository.SQL.Web;
using Gorgias.Infrastruture.Core;
using Gorgias.Infrastruture.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Gorgias.Controllers
{
    [RoutePrefix("api")]
    public class WebController : ApiControllerBase
    {
        [Route("Web/Validity", Name = "GetValidity")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetValidity(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                response = request.CreateResponse<string>(HttpStatusCode.Accepted, null);
                return response;
            });
        }

        [Route("Web/Notification/", Name = "SendNotification")]
        //[Authorize]
        [HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> SendNotification(HttpRequestMessage request, NotificationModel objNotification)
        {
            HttpResponseMessage response = null;

            FirebaseNet.Messaging.FCMClient client = new FirebaseNet.Messaging.FCMClient("AAAAH1qDRKw:APA91bHX1I5ohgU4_gm42LmgFf7Gem_7gxq0-TlXYXptzXGnpBj4i9pw2o7Um3CUqT03YUN0HwmqgtdHqWCYhMh8LZUAX0jSHja4GJYnNebGo8B_i5Q4IzZaY1wk6F52XSM3u-OA6FHo");

            var message = new FirebaseNet.Messaging.Message()
            {
                To = "/topics/" + objNotification.ChannelID,
                Notification = new FirebaseNet.Messaging.AndroidNotification()
                {
                    Body = objNotification.Body,
                    Title = objNotification.Title,
                    Icon = "myIcon",
                    Sound = "default",
                    ClickAction = "fcm.ACTION.HELLO"
                },
                Data = new Dictionary<string, string>
                {
                    {"AlbumID", objNotification.AlbumID },
                    {"ProfileID", objNotification.ChannelID },
                    {"canValidate", "true" },
                    {"remote", "true"},
                    {"Body", objNotification.Body},
                    {"Title", objNotification.Title},
                    {"click_action", "fcm.ACTION.HELLO"},
                }
            };

            var result = await client.SendMessageAsync(message);

            response = request.CreateResponse(HttpStatusCode.Accepted, result);
            return response;            
        }

        [Route("Web/Notification/V2/", Name = "SendV2Notification")]
        //[Authorize]
        [HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> SendV2Notification(HttpRequestMessage request, NotificationModel objNotification)
        {
            HttpResponseMessage response = null;

            FirebaseNet.Messaging.FCMClient client = new FirebaseNet.Messaging.FCMClient("AAAAH1qDRKw:APA91bHX1I5ohgU4_gm42LmgFf7Gem_7gxq0-TlXYXptzXGnpBj4i9pw2o7Um3CUqT03YUN0HwmqgtdHqWCYhMh8LZUAX0jSHja4GJYnNebGo8B_i5Q4IzZaY1wk6F52XSM3u-OA6FHo");

            var serializer = new JavaScriptSerializer();
            var data = new {AlbumID = objNotification.AlbumID, ProfileID = objNotification.ProfileID.Value, canValidate = true};
            var json = serializer.Serialize(data);

            var message = new FirebaseNet.Messaging.Message()
            {
                To = "/topics/" + objNotification.ChannelID,
                Notification = new FirebaseNet.Messaging.AndroidNotification()
                {
                    Body = objNotification.Body,
                    Title = objNotification.Title,
                    Icon = "myIcon",
                    Sound = "default",
                    ClickAction = "fcm.ACTION.HELLO"
                },
                Data = new Dictionary<string, string>
                {
                    {"AlbumID", objNotification.AlbumID },
                    {"ProfileID", objNotification.ProfileID.Value.ToString()},
                    {"canValidate", "true" },
                    {"extraData", json},
                    {"remote", "true"},
                    {"Body", objNotification.Body},
                    {"Title", objNotification.Title},
                    {"click_action", "fcm.ACTION.HELLO"},
                }
            };

            var result = await client.SendMessageAsync(message);

            response = request.CreateResponse(HttpStatusCode.Accepted, result);
            return response;
        }


        [Route("Web/Validity/{UserID}/{ProfileID}", Name = "GetValidityByUserIDANDProfileID")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetValidityByUserIDANDProfileID(HttpRequestMessage request, int UserID, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserProfileDTO result = BusinessLayer.Facades.Facade.WebFacade().getAdministrationAgencyProfileValidation(UserID, ProfileID);
                if (result != null)
                {
                    response = request.CreateResponse<UserProfileDTO>(HttpStatusCode.Accepted, result);
                }
                else
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                return response;
            });
        }

        [Route("Web/UserProfile/Agency/{UserID}", Name = "GetUserProfilesAgency")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetUserProfilesAgency(HttpRequestMessage request, int UserID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<UserProfileDTO> result = BusinessLayer.Facades.Facade.WebFacade().getUserProfilesAgency(UserID);
                if (result != null)
                {
                    response = request.CreateResponse<IEnumerable<UserProfileDTO>>(HttpStatusCode.Accepted, result);
                }
                else
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                return response;
            });
        }

        [Route("Web/UserProfile/Agency/Profile/{ProfileID}", Name = "GetUserProfilesAgencyForProfile")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetUserProfilesAgencyForProfile(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<UserProfileDTO> result = BusinessLayer.Facades.Facade.WebFacade().getUserProfilesAgencyForProfile(ProfileID);
                if (result != null)
                {
                    response = request.CreateResponse<IEnumerable<UserProfileDTO>>(HttpStatusCode.Accepted, result);
                }
                else
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                return response;
            });
        }

        [Route("Web/Cities", Name = "GetWebCities")]
        [HttpGet]
        public HttpResponseMessage GetCities(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<CityModel> result = BusinessLayer.Facades.Facade.WebFacade().getCities();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<CityModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/MainEntities", Name = "GetWebMainEntities")]
        [HttpGet]
        public HttpResponseMessage GetMainEntities(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                MainEntities result = BusinessLayer.Facades.Facade.WebFacade().getMainEntities();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<MainEntities>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Categories", Name = "GetWebCategories")]
        [HttpGet]
        public HttpResponseMessage GetCategories(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var header = request.Headers.AcceptLanguage;
                IEnumerable<CategoryModel> result = BusinessLayer.Facades.Facade.WebFacade().getCategories();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<CategoryModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/ProfileTypes", Name = "GetWebProfileTypes")]
        [HttpGet]
        public HttpResponseMessage GetProfileTypes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<ProfileTypeModel> result = BusinessLayer.Facades.Facade.WebFacade().getProfileTypes();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ProfileTypeModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/QR/{ProfileID}", Name = "GetWebQR")]
        [HttpGet]
        public HttpResponseMessage GetQRCode(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().GenerateQRCode(ProfileID);
                response = request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            });
        }

        [Route("Web/Subscribe/{ProfileID}/{RequestedProfileID}/{Status}", Name = "GetWebSubscribe")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Subscribe(HttpRequestMessage request, int ProfileID, int RequestedProfileID, bool Status)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string result = BusinessLayer.Facades.Facade.WebFacade().Subscribe(ProfileID, RequestedProfileID, Status);
                //if (!result)
                //{
                //    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                //}
                //else
                //{
                response = request.CreateResponse<string>(HttpStatusCode.OK, result);
                //}
                return response;
            });
        }

        [Route("Web/Administration/Agency/{UserEmail}/", Name = "GetWebAdministrationAgencyProfile")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetAgencyProfiles(HttpRequestMessage request, string UserEmail)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<UserProfileDTO> result = BusinessLayer.Facades.Facade.WebFacade().getAdministrationAgencyProfile(UserEmail);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<UserProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Administration/Country/{UserID}", Name = "GetWebAdministrationCountryProfile")]
        //[Authorize]
        [HttpGet]
        public HttpResponseMessage GetCountryProfiles(HttpRequestMessage request, int UserID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                // add s, UserProfile
                IEnumerable<ProfileDTO> result = BusinessLayer.Facades.Facade.WebFacade().getAdministrationCountryProfiles(UserID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ProfileDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }


        [Route("Web/Administration/Agency/{UserID}/{ProfileID}", Name = "GetWebAdministrationAgencyProfileByUserID")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetAgencyProfileByUserID(HttpRequestMessage request, int UserID, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserProfileDTO result = BusinessLayer.Facades.Facade.WebFacade().getAdministrationAgencyProfileValidation(UserID, ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<UserProfileDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Countries", Name = "GetWebCountires")]
        [HttpGet]
        public HttpResponseMessage GetCountries(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<CountryModel> result = BusinessLayer.Facades.Facade.WebFacade().getCountries();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<CountryModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/AddressTypes", Name = "GetWebAddressTypes")]
        [HttpGet]
        public HttpResponseMessage GetAddressTypes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<AddressTypeModel> result = BusinessLayer.Facades.Facade.WebFacade().getAddressTypes();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<AddressTypeModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Tags", Name = "GetWebTags")]
        [HttpGet]
        public HttpResponseMessage GetTags(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<TagModel> result = BusinessLayer.Facades.Facade.WebFacade().getTags();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<TagModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Tags/Primary", Name = "GetWebTagsPrimary")]
        [HttpGet]
        public HttpResponseMessage GetTagsPrimary(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<TagModel> result = BusinessLayer.Facades.Facade.WebFacade().getTagsPrimary();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<TagModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Industries", Name = "GetWebIndustries")]
        [HttpGet]
        public HttpResponseMessage GetIndustries(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<IndustryModel> result = BusinessLayer.Facades.Facade.WebFacade().getIndustries();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<IndustryModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/{ProfileID}", Name = "GetWebProfileDetail")]
        [HttpGet]
        public HttpResponseMessage GetProfileDetail(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getProfileDetail(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<ProfileListModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/About/Page/{ProfileURL}/{RequestedProfileID}/{pagesize}/{pagenumber}", Name = "GetWebAboutPage")]
        [HttpGet]
        public HttpResponseMessage GetAboutPage(HttpRequestMessage request, string ProfileURL, int RequestedProfileID, int pagesize, int pagenumber)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getAboutPage(ProfileURL, RequestedProfileID, pagenumber, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<AboutPageModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Latest/Contents/{ProfileID}/{pagesize}/{pagenumber}", Name = "GetLatestContentsByProfileID")]
        [HttpGet]
        public HttpResponseMessage GetLatestContents(HttpRequestMessage request, int ProfileID, int pagesize, int pagenumber)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getLatestContents(ProfileID, pagenumber, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ContentItemModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profiles/", Name = "GetAllProfilesAvailable")]
        [HttpGet]
        public HttpResponseMessage GetAllProfiles(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getAllProfiles();
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

        [Route("Web/Gallery/Page/{ProfileURL}/{CategoryID}/{OrderType}/{pagesize}/{pagenumber}", Name = "GetWebGalleryPage")]
        [HttpGet]
        public HttpResponseMessage GetGalleryPage(HttpRequestMessage request, string ProfileURL, int pagesize, int pagenumber, int CategoryID, int OrderType)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                GalleryPageModel result = new GalleryPageModel();
                result = BusinessLayer.Facades.Facade.WebFacade().getGalleryPage(ProfileURL, pagenumber, pagesize, CategoryID, OrderType);

                //if (CategoryID != 0)
                //{
                //} else
                //{
                //    result = BusinessLayer.Facades.Facade.WebFacade().getGalleryPage(ProfileURL, pagenumber, pagesize, null, OrderType);
                //}
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<GalleryPageModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Address/Page/{ProfileURL}/{AddressTypeID}", Name = "GetWebAddressPage")]
        [HttpGet]
        public HttpResponseMessage GetAddressPage(HttpRequestMessage request, string ProfileURL, int AddressTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AddressPageModel result = new AddressPageModel();
                if (AddressTypeID != 0)
                {
                    result = BusinessLayer.Facades.Facade.WebFacade().getAddressPage(ProfileURL, AddressTypeID);
                }
                else
                {
                    result = BusinessLayer.Facades.Facade.WebFacade().getAddressPage(ProfileURL, null);
                }
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<AddressPageModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/Low/{ProfileURL}/{RequestedProfileID}", Name = "GetMyWebProfileLow")]
        [HttpGet]
        public HttpResponseMessage GetLowProfile(HttpRequestMessage request, string ProfileURL, int RequestedProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getLowProfile(ProfileURL, RequestedProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<LowProfileModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/Low/App/{ProfileURL}", Name = "GetMyWebAppProfileLow")]
        [HttpGet]
        public HttpResponseMessage GetLowAppProfile(HttpRequestMessage request, string ProfileURL)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getLowAppProfile(ProfileURL);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<LowAppProfileModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/URL/{ProfileURL}/{RequestedProfileID}", Name = "GetWebProfileDetailByURL")]
        [HttpGet]
        public HttpResponseMessage GetProfileDetailByURL(HttpRequestMessage request, string ProfileURL, int RequestedProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getProfileDetailByURL(ProfileURL, RequestedProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<ProfileListModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/Web/URL/{ProfileURL}", Name = "GetWebProfileDetailByURLWeb")]
        [HttpGet]
        public HttpResponseMessage GetProfileDetailByURLWeb(HttpRequestMessage request, string ProfileURL)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getProfileDetailByURLWeb(ProfileURL);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<ProfileListModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/About/{ProfileID}", Name = "GetWebProfileAbout")]
        [HttpGet]
        public HttpResponseMessage GetProfileAbout(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getProfileAboutUs(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<AboutModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/Addresses/{ProfileURL}/{AddressTypeID}", Name = "GetWebProfileAdresses")]
        [HttpGet]
        public HttpResponseMessage GetProfileAddress(HttpRequestMessage request, string ProfileURL, int AddressTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<AddressList> result = null;
                result = BusinessLayer.Facades.Facade.WebFacade().getProfileAddresses(ProfileURL, AddressTypeID);
                //var result = BusinessLayer.Facades.Facade.WebFacade().getProfileAddresses(ProfileID, AddressTypeID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<List<AddressList>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/AlbumSet/{ProfileID}/{ImageRows}/{PageNumber}/{PageSize}", Name = "GetWebProfileAlbumSets")]
        [HttpGet]
        public HttpResponseMessage GetProfileAlbumSet(HttpRequestMessage request, int ProfileID, int ImageRows, int pagenumber, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //var result = BusinessLayer.Facades.Facade.WebFacade().getProfileAlbumImageSets(ProfileID, ImageRows, pagenumber, pagesize);
                var result = BusinessLayer.Facades.Facade.ContentFacade().getAlbumContentSet(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<object>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/AlbumSet/{ProfileID}", Name = "GetWebProfileFirstAlbumSets")]
        [HttpGet]
        public HttpResponseMessage GetProfileFirstAlbumSet(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.ContentFacade().getAlbumContentSet(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<object>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/Albums/{ProfileID}/{OrderType}/{CategoryID}/{PageNumber}/{PageSize}", Name = "GetWebProfileAlbums")]
        [HttpGet]
        public HttpResponseMessage GetProfileAlbums(HttpRequestMessage request, int ProfileID, int OrderType, int CategoryID, int pagenumber, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getProfileAlbums(ProfileID, OrderType, CategoryID, pagenumber, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<AlbumModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/Albums/{ProfileID}/{PageNumber}/{PageSize}", Name = "GetWebProfileAllAlbums")]
        [HttpGet]
        public HttpResponseMessage GetProfileAlbums(HttpRequestMessage request, int ProfileID, int pagenumber, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getProfileAlbums(ProfileID, pagenumber, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<AlbumModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/Album/{AlbumID}", Name = "GetWebProfileAlbum")]
        [HttpGet]
        public HttpResponseMessage GetAlbum(HttpRequestMessage request, int AlbumID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getAlbum(AlbumID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ContentModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/Slider/{ProfileID}", Name = "GetWebProfileSlider")]
        [HttpGet]
        public HttpResponseMessage GetProfileSlider(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getProfileDetailSlider(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<ProfileSliderModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        //[ClaimsAuthorization(ClaimType = ClaimTypes.Role, ClaimValue = "user")]
        [Route("Web/Profiles/Fresh/", Name = "GetWebFreshProfiles")]
        [HttpPost]
        public HttpResponseMessage GetFreshProfiles(HttpRequestMessage request, ProfileSearch search)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FreshProfiles result;
                if (search.ProfileID != null)
                {
                    result = BusinessLayer.Facades.Facade.WebFacade().getMainLoadProfiles(search.OrderType, search.CountryID, search.Industries, search.ProfileTypeID, search.Tags, search.Location, search.ProfileID);
                }
                else
                {
                    result = BusinessLayer.Facades.Facade.WebFacade().getProfileEntities(search.OrderType, search.CountryID, search.Industries, search.ProfileTypeID, search.Tags, search.Location);
                }

                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<FreshProfiles>(HttpStatusCode.OK, result);
                }
                //if (search.SubscriptionTypeID == null) {

                //} else
                //{
                //    IEnumerable<ProfileItemModel> result;
                //    if (search.ProfileID != null)
                //    {
                //        //result = BusinessLayer.Facades.Facade.WebFacade().getMainLoadProfiles(search.OrderType, search.CountryID, search.Industries, search.ProfileTypeID, search.Tags, search.Location, search.ProfileID);
                //        result = new List<ProfileItemModel>();
                //    }
                //    else
                //    {
                //        result = BusinessLayer.Facades.Facade.WebFacade().getProfiles(search.SubscriptionTypeID, search.PageNumber, search.PageSize, search.OrderType, search.CountryID, search.Industries, search.ProfileTypeID, search.Tags, search.Location);
                //    }
                //}

                return response;
            });
        }

        [Route("Web/Profiles/", Name = "GetWebProfiles")]
        [HttpPost]
        public HttpResponseMessage GetProfiles(HttpRequestMessage request, ProfileSearch search)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getProfiles(search.SubscriptionTypeID, search.PageNumber, search.PageSize, search.OrderType, search.CountryID, search.Industries, search.ProfileTypeID, search.Tags, search.Location);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ProfileItemModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Admin/Mini/Profile/{ProfileID}", Name = "GetWebAdminMiniProfile")]
        [HttpGet]
        public HttpResponseMessage GetMiniProfile(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getMiniProfile(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<AdminMiniProfile>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/Contact", Name = "SendWebProfileContact")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage SendContact(HttpRequestMessage request, SendContact contact)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().SendContact(contact.ProfileID, contact.RequestedProfileID, contact.ContactSubject, contact.ContactNote);
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

        [Route("Web/Profile/Main/{SubscriptionTypeID}/{PageNumber}/{PageSize}", Name = "GetWebMainProfiles")]
        [HttpGet]
        public HttpResponseMessage GetMainProfiles(HttpRequestMessage request, int SubscriptionTypeID, int pagenumber, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getProfiles(SubscriptionTypeID, pagenumber, pagesize, 1, null, null, null, null, null);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ProfileItemModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/My/{ProfileID}/{PageNumber}/{PageSize}", Name = "GetWebMyProfiles")]
        [HttpGet]
        public HttpResponseMessage GetMyProfiles(HttpRequestMessage request, int ProfileID, int pagenumber, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getMyProfiles(ProfileID, true, 1, pagenumber, pagesize, 1, null, null, null, null, null);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ProfileItemModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/My/Friends/{ProfileID}/{PageNumber}/{PageSize}", Name = "GetWebMyFriendsProfiles")]
        [HttpGet]
        public HttpResponseMessage GetMyFriendsProfiles(HttpRequestMessage request, int ProfileID, int pagenumber, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getMyFriendsProfiles(ProfileID, true, null, pagenumber, pagesize, 1, null, null, null, null, null);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ProfileItemModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/Albums/Latest/{ProfileID}/{PageNumber}/{PageSize}", Name = "GetWebProfileLatestAlbums")]
        [HttpGet]
        public HttpResponseMessage GetProfileLatestAlbums(HttpRequestMessage request, int ProfileID, int pagenumber, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getProfileLatestAlbums(ProfileID, pagenumber, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<AlbumModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Profile/Connections/{ProfileID}/{ProfileIsPeople}/{ProfileStatus}/{RequestTypeID}/{PageNumber}/{PageSize}", Name = "GetWebProfileConnections")]
        [HttpGet]
        public HttpResponseMessage GetProfileConnection(HttpRequestMessage request, int ProfileID, int ProfileIsPeople, int ProfileStatus, int RequestTypeID, int pagenumber, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getProfileConnections(ProfileID, ProfileIsPeople, ProfileStatus, RequestTypeID, pagenumber, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<ConnectionModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Features", Name = "GetWebFeatures")]
        [HttpGet]
        public HttpResponseMessage GetFeatures(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getFeatures();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<FeatureListModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/Feature/{FeatureID}", Name = "GetWebFeatureByFeatureID")]
        [HttpGet]
        public HttpResponseMessage GetFeatureByID(HttpRequestMessage request, int FeatureID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getFeatureByID(FeatureID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<FeatureDetailModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }
    }
}
