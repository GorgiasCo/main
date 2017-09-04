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
    public class ActivityTypeFacade
    {
        //V2 Begin

        public IQueryable<Business.DataTransferObjects.Mobile.V2.ActivityTypeMobileModel> getActivityTypes(string languageCode, int ActivityTypeParentID)
        {
            return DataLayer.DataLayerFacade.ActivityTypeRepository().GetActivityTypesFeltAsQueryable(languageCode, ActivityTypeParentID);
        }

        //V2 End

        public ActivityTypeDTO GetActivityType(int ActivityTypeID)
        {
            ActivityTypeDTO result = Mapper.Map<ActivityTypeDTO>(DataLayer.DataLayerFacade.ActivityTypeRepository().GetActivityType(ActivityTypeID));
            return result;
        }

        public DTResult<ActivityTypeDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ActivityTypeRepository().GetActivityTypesAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ActivityTypeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ActivityType>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ActivityTypeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ActivityTypeDTO> result = new DTResult<ActivityTypeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<ActivityTypeDTO> GetActivityTypes(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.ActivityTypeRepository().GetActivityTypesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ActivityType>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ActivityTypeDTO> result = new PaginationSet<ActivityTypeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ActivityTypeDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<ActivityTypeDTO> GetActivityTypes()
        {
            var basequery = Mapper.Map<List<ActivityTypeDTO>>(DataLayer.DataLayerFacade.ActivityTypeRepository().GetActivityTypesAllAsQueryable());
            return basequery.ToList();
        }


        public DTResult<ActivityTypeDTO> FilterResultByActivityTypeParentID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ActivityTypeParentID)
        {
            var basequery = DataLayer.DataLayerFacade.ActivityTypeRepository().GetActivityTypesAllAsQueryable().Where(m => m.ActivityTypeParentID == ActivityTypeParentID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ActivityTypeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ActivityType>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ActivityTypeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ActivityTypeDTO> result = new DTResult<ActivityTypeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ActivityTypeDTO> GetActivityTypesByActivityTypeParentID(int ActivityTypeParentID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ActivityTypeRepository().GetActivityTypesByActivityTypeParentIDAsQueryable(ActivityTypeParentID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ActivityType>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ActivityTypeDTO> result = new PaginationSet<ActivityTypeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ActivityTypeDTO>>(queryList.ToList())
            };

            return result;
        }

        public ActivityTypeDTO Insert(ActivityTypeDTO objActivityType)
        {
            ActivityTypeDTO result = Mapper.Map<ActivityTypeDTO>(DataLayer.DataLayerFacade.ActivityTypeRepository().Insert(objActivityType.ActivityTypeName, objActivityType.ActivityTypeLanguageCode, objActivityType.ActivityTypeStatus, objActivityType.ActivityTypeParentID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new ActivityTypeDTO();
            }
        }

        public bool Delete(int ActivityTypeID)
        {
            bool result = DataLayer.DataLayerFacade.ActivityTypeRepository().Delete(ActivityTypeID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ActivityTypeID, ActivityTypeDTO objActivityType)
        {
            bool result = DataLayer.DataLayerFacade.ActivityTypeRepository().Update(objActivityType.ActivityTypeID, objActivityType.ActivityTypeName, objActivityType.ActivityTypeLanguageCode, objActivityType.ActivityTypeStatus, objActivityType.ActivityTypeParentID);
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