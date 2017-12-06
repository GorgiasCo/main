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
    public class BrandSoController : ApiControllerBase
    {
        [Route("BrandSo/V2/Profiles/{PageSize}/{PageNumber}", Name = "GetV2BrandSoProfiles")]
        [HttpGet]
        public HttpResponseMessage GetProfiles(HttpRequestMessage request, int PageSize, int PageNumber)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<Business.DataTransferObjects.BrandSo.Profile> result = BusinessLayer.Facades.Facade.ProfileFacade().getBrandSoProfiles(PageNumber, PageSize);
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, "Nothing Found");
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<Business.DataTransferObjects.BrandSo.Profile>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }
    }
}