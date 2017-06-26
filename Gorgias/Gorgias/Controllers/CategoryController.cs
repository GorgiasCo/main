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
    public class CategoryController : ApiControllerBase
    {
        [Route("Category/CategoryID/{CategoryID}", Name = "GetCategoryByID")]
        [HttpGet]
        public HttpResponseMessage GetCategory(HttpRequestMessage request, int CategoryID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                CategoryDTO result = BusinessLayer.Facades.Facade.CategoryFacade().GetCategory(CategoryID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<CategoryDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Categories/data", Name = "GetCategoriesDataTables")]
        [HttpPost]
        public DTResult<CategoryDTO> GetCategories(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<CategoryDTO> result = BusinessLayer.Facades.Facade.CategoryFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Categories", Name = "GetCategoriesAll")]
        [HttpGet]
        public HttpResponseMessage GetCategories(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<CategoryDTO> result = BusinessLayer.Facades.Facade.CategoryFacade().GetCategories();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<CategoryDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Categories/{page}/{pagesize}", Name = "GetCategories")]
        [HttpGet]
        public HttpResponseMessage GetCategories(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<CategoryDTO> result = BusinessLayer.Facades.Facade.CategoryFacade().GetCategories(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<CategoryDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("Category", Name = "CategoryInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.CategoryDTO objCategory)
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
                    CategoryDTO result = BusinessLayer.Facades.Facade.CategoryFacade().Insert(objCategory);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.CategoryDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Category/CategoryID/{CategoryID}", Name = "DeleteCategory")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int CategoryID)
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
                    bool result = BusinessLayer.Facades.Facade.CategoryFacade().Delete(CategoryID);
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


        [Route("Category/CategoryID/{CategoryID}", Name = "UpdateCategory")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int CategoryID, CategoryDTO objCategory)
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
                    bool result = BusinessLayer.Facades.Facade.CategoryFacade().Update(CategoryID,objCategory);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.CategoryDTO>(HttpStatusCode.OK, objCategory);
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