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
    public class ThemeController : ApiControllerBase
    {
        [Route("Theme/ThemeID/{ThemeID}", Name = "GetThemeByID")]
        [HttpGet]
        public HttpResponseMessage GetTheme(HttpRequestMessage request, int ThemeID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ThemeDTO result = BusinessLayer.Facades.Facade.ThemeFacade().GetTheme(ThemeID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<ThemeDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Themes/data", Name = "GetThemesDataTables")]
        [HttpPost]
        public DTResult<ThemeDTO> GetThemes(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<ThemeDTO> result = BusinessLayer.Facades.Facade.ThemeFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Themes", Name = "GetThemesAll")]
        [HttpGet]
        public HttpResponseMessage GetThemes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ThemeDTO> result = BusinessLayer.Facades.Facade.ThemeFacade().GetThemes();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<ThemeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Themes/{page}/{pagesize}", Name = "GetThemes")]
        [HttpGet]
        public HttpResponseMessage GetThemes(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<ThemeDTO> result = BusinessLayer.Facades.Facade.ThemeFacade().GetThemes(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<ThemeDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("Theme", Name = "ThemeInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ThemeDTO objTheme)
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
                    ThemeDTO result = BusinessLayer.Facades.Facade.ThemeFacade().Insert(objTheme);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ThemeDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Theme/ThemeID/{ThemeID}", Name = "DeleteTheme")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int ThemeID)
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
                    bool result = BusinessLayer.Facades.Facade.ThemeFacade().Delete(ThemeID);
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


        [Route("Theme/ThemeID/{ThemeID}", Name = "UpdateTheme")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int ThemeID, ThemeDTO objTheme)
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
                    bool result = BusinessLayer.Facades.Facade.ThemeFacade().Update(ThemeID,objTheme);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.ThemeDTO>(HttpStatusCode.OK, objTheme);
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