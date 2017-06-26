using Gorgias.Business.DataTransferObjects.Web;
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
    public class DBToolController : ApiControllerBase
    {
        [Route("BulkProfile", Name = "BulkInsert")]
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, BulkInsertModel profileList)
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
                    bool result = BusinessLayer.Facades.Facade.DBToolFacade().BulkProfileInsert(profileList.list);
                    if (result)
                    {
                        response = request.CreateResponse<bool>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<bool>(HttpStatusCode.Found, result);
                    }
                }
                return response;
            });
        }
    }
}
