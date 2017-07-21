using Gorgias.Business.DataTransferObjects.Mobile;
using Gorgias.Business.DataTransferObjects.Web;
using Gorgias.Business.DataTransferObjects.Web.List;
using Gorgias.Infrastruture.Core;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gorgias.Controllers
{
    [RoutePrefix("api")]
    public class MobileController : ApiControllerBase
    {
        [Route("Mobile/Comments/Content/{ContentID}/{pagesize}/{page}", Name = "GetMobileCommentsByContent")]
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

        [Route("Mobile/Subscriber/{Mode}/{ProfileID}/{UserProfileID}", Name = "SubscriberMobile")]
        [HttpGet]
        public HttpResponseMessage Subscriber(HttpRequestMessage request, int Mode, int ProfileID, int UserProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool result = BusinessLayer.Facades.Facade.ConnectionFacade().InsertForMobile(new Business.DataTransferObjects.ConnectionDTO { ProfileID = ProfileID, RequestedProfileID = UserProfileID, RequestTypeID = Mode});

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

        [Route("Mobile/Profile/Addresses/{ProfileID}", Name = "GetMobileProfileAddresses")]
        [HttpGet]
        public HttpResponseMessage GetProfileAddresses(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<Business.DataTransferObjects.Mobile.AddressModelV2> result = BusinessLayer.Facades.Facade.WebFacade().getAddressMobileV2(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<Business.DataTransferObjects.Mobile.AddressModelV2>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/Profile/NewUser", Name = "CreateNewMobileUser")]
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

        [Route("Mobile/Album/Availability/{ProfileID}", Name = "GetMobileAlbumAvailability")]
        [HttpGet]
        public HttpResponseMessage GetAlbumAvailability(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<AlbumAvailabilityModel> result = BusinessLayer.Facades.Facade.WebFacade().AlbumAvailability(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<AlbumAvailabilityModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/Categories", Name = "GetMobileCategories")]
        [HttpGet]
        public HttpResponseMessage GetCategories(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
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

        [Route("Mobile/Categories/{ProfileID}", Name = "GetMobileCategoriesByProfileID")]
        [HttpGet]
        public HttpResponseMessage GetCategoriesByProfileID(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<CategoryMobileModel> result = BusinessLayer.Facades.Facade.WebFacade().getCategories(ProfileID);
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

        [Route("Mobile/Album/Comment/Status/{AlbumID}", Name = "UpdateAlbumCommentStatus")]
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

        [Route("Mobile/Profile/About/{ProfileID}", Name = "GetProfileAboutMobile")]
        [HttpGet]
        public HttpResponseMessage GetProfileAbout(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getAbout(ProfileID);
                response = request.CreateResponse<IEnumerable<AboutModel>>(HttpStatusCode.OK, result);
                return response;
            });
        }

        [Route("Mobile/Content/Like/{ContentID}", Name = "UpdateContentLike")]
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

        [Route("Mobile/Contents/Likes", Name = "UpdateContentsLikes")]
        [HttpPost]
        public HttpResponseMessage UpdateContnetsLikes(HttpRequestMessage request, ContentLikesModel[] ContentLikes)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().updateContentLike(ContentLikes);
                response = request.CreateResponse<bool>(HttpStatusCode.OK, result);
                return response;
            });
        }

        [Route("Mobile/Album/View/{AlbumID}", Name = "UpdateAlbumView")]
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

        [Route("Mobile/Hottest/{AlbumID}", Name = "GetMobileAlbumHottestByAlbumID")]
        [HttpGet]
        public HttpResponseMessage GetHottestAlbum(HttpRequestMessage request, int AlbumID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getHottestAlbum(AlbumID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<Business.DataTransferObjects.Mobile.AlbumMobileModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/Latest/Moments/{ProfileID}/{contentsize}/{pagesize}/{pagenumber}", Name = "GetMobileLatestMomentsByProfileID")]
        [HttpGet]
        public HttpResponseMessage GetLatestMoments(HttpRequestMessage request, int ProfileID, int pagesize, int pagenumber, int contentsize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getLatestMomentAlbums(ProfileID, pagenumber, pagesize, contentsize);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<Business.DataTransferObjects.Mobile.AlbumMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/Latest/Moments/Admin/{ProfileID}/{contentsize}/{pagesize}/{pagenumber}", Name = "GetMobileAdminLatestMomentsByProfileID")]
        [HttpGet]
        public HttpResponseMessage GetAdminLatestMoments(HttpRequestMessage request, int ProfileID, int pagesize, int pagenumber, int contentsize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getLatestMomentAdminAlbums(ProfileID, pagenumber, pagesize, contentsize);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<Business.DataTransferObjects.Mobile.AlbumMobileAdminModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/Latest/Moments/Admin/Async/{ProfileID}/{contentsize}/{pagesize}/{pagenumber}", Name = "GetMobileAdminAsyncLatestMomentsByProfileID")]
        [HttpGet]
        public async System.Threading.Tasks.Task<HttpResponseMessage> GetAdminAsyncLatestMoments(HttpRequestMessage request, int ProfileID, int pagesize, int pagenumber, int contentsize)
        {
            HttpResponseMessage response = null;
            var result = await BusinessLayer.Facades.Facade.WebFacade().getLatestMomentAdminAlbumsAsync(ProfileID, pagenumber, pagesize, contentsize);
            if (result == null)
            {
                response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.OK, result);
            }
            return response;
        }

        [Route("Mobile/Latest/Hottest/{ProfileID}/{CategoryID}/{pagesize}/{pagenumber}", Name = "GetMobileLatestHottestByProfileID")]
        [HttpGet]
        public HttpResponseMessage GetLatestHottest(HttpRequestMessage request, int ProfileID, int CategoryID, int pagesize, int pagenumber)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getLatestHottestAlbums(ProfileID, CategoryID, pagenumber, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<Business.DataTransferObjects.Mobile.AlbumMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/Latest/Gallery/{ProfileID}/{CategoryID}/{pagesize}/{pagenumber}", Name = "GetMobileLatestGalleryByProfileIDandCategoryID")]
        [HttpGet]
        public HttpResponseMessage GetLatestGallery(HttpRequestMessage request, int ProfileID, int CategoryID, int pagesize, int pagenumber)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getLatestGalleryAlbums(ProfileID, CategoryID, pagenumber, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<Business.DataTransferObjects.Mobile.AlbumMobileModel>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/Latest/Contents/{ProfileID}/{pagesize}/{pagenumber}", Name = "GetMobileLatestContentsByProfileID")]
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

        [Route("Mobile/Profiles/", Name = "GetMobileAllProfilesAvailable")]
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

        [Route("Mobile/Profiles/MyConnections/{ProfileID}", Name = "GetMobileAllMyConnectionProfilesAvailable")]
        [HttpGet]
        public HttpResponseMessage GetAllMyConnectionProfiles(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getAllMyConnectedProfiles(ProfileID);
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

        [Route("Mobile/Profile/Slider/{ProfileID}", Name = "GetMobileWebProfileSlider")]
        [HttpGet]
        public HttpResponseMessage GetProfileSlider(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getMobileProfileDetailSlider(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<Business.DataTransferObjects.Mobile.ProfileSliderModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Mobile/Album/AlbumID/{AlbumID}", Name = "DeleteMobileAlbum")]
        [HttpGet]
        public HttpResponseMessage DeleteAlbum(HttpRequestMessage request, int AlbumID)
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
                    bool result = BusinessLayer.Facades.Facade.AlbumFacade().Delete(AlbumID);
                    if (result)
                    {
                        response = request.CreateResponse<string>(HttpStatusCode.OK, "Done");
                    }
                    else
                    {
                        response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                    }
                }
                return response;
            });
        }

        [Route("Mobile/Content/ContentID/{ContentID}", Name = "DeleteMobileContent")]
        [HttpGet]
        public HttpResponseMessage DeleteContent(HttpRequestMessage request, int ContentID)
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
                    bool result = BusinessLayer.Facades.Facade.ContentFacade().Delete(ContentID);
                    if (result)
                    {
                        response = request.CreateResponse<string>(HttpStatusCode.OK, "Done");
                    }
                    else
                    {
                        response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                    }
                }
                return response;
            });
        }

        [Route("Mobile/Content", Name = "MobileContentInsert")]
        [HttpPost]
        public HttpResponseMessage PostContent(HttpRequestMessage request, Business.DataTransferObjects.ContentDTO objContent)
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
                    Business.DataTransferObjects.ContentDTO result = BusinessLayer.Facades.Facade.ContentFacade().Insert(objContent);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ContentDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                    }
                }
                return response;
            });
        }

        [Route("Mobile/Contents/{AlbumID}", Name = "MobileContentsInsert")]
        [HttpPost]
        public HttpResponseMessage PostContents(HttpRequestMessage request, List<ContentUploadMobileModel> objContents, int AlbumID)
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
                    bool result = BusinessLayer.Facades.Facade.ContentFacade().Insert(objContents, AlbumID);
                    if (result)
                    {
                        response = request.CreateResponse<bool>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                    }
                }
                return response;
            });
        }

        [Route("Mobile/Album/{Availability}/{CategoryID}/{ProfileID}", Name = "MobileAlbumInsert")]
        [HttpPost]
        public HttpResponseMessage PostAlbum(HttpRequestMessage request, List<ContentUploadMobileModel> objContents, int Availability, int CategoryID, int ProfileID)
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
                    Business.DataTransferObjects.AlbumDTO result = BusinessLayer.Facades.Facade.ContentFacade().Insert(objContents, Availability, CategoryID, ProfileID);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AlbumDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                    }
                }
                return response;
            });
        }

        [Route("Mobile/Album/{Availability}/{CategoryID}/{ProfileID}/{AlbumHasComment}", Name = "MobileAlbumInsertWithComment")]
        [HttpPost]
        public HttpResponseMessage PostAlbum(HttpRequestMessage request, List<ContentUploadMobileModel> objContents, int Availability, int CategoryID, int ProfileID, int AlbumHasComment)
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
                    Business.DataTransferObjects.AlbumDTO result;
                    if(AlbumHasComment == 1) {
                        result = BusinessLayer.Facades.Facade.ContentFacade().Insert(objContents, Availability, CategoryID, ProfileID, true);
                    }
                    else
                    {
                        result = BusinessLayer.Facades.Facade.ContentFacade().Insert(objContents, Availability, CategoryID, ProfileID, false);
                    }
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AlbumDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                    }
                }
                return response;
            });
        }

        [Route("Mobile/Comment", Name = "CommentMobileInsert")]
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

    }
}
