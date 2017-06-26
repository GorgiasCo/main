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
    public class FeaturedSponsorFacade
    {                
        public FeaturedSponsorDTO GetFeaturedSponsor(int FeatureID, int ProfileID)
        {
            FeaturedSponsorDTO result = Mapper.Map<FeaturedSponsorDTO>(DataLayer.DataLayerFacade.FeaturedSponsorRepository().GetFeaturedSponsor(FeatureID, ProfileID));             
            return result;
        }

        public DTResult<FeaturedSponsorDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.FeaturedSponsorRepository().GetFeaturedSponsorsAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<FeaturedSponsor>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<FeaturedSponsorDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<FeaturedSponsorDTO> result = new DTResult<FeaturedSponsorDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<FeaturedSponsorDTO> GetFeaturedSponsors(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.FeaturedSponsorRepository().GetFeaturedSponsorsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<FeaturedSponsor>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<FeaturedSponsorDTO> result = new PaginationSet<FeaturedSponsorDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<FeaturedSponsorDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<FeaturedSponsorDTO> GetFeaturedSponsors()
        {           
            var basequery = Mapper.Map <List<FeaturedSponsorDTO>>(DataLayer.DataLayerFacade.FeaturedSponsorRepository().GetFeaturedSponsorsAllAsQueryable());
            return basequery.ToList();
        }

        
        public DTResult<FeaturedSponsorDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.FeaturedSponsorRepository().GetFeaturedSponsorsAllAsQueryable().Where(m=> m.ProfileID==ProfileID);

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<FeaturedSponsor>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<FeaturedSponsorDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<FeaturedSponsorDTO> result = new DTResult<FeaturedSponsorDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }               
        
        
        public PaginationSet<FeaturedSponsorDTO> GetFeaturedSponsorsByProfileID(int ProfileID, int page, int pagesize)
        {
            
            var basequery = DataLayer.DataLayerFacade.FeaturedSponsorRepository().GetFeaturedSponsorsByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<FeaturedSponsor>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<FeaturedSponsorDTO> result = new PaginationSet<FeaturedSponsorDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<FeaturedSponsorDTO>>(queryList.ToList())
            };

            return result;            
        }

        public FeaturedSponsorDTO Insert(FeaturedSponsorDTO objFeaturedSponsor)
        {            
            FeaturedSponsorDTO result = Mapper.Map<FeaturedSponsorDTO>(DataLayer.DataLayerFacade.FeaturedSponsorRepository().Insert(objFeaturedSponsor.FeatureID, objFeaturedSponsor.ProfileID, objFeaturedSponsor.FeaturedSponsorMode, objFeaturedSponsor.FeaturedRole));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new FeaturedSponsorDTO();
            }
        }
               
        public bool Delete(int FeatureID, int ProfileID)
        {            
            bool result = DataLayer.DataLayerFacade.FeaturedSponsorRepository().Delete(FeatureID, ProfileID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int FeatureID, int ProfileID, FeaturedSponsorDTO objFeaturedSponsor)
        {            
            bool result = DataLayer.DataLayerFacade.FeaturedSponsorRepository().Update(objFeaturedSponsor.FeatureID, objFeaturedSponsor.ProfileID, objFeaturedSponsor.FeaturedSponsorMode, objFeaturedSponsor.FeaturedRole);
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