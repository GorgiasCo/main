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
    public class FBActivityFacade
    {                
        public FBActivityDTO GetFBActivity(int FBActivityID)
        {
            FBActivityDTO result = Mapper.Map<FBActivityDTO>(DataLayer.DataLayerFacade.FBActivityRepository().GetFBActivity(FBActivityID));             
            return result;
        }

        public DTResult<FBActivityDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.FBActivityRepository().GetFBActivitiesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.FBActivityCount.Equals(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<FBActivity>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<FBActivityDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<FBActivityDTO> result = new DTResult<FBActivityDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<FBActivityDTO> GetFBActivities(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.FBActivityRepository().GetFBActivitiesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<FBActivity>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<FBActivityDTO> result = new PaginationSet<FBActivityDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<FBActivityDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<FBActivityDTO> GetFBActivities()
        {           
            var basequery = Mapper.Map <List<FBActivityDTO>>(DataLayer.DataLayerFacade.FBActivityRepository().GetFBActivitiesAllAsQueryable());
            return basequery.ToList();
        }

        

        public FBActivityDTO Insert(FBActivityDTO objFBActivity)
        {            
            FBActivityDTO result = Mapper.Map<FBActivityDTO>(DataLayer.DataLayerFacade.FBActivityRepository().Insert(objFBActivity.FBActivityCount, objFBActivity.FBActivityDate, objFBActivity.FBActivityType));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new FBActivityDTO();
            }
        }
               
        public bool Delete(int FBActivityID)
        {            
            bool result = DataLayer.DataLayerFacade.FBActivityRepository().Delete(FBActivityID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int FBActivityID, FBActivityDTO objFBActivity)
        {            
            bool result = DataLayer.DataLayerFacade.FBActivityRepository().Update(objFBActivity.FBActivityID, objFBActivity.FBActivityCount, objFBActivity.FBActivityDate, objFBActivity.FBActivityType);
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