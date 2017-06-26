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
    public class FeatureController : ApiControllerBase
    {
        [Route("Feature/FeatureID/{FeatureID}", Name = "GetFeatureByID")]
        [HttpGet]
        public HttpResponseMessage GetFeature(HttpRequestMessage request, int FeatureID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FeatureDTO result = BusinessLayer.Facades.Facade.FeatureFacade().GetFeature(FeatureID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<FeatureDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Features/data", Name = "GetFeaturesDataTables")]
        [HttpPost]
        public DTResult<FeatureDTO> GetFeatures(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<FeatureDTO> result = BusinessLayer.Facades.Facade.FeatureFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Features", Name = "GetFeaturesAll")]
        [HttpGet]
        public HttpResponseMessage GetFeatures(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FeatureDTO> result = BusinessLayer.Facades.Facade.FeatureFacade().GetFeatures();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<FeatureDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Features/{page}/{pagesize}", Name = "GetFeatures")]
        [HttpGet]
        public HttpResponseMessage GetFeatures(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<FeatureDTO> result = BusinessLayer.Facades.Facade.FeatureFacade().GetFeatures(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<FeatureDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        
        [Route("Feature", Name = "FeatureInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.FeatureDTO objFeature)
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
                    FeatureDTO result = BusinessLayer.Facades.Facade.FeatureFacade().Insert(objFeature);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.FeatureDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Feature/FeatureID/{FeatureID}", Name = "DeleteFeature")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int FeatureID)
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
                    bool result = BusinessLayer.Facades.Facade.FeatureFacade().Delete(FeatureID);
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


        [Route("Feature/FeatureID/{FeatureID}", Name = "UpdateFeature")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int FeatureID, FeatureDTO objFeature)
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
                    bool result = BusinessLayer.Facades.Facade.FeatureFacade().Update(FeatureID,objFeature);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.FeatureDTO>(HttpStatusCode.OK, objFeature);
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