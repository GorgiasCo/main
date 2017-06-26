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
    public class ProfileSocialNetworkFacade
    {                
        public ProfileSocialNetworkDTO GetProfileSocialNetwork(int SocialNetworkID, int ProfileID)
        {
            ProfileSocialNetworkDTO result = Mapper.Map<ProfileSocialNetworkDTO>(DataLayer.DataLayerFacade.ProfileSocialNetworkRepository().GetProfileSocialNetwork(SocialNetworkID, ProfileID));             
            return result;
        }

        public DTResult<ProfileSocialNetworkDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileSocialNetworkRepository().GetProfileSocialNetworksAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileSocialNetwork>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileSocialNetworkDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileSocialNetworkDTO> result = new DTResult<ProfileSocialNetworkDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<ProfileSocialNetworkDTO> GetProfileSocialNetworks(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.ProfileSocialNetworkRepository().GetProfileSocialNetworksAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileSocialNetwork>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileSocialNetworkDTO> result = new PaginationSet<ProfileSocialNetworkDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileSocialNetworkDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<ProfileSocialNetworkDTO> GetProfileSocialNetworks()
        {           
            var basequery = Mapper.Map <List<ProfileSocialNetworkDTO>>(DataLayer.DataLayerFacade.ProfileSocialNetworkRepository().GetProfileSocialNetworksAllAsQueryable());
            return basequery.ToList();
        }

        
        public DTResult<ProfileSocialNetworkDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileSocialNetworkRepository().GetProfileSocialNetworksAllAsQueryable().Where(m=> m.ProfileID==ProfileID);

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileSocialNetwork>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileSocialNetworkDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileSocialNetworkDTO> result = new DTResult<ProfileSocialNetworkDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }               
        
        
        public PaginationSet<ProfileSocialNetworkDTO> GetProfileSocialNetworksByProfileID(int ProfileID, int page, int pagesize)
        {
            
            var basequery = DataLayer.DataLayerFacade.ProfileSocialNetworkRepository().GetProfileSocialNetworksByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileSocialNetwork>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileSocialNetworkDTO> result = new PaginationSet<ProfileSocialNetworkDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileSocialNetworkDTO>>(queryList.ToList())
            };

            return result;            
        }

        public ProfileSocialNetworkDTO Insert(ProfileSocialNetworkDTO objProfileSocialNetwork)
        {            
            ProfileSocialNetworkDTO result = Mapper.Map<ProfileSocialNetworkDTO>(DataLayer.DataLayerFacade.ProfileSocialNetworkRepository().Insert(objProfileSocialNetwork.SocialNetworkID, objProfileSocialNetwork.ProfileID, objProfileSocialNetwork.ProfileSocialNetworkURL));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new ProfileSocialNetworkDTO();
            }
        }
               
        public bool Delete(int SocialNetworkID, int ProfileID)
        {            
            bool result = DataLayer.DataLayerFacade.ProfileSocialNetworkRepository().Delete(SocialNetworkID, ProfileID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int SocialNetworkID, int ProfileID, ProfileSocialNetworkDTO objProfileSocialNetwork)
        {            
            bool result = DataLayer.DataLayerFacade.ProfileSocialNetworkRepository().Update(objProfileSocialNetwork.SocialNetworkID, objProfileSocialNetwork.ProfileID, objProfileSocialNetwork.ProfileSocialNetworkURL);
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