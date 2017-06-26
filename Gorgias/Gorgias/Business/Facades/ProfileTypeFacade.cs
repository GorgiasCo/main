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
    public class ProfileTypeFacade
    {                
        public ProfileTypeDTO GetProfileType(int ProfileTypeID)
        {
            ProfileTypeDTO result = Mapper.Map<ProfileTypeDTO>(DataLayer.DataLayerFacade.ProfileTypeRepository().GetProfileType(ProfileTypeID));             
            return result;
        }

        public DTResult<ProfileTypeDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileTypeRepository().GetProfileTypesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.ProfileTypeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileType>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileTypeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileTypeDTO> result = new DTResult<ProfileTypeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<ProfileTypeDTO> GetProfileTypes(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.ProfileTypeRepository().GetProfileTypesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileType>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileTypeDTO> result = new PaginationSet<ProfileTypeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileTypeDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<ProfileTypeDTO> GetProfileTypes()
        {           
            var basequery = Mapper.Map <List<ProfileTypeDTO>>(DataLayer.DataLayerFacade.ProfileTypeRepository().GetProfileTypesAllAsQueryable());
            return basequery.ToList();
        }

        

        public ProfileTypeDTO Insert(ProfileTypeDTO objProfileType)
        {            
            ProfileTypeDTO result = Mapper.Map<ProfileTypeDTO>(DataLayer.DataLayerFacade.ProfileTypeRepository().Insert(objProfileType.ProfileTypeName, objProfileType.ProfileTypeStatus, objProfileType.ProfileTypeImage, objProfileType.ProfileTypeDescription, objProfileType.ProfileTypeParentID));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new ProfileTypeDTO();
            }
        }
               
        public bool Delete(int ProfileTypeID)
        {            
            bool result = DataLayer.DataLayerFacade.ProfileTypeRepository().Delete(ProfileTypeID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileTypeID, ProfileTypeDTO objProfileType)
        {            
            bool result = DataLayer.DataLayerFacade.ProfileTypeRepository().Update(objProfileType.ProfileTypeID, objProfileType.ProfileTypeName, objProfileType.ProfileTypeStatus, objProfileType.ProfileTypeImage, objProfileType.ProfileTypeDescription, objProfileType.ProfileTypeParentID);
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