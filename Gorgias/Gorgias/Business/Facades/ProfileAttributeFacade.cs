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
    public class ProfileAttributeFacade
    {                
        public ProfileAttributeDTO GetProfileAttribute(int AttributeID, int ProfileID)
        {
            ProfileAttributeDTO result = Mapper.Map<ProfileAttributeDTO>(DataLayer.DataLayerFacade.ProfileAttributeRepository().GetProfileAttribute(AttributeID, ProfileID));             
            return result;
        }

        public DTResult<ProfileAttributeDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileAttributeRepository().GetProfileAttributesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.ProfileAttributeNote.Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileAttribute>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileAttributeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileAttributeDTO> result = new DTResult<ProfileAttributeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<ProfileAttributeDTO> GetProfileAttributes(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.ProfileAttributeRepository().GetProfileAttributesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileAttribute>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileAttributeDTO> result = new PaginationSet<ProfileAttributeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileAttributeDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<ProfileAttributeDTO> GetProfileAttributes()
        {           
            var basequery = Mapper.Map <List<ProfileAttributeDTO>>(DataLayer.DataLayerFacade.ProfileAttributeRepository().GetProfileAttributesAllAsQueryable());
            return basequery.ToList();
        }

        
        public DTResult<ProfileAttributeDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileAttributeRepository().GetProfileAttributesAllAsQueryable().Where(m=> m.ProfileID==ProfileID);

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.ProfileAttributeNote.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileAttribute>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileAttributeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileAttributeDTO> result = new DTResult<ProfileAttributeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }               
        
        
        public PaginationSet<ProfileAttributeDTO> GetProfileAttributesByProfileID(int ProfileID, int page, int pagesize)
        {
            
            var basequery = DataLayer.DataLayerFacade.ProfileAttributeRepository().GetProfileAttributesByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileAttribute>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileAttributeDTO> result = new PaginationSet<ProfileAttributeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileAttributeDTO>>(queryList.ToList())
            };

            return result;            
        }

        public ProfileAttributeDTO Insert(ProfileAttributeDTO objProfileAttribute)
        {            
            ProfileAttributeDTO result = Mapper.Map<ProfileAttributeDTO>(DataLayer.DataLayerFacade.ProfileAttributeRepository().Insert(objProfileAttribute.AttributeID, objProfileAttribute.ProfileID, objProfileAttribute.ProfileAttributeNote));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new ProfileAttributeDTO();
            }
        }
               
        public bool Delete(int AttributeID, int ProfileID)
        {            
            bool result = DataLayer.DataLayerFacade.ProfileAttributeRepository().Delete(AttributeID, ProfileID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int AttributeID, int ProfileID, ProfileAttributeDTO objProfileAttribute)
        {            
            bool result = DataLayer.DataLayerFacade.ProfileAttributeRepository().Update(objProfileAttribute.AttributeID, objProfileAttribute.ProfileID, objProfileAttribute.ProfileAttributeNote);
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