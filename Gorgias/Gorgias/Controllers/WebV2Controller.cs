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

namespace Gorgias.Controllers
{
    [RoutePrefix("api")]
    public class WebV2Controller : ApiControllerBase
    {
        [Route("Web/V2/Address/{ProfileID}/{AddressTypeID}", Name = "GetWebV2AddressPage")]
        [HttpGet]
        public HttpResponseMessage GetAddressPage(HttpRequestMessage request, int ProfileID, int AddressTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AddressPageModelV2 result = new AddressPageModelV2();
                if (AddressTypeID != 0)
                {
                    result = BusinessLayer.Facades.Facade.WebFacade().getAddressesByProfileID(ProfileID, AddressTypeID);
                }
                else
                {
                    result = BusinessLayer.Facades.Facade.WebFacade().getAddressesByProfileID(ProfileID, null);
                }
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<AddressPageModelV2>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/V2/Store/Profile/{ProfileID}", Name = "GetWebV2StoreProfile")]
        [HttpGet]
        public HttpResponseMessage GetStoreProfile(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Business.DataTransferObjects.Web.V2.StoreProfileModel result = BusinessLayer.Facades.Facade.WebFacade().getStoreProfile(ProfileID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<Business.DataTransferObjects.Web.V2.StoreProfileModel>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Web/V2/MainEntities", Name = "GetWebV2MainEntities")]
        [HttpGet]
        public HttpResponseMessage GetMainEntities(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                MainEntities result = BusinessLayer.Facades.Facade.WebFacade().getMainV2Entities();
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

        [Route("Web/V2/ProfileTypes", Name = "GetWebV2ProfileTypes")]
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

        [Route("Web/V2/Countries", Name = "GetWebV2Countires")]
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

        [Route("Web/V2/Industries", Name = "GetWebV2Industries")]
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

        [Route("Web/V2/Profile/{ProfileID}", Name = "GetWebV2ProfileDetail")]
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

        [Route("Web/V2/Stories/Latest/{pagesize}/{pagenumber}", Name = "GetLatestStories")]
        [HttpGet]
        public HttpResponseMessage GetLatestStories(HttpRequestMessage request, int pagesize, int pagenumber)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.AlbumFacade().getLatestAlbums(pagesize, pagenumber);
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

        [Route("Web/V2/Profiles/", Name = "GetWebV2Profiles")]
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

        [Route("Web/V2/Profiles/Brand/", Name = "GetWebV2ProfilesBrands")]
        [HttpPost]
        public HttpResponseMessage GetBrandProfiles(HttpRequestMessage request, ProfileSearch search)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var result = BusinessLayer.Facades.Facade.WebFacade().getProfiles(search.SubscriptionTypeID, search.PageNumber, search.PageSize, search.OrderType, search.CountryID, search.Industries, search.ProfileTypeID, search.Tags, search.Location, search.IsPeople.Value);
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

        [Route("Web/V2/Features", Name = "GetWebV2Features")]
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

        [Route("Web/V2/Feature/{FeatureID}", Name = "GetWebV2FeatureByFeatureID")]
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
