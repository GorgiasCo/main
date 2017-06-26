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
    public class FeaturedSponsorController : ApiControllerBase
    {
        [Route("FeaturedSponsor/FeatureID/ProfileID/{FeatureID}/{ProfileID}", Name = "GetFeaturedSponsorByID")]
        [HttpGet]
        public HttpResponseMessage GetFeaturedSponsor(HttpRequestMessage request, int FeatureID, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FeaturedSponsorDTO result = BusinessLayer.Facades.Facade.FeaturedSponsorFacade().GetFeaturedSponsor(FeatureID, ProfileID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<FeaturedSponsorDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("FeaturedSponsors/data", Name = "GetFeaturedSponsorsDataTables")]
        [HttpPost]
        public DTResult<FeaturedSponsorDTO> GetFeaturedSponsors(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<FeaturedSponsorDTO> result = BusinessLayer.Facades.Facade.FeaturedSponsorFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("FeaturedSponsors", Name = "GetFeaturedSponsorsAll")]
        [HttpGet]
        public HttpResponseMessage GetFeaturedSponsors(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FeaturedSponsorDTO> result = BusinessLayer.Facades.Facade.FeaturedSponsorFacade().GetFeaturedSponsors();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<FeaturedSponsorDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("FeaturedSponsors/{page}/{pagesize}", Name = "GetFeaturedSponsors")]
        [HttpGet]
        public HttpResponseMessage GetFeaturedSponsors(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<FeaturedSponsorDTO> result = BusinessLayer.Facades.Facade.FeaturedSponsorFacade().GetFeaturedSponsors(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<FeaturedSponsorDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("FeaturedSponsors/ProfileID/{ProfileID}/data", Name = "GetFeaturedSponsorsDataTablesByProfileID}")]
        [HttpPost]
        public DTResult<FeaturedSponsorDTO> GetFeaturedSponsorsByProfileID(HttpRequestMessage request, int ProfileID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<FeaturedSponsorDTO> result = BusinessLayer.Facades.Facade.FeaturedSponsorFacade().FilterResultByProfileID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileID);
            return result;
        }
            
        [Route("FeaturedSponsors/ProfileID/{ProfileID}/{page}/{pagesize}", Name = "GetFeaturedSponsorsByProfileID")]
        [HttpGet]
        public HttpResponseMessage GetFeaturedSponsorsByProfileID(HttpRequestMessage request, int ProfileID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<FeaturedSponsorDTO> result = BusinessLayer.Facades.Facade.FeaturedSponsorFacade().GetFeaturedSponsorsByProfileID(ProfileID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<FeaturedSponsorDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("FeaturedSponsor", Name = "FeaturedSponsorInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.FeaturedSponsorDTO objFeaturedSponsor)
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
                    FeaturedSponsorDTO result = BusinessLayer.Facades.Facade.FeaturedSponsorFacade().Insert(objFeaturedSponsor);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.FeaturedSponsorDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("FeaturedSponsor/FeatureID/ProfileID/{FeatureID}/{ProfileID}", Name = "DeleteFeaturedSponsor")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int FeatureID, int ProfileID)
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
                    bool result = BusinessLayer.Facades.Facade.FeaturedSponsorFacade().Delete(FeatureID, ProfileID);
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


        [Route("FeaturedSponsor/FeatureID/ProfileID/{FeatureID}/{ProfileID}", Name = "UpdateFeaturedSponsor")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int FeatureID, int ProfileID, FeaturedSponsorDTO objFeaturedSponsor)
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
                    bool result = BusinessLayer.Facades.Facade.FeaturedSponsorFacade().Update(FeatureID, ProfileID,objFeaturedSponsor);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.FeaturedSponsorDTO>(HttpStatusCode.OK, objFeaturedSponsor);
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