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
    public class ProfileIndustryController : ApiControllerBase
    {
        [Route("ProfileIndustry/ProfileID/{ProfileID}", Name = "GetProfileIndustryByID")]
        [HttpGet]
        public HttpResponseMessage GetProfileIndustry(HttpRequestMessage request, int ProfileID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<IndustryDTO> result = BusinessLayer.Facades.Facade.ProfileIndustryFacade().GetProfileIndustry(ProfileID); 
                if (result == null)
                {
                    response = request.CreateResponse<string>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<IndustryDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }
       
        
        [Route("ProfileIndustry", Name = "ProfileIndustryInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.ProfileIndustryDTO objProfileIndustry)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileIndustryFacade().Insert(objProfileIndustry);
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
        

        [Route("ProfileIndustry/IndustryID/ProfileID/{IndustryID}/{ProfileID}", Name = "DeleteProfileIndustry")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int IndustryID, int ProfileID)
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
                    bool result = BusinessLayer.Facades.Facade.ProfileIndustryFacade().Delete(IndustryID, ProfileID);
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
    }
}