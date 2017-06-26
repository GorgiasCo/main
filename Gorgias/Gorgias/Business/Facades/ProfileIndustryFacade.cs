using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Gorgias.DataLayer.Repository;
using Gorgias.Business.DataTransferObjects;
using Gorgias.Infrastruture.Core;
using Gorgias.Business.DataTransferObjects.Helper;
using EntityFramework.Extensions;

namespace Gorgias.BusinessLayer.Facades
{   
    public class ProfileIndustryFacade
    {                
        public List<IndustryDTO> GetProfileIndustry(int ProfileID)
        {
            List<IndustryDTO> result = Mapper.Map<List<IndustryDTO>>(DataLayer.DataLayerFacade.ProfileIndustryRepository().GetProfileIndustriesByProfileID(ProfileID).Industries);             
            return result;
        }       

        public bool Insert(ProfileIndustryDTO objProfileIndustry)
        {
            bool result = DataLayer.DataLayerFacade.ProfileIndustryRepository().Insert(objProfileIndustry.IndustryID, objProfileIndustry.ProfileID);
            return result;
        }
               
        public bool Delete(int IndustryID, int ProfileID)
        {            
            bool result = DataLayer.DataLayerFacade.ProfileIndustryRepository().Delete(IndustryID, ProfileID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }      
    }
}