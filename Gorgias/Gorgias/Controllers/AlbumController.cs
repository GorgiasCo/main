using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Gorgias.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Gorgias.Infrastruture.Core;
using Gorgias.Business.DataTransferObjects.Helper;


namespace Gorgias.Controllers
{   
    [RoutePrefix("api")]
    public class AlbumController : ApiControllerBase
    {
        [Route("Album/AlbumID/{AlbumID}", Name = "GetAlbumByID")]
        [HttpGet]
        public HttpResponseMessage GetAlbum(HttpRequestMessage request, int AlbumID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AlbumDTO result = BusinessLayer.Facades.Facade.AlbumFacade().GetAlbum(AlbumID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<AlbumDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Albums/data", Name = "GetAlbumsDataTables")]
        [HttpPost]
        public DTResult<AlbumDTO> GetAlbums(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<AlbumDTO> result = BusinessLayer.Facades.Facade.AlbumFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Albums", Name = "GetAlbumsAll")]
        [HttpGet]
        public HttpResponseMessage GetAlbums(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<AlbumContentDTO> result = BusinessLayer.Facades.Facade.AlbumFacade().GetAlbumsForContents();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<AlbumContentDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Albums/{page}/{pagesize}", Name = "GetAlbums")]
        [HttpGet]
        public HttpResponseMessage GetAlbums(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<AlbumDTO> result = BusinessLayer.Facades.Facade.AlbumFacade().GetAlbums(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<AlbumDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("Albums/Category/{CategoryID}/data", Name = "GetAlbumsDataTablesByCategoryID}")]
        [HttpPost]
        public DTResult<AlbumDTO> GetAlbumsByCategoryID(HttpRequestMessage request, int CategoryID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<AlbumDTO> result = BusinessLayer.Facades.Facade.AlbumFacade().FilterResultByCategoryID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, CategoryID);
            return result;
        }
            
        [Route("Albums/Category/{CategoryID}/{page}/{pagesize}", Name = "GetAlbumsByCategory")]
        [HttpGet]
        public HttpResponseMessage GetAlbumsByCategoryID(HttpRequestMessage request, int CategoryID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<AlbumDTO> result = BusinessLayer.Facades.Facade.AlbumFacade().GetAlbumsByCategoryID(CategoryID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<AlbumDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        [Route("Albums/Profile/{ProfileID}/data", Name = "GetAlbumsDataTablesByProfileID}")]
        [HttpPost]
        public DTResult<AlbumDTO> GetAlbumsByProfileID(HttpRequestMessage request, int ProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<AlbumDTO> result = BusinessLayer.Facades.Facade.AlbumFacade().FilterResultByProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileID);
            return result;
        }
            
        [Route("Albums/Profile/{ProfileID}/{page}/{pagesize}", Name = "GetAlbumsByProfile")]
        [HttpGet]
        public HttpResponseMessage GetAlbumsByProfileID(HttpRequestMessage request, int ProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<AlbumDTO> result = BusinessLayer.Facades.Facade.AlbumFacade().GetAlbumsByProfileID(ProfileID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<AlbumDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("Album", Name = "AlbumInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.AlbumDTO objAlbum)
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
                    AlbumDTO result = BusinessLayer.Facades.Facade.AlbumFacade().Insert(objAlbum);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AlbumDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }

        [Route("Album/Hottest", Name = "AlbumHottestInsert")]
        [HttpPost]
        public HttpResponseMessage PostHottest(HttpRequestMessage request, Business.DataTransferObjects.AlbumDTO objAlbum)
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
                    AlbumDTO result = BusinessLayer.Facades.Facade.AlbumFacade().InsertHottest(objAlbum);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AlbumDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });
        }


        [Route("Album/AlbumID/{AlbumID}", Name = "DeleteAlbum")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int AlbumID)
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


        [Route("Album/AlbumID/{AlbumID}", Name = "UpdateAlbum")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int AlbumID, AlbumDTO objAlbum)
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
                    bool result = BusinessLayer.Facades.Facade.AlbumFacade().Update(AlbumID,objAlbum);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AlbumDTO>(HttpStatusCode.OK, objAlbum);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                    }                             
                }
                return response;
            });                        
        }

        [Route("Album/Hottest/AlbumID/{AlbumID}", Name = "UpdateHottestAlbum")]
        [HttpPost]
        public HttpResponseMessage UpdateHottest(HttpRequestMessage request, int AlbumID, AlbumDTO objAlbum)
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
                    bool result = BusinessLayer.Facades.Facade.AlbumFacade().UpdateHottest(AlbumID, objAlbum);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.AlbumDTO>(HttpStatusCode.OK, objAlbum);
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