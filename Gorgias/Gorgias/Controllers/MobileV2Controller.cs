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
    }
}
