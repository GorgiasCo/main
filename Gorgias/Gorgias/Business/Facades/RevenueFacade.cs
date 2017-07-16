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
    public class RevenueFacade
    {                
        public RevenueDTO GetRevenue(int RevenueID)
        {
            RevenueDTO result = Mapper.Map<RevenueDTO>(DataLayer.DataLayerFacade.RevenueRepository().GetRevenue(RevenueID));             
            return result;
        }

        public DTResult<RevenueDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.RevenueRepository().GetRevenuesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.RevenueDateCreated.Equals(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Revenue>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<RevenueDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<RevenueDTO> result = new DTResult<RevenueDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<RevenueDTO> GetRevenues(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.RevenueRepository().GetRevenuesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Revenue>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<RevenueDTO> result = new PaginationSet<RevenueDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<RevenueDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<RevenueDTO> GetRevenues()
        {           
            var basequery = Mapper.Map <List<RevenueDTO>>(DataLayer.DataLayerFacade.RevenueRepository().GetRevenuesAllAsQueryable());
            return basequery.ToList();
        }

        

        public RevenueDTO Insert(RevenueDTO objRevenue)
        {            
            RevenueDTO result = Mapper.Map<RevenueDTO>(DataLayer.DataLayerFacade.RevenueRepository().Insert(objRevenue.RevenueDateCreated, objRevenue.RevenueAmount, objRevenue.RevenueTotalViews, objRevenue.RevenueMemberShare));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new RevenueDTO();
            }
        }
               
        public bool Delete(int RevenueID)
        {            
            bool result = DataLayer.DataLayerFacade.RevenueRepository().Delete(RevenueID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int RevenueID, RevenueDTO objRevenue)
        {            
            bool result = DataLayer.DataLayerFacade.RevenueRepository().Update(objRevenue.RevenueID, objRevenue.RevenueDateCreated, objRevenue.RevenueAmount, objRevenue.RevenueTotalViews, objRevenue.RevenueMemberShare);
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