using Gorgias.Business.DataTransferObjects.Report;
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
    public class ReportController : ApiControllerBase
    {

        [Route("Reports/Profiles/Current", Name = "GetProfilesCurrentReport")]
        [HttpGet]
        public HttpResponseMessage GetProfilesCurrentReport(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<Business.DataTransferObjects.Report.ProfileReport> result = BusinessLayer.Facades.Facade.ReportFacade().getCurrentProfileReport();
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<Business.DataTransferObjects.Report.ProfileReport>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Reports/Profiles/Current/{paramID}/{isCountry}", Name = "GetProfilesCurrentReportByUserID")]
        [HttpGet]
        public HttpResponseMessage GetProfilesCurrentReport(HttpRequestMessage request, int paramID, int isCountry)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FullProfileReport result;

                if (isCountry == 0)
                {
                    result = BusinessLayer.Facades.Facade.ReportFacade().getCurrentProfileReport(paramID, true);
                }
                else
                {
                    result = BusinessLayer.Facades.Facade.ReportFacade().getCurrentProfileReport(paramID, false);
                }
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<FullProfileReport>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Reports/Profiles/Current/Country/{CountryID}", Name = "GetProfilesCurrentReportByCountryID")]
        [HttpGet]
        public HttpResponseMessage GetProfilesCurrentReportByCountry(HttpRequestMessage request, int CountryID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IList<Business.DataTransferObjects.Report.ProfileReport> result = BusinessLayer.Facades.Facade.ReportFacade().getCurrentProfileReportByCountry(CountryID);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IList<Business.DataTransferObjects.Report.ProfileReport>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Reports/AlbumLikes/{UserID}/{Availability}/{OrderType}", Name = "GetAlbumLikes")]
        [HttpGet]
        public HttpResponseMessage GetAlbumLikes(HttpRequestMessage request, int UserID, int Availability, int OrderType)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<AlbumLike> result = BusinessLayer.Facades.Facade.ReportFacade().AlbumLikes(UserID,Availability,OrderType);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<AlbumLike>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Reports/AlbumResult/{UserID}/{Availability}/{OrderType}", Name = "GetAlbumResults")]
        [HttpGet]
        public HttpResponseMessage GetAlbumResults(HttpRequestMessage request, int UserID, int Availability, int OrderType)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<AlbumResult> result = BusinessLayer.Facades.Facade.ReportFacade().AlbumResults(UserID, Availability, OrderType);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<AlbumResult>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Reports/AlbumMomentInformation/{UserID}/{OrderType}/{Availability}", Name = "GetAlbumMomentsInformation")]
        [HttpGet]
        public HttpResponseMessage GetAlbumMomentsInformation(HttpRequestMessage request, int UserID, int OrderType, int Availability)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<AlbumInformation> result = BusinessLayer.Facades.Facade.ReportFacade().AlbumMomentsInformation(UserID, Availability, OrderType);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<AlbumInformation>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Reports/AlbumInformation/{UserID}/{OrderType}/{Availability}", Name = "GetAlbumInformation")]
        [HttpGet]
        public HttpResponseMessage GetAlbumInformation(HttpRequestMessage request, int UserID, int OrderType, int Availability)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<AlbumInformation> result = BusinessLayer.Facades.Facade.ReportFacade().AlbumInformation(UserID, Availability, OrderType);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<IEnumerable<AlbumInformation>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

    }
}
