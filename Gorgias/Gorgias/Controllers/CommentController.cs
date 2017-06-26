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
    public class CommentController : ApiControllerBase
    {
        [Route("Comment/CommentID/{CommentID}", Name = "GetCommentByID")]
        [HttpGet]
        public HttpResponseMessage GetComment(HttpRequestMessage request, int CommentID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                CommentDTO result = BusinessLayer.Facades.Facade.CommentFacade().GetComment(CommentID);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<CommentDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Comments/data", Name = "GetCommentsDataTables")]
        [HttpPost]
        public DTResult<CommentDTO> GetComments(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<CommentDTO> result = BusinessLayer.Facades.Facade.CommentFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Comments", Name = "GetCommentsAll")]
        [HttpGet]
        public HttpResponseMessage GetComments(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<CommentDTO> result = BusinessLayer.Facades.Facade.CommentFacade().GetComments();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<List<CommentDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Comments/{page}/{pagesize}", Name = "GetComments")]
        [HttpGet]
        public HttpResponseMessage GetComments(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<CommentDTO> result = BusinessLayer.Facades.Facade.CommentFacade().GetComments(page, pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<CommentDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }



        [Route("Comments/Profile/{ProfileID}/data", Name = "GetCommentsDataTablesByProfileID}")]
        [HttpPost]
        public DTResult<CommentDTO> GetCommentsByProfileID(HttpRequestMessage request, int ProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<CommentDTO> result = BusinessLayer.Facades.Facade.CommentFacade().FilterResultByProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileID);
            return result;
        }

        [Route("Comments/Profile/{ProfileID}/{page}/{pagesize}", Name = "GetCommentsByProfile")]
        [HttpGet]
        public HttpResponseMessage GetCommentsByProfileID(HttpRequestMessage request, int ProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<CommentDTO> result = BusinessLayer.Facades.Facade.CommentFacade().GetCommentsByProfileID(ProfileID, page, pagesize);

                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<CommentDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }
        [Route("Comments/Content/{ContentID}/data", Name = "GetCommentsDataTablesByContentID}")]
        [HttpPost]
        public DTResult<CommentDTO> GetCommentsByContentID(HttpRequestMessage request, int ContentID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<CommentDTO> result = BusinessLayer.Facades.Facade.CommentFacade().FilterResultByContentID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ContentID);
            return result;
        }

        [Route("Comments/Content/{ContentID}/{page}/{pagesize}", Name = "GetCommentsByContent")]
        [HttpGet]
        public HttpResponseMessage GetCommentsByContentID(HttpRequestMessage request, int ContentID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<CommentDTO> result = BusinessLayer.Facades.Facade.CommentFacade().GetCommentsByContentID(ContentID, page, pagesize);

                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                }
                else
                {
                    response = request.CreateResponse<PaginationSet<CommentDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [Route("Comment", Name = "CommentInsert")]
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.CommentDTO objComment)
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
                   CommentDTO result = BusinessLayer.Facades.Facade.CommentFacade().Insert(objComment);
                   if (result != null)
                   {
                       response = request.CreateResponse<Business.DataTransferObjects.CommentDTO>(HttpStatusCode.Created, result);
                   }
                   else
                   {
                       response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                   }
               }
               return response;
           });
        }


        [Route("Comment/CommentID/{CommentID}", Name = "DeleteComment")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int CommentID)
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
                    bool result = BusinessLayer.Facades.Facade.CommentFacade().Delete(CommentID);
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


        [Route("Comment/CommentID/{CommentID}", Name = "UpdateComment")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int CommentID, CommentDTO objComment)
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
                    bool result = BusinessLayer.Facades.Facade.CommentFacade().Update(CommentID, objComment);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.CommentDTO>(HttpStatusCode.OK, objComment);
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