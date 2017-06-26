using Gorgias.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gorgias.Infrastruture.Core;
using Gorgias.Business.DataTransferObjects.Helper;

namespace Gorgias.Controllers
{
    [RoutePrefix("api")]
    public class AlbumTypeController : ApiControllerBase
    {
        [Route("AlbumType/AlbumTypeID/{AlbumTypeID}", Name = "GetAlbumTypeByID")]
        [HttpGet]
        public HttpResponseMessage GetAlbumType(HttpRequestMessage request, int AlbumTypeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AlbumTypeDTO result = BusinessLayer.Facades.Facade.AlbumTypeFacade().GetAlbumType(AlbumTypeID);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<AlbumTypeDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("AlbumTypes/data", Name = "GetAlbumTypesDataTables")]
        [HttpPost]
        public DTResult<AlbumTypeDTO> GetAlbumTypes(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<AlbumTypeDTO> result = BusinessLayer.Facades.Facade.AlbumTypeFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("AlbumTypes", Name = "GetAlbumTypesAll")]
        [HttpGet]
        public HttpResponseMessage GetAlbumTypes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<AlbumTypeDTO> result = BusinessLayer.Facades.Facade.AlbumTypeFacade().GetAlbumTypes();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<List<AlbumTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("AlbumTypes/{page}/{pagesize}", Name = "GetAlbumTypes")]
        [HttpGet]
        public HttpResponseMessage GetAlbumTypes(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<AlbumTypeDTO> result = BusinessLayer.Facades.Facade.AlbumTypeFacade().GetAlbumTypes(page, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<AlbumTypeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }




        [Route("AlbumType", Name = "AlbumTypeInsert")]
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.AlbumTypeDTO objAlbumType)
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
                    AlbumTypeDTO result = BusinessLayer.Facades.Facade.AlbumTypeFacade().Insert(objAlbumType);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AlbumTypeDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });
        }


        [Route("AlbumType/AlbumTypeID/{AlbumTypeID}", Name = "DeleteAlbumType")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int AlbumTypeID)
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
                    bool result = BusinessLayer.Facades.Facade.AlbumTypeFacade().Delete(AlbumTypeID);
                    if (result)
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.OK, "Done");
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                    }
                }
                return response;
            });
        }


        [Route("AlbumType/AlbumTypeID/{AlbumTypeID}", Name = "UpdateAlbumType")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int AlbumTypeID, AlbumTypeDTO objAlbumType)
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
                    bool result = BusinessLayer.Facades.Facade.AlbumTypeFacade().Update(AlbumTypeID, objAlbumType);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AlbumTypeDTO>(HttpStatusCode.OK, objAlbumType);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                    }
                }
                return response;
            });
        }
    }
}