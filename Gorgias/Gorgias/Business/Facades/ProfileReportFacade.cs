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
    public class ProfileReportFacade
    {                
        public ProfileReportDTO GetProfileReport(int ProfileReportID)
        {
            ProfileReportDTO result = Mapper.Map<ProfileReportDTO>(DataLayer.DataLayerFacade.ProfileReportRepository().GetProfileReport(ProfileReportID));             
            return result;
        }

        public DTResult<ProfileReportDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileReportRepository().GetProfileReportsAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.ProfileReportActivityCount.Equals(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileReport>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileReportDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileReportDTO> result = new DTResult<ProfileReportDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<ProfileReportDTO> GetProfileReports(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.ProfileReportRepository().GetProfileReportsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileReport>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileReportDTO> result = new PaginationSet<ProfileReportDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileReportDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<ProfileReportDTO> GetProfileReports()
        {           
            var basequery = Mapper.Map <List<ProfileReportDTO>>(DataLayer.DataLayerFacade.ProfileReportRepository().GetProfileReportsAllAsQueryable());
            return basequery.ToList();
        }

        
        public DTResult<ProfileReportDTO> FilterResultByReportTypeID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ReportTypeID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileReportRepository().GetProfileReportsAllAsQueryable().Where(m=> m.ReportTypeID==ReportTypeID);

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.ProfileReportActivityCount.Equals(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileReport>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileReportDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileReportDTO> result = new DTResult<ProfileReportDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }               
        
        
        public PaginationSet<ProfileReportDTO> GetProfileReportsByReportTypeID(int ReportTypeID, int page, int pagesize)
        {
            
            var basequery = DataLayer.DataLayerFacade.ProfileReportRepository().GetProfileReportsByReportTypeIDAsQueryable(ReportTypeID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileReport>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileReportDTO> result = new PaginationSet<ProfileReportDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileReportDTO>>(queryList.ToList())
            };

            return result;            
        }
        public DTResult<ProfileReportDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileReportRepository().GetProfileReportsAllAsQueryable().Where(m=> m.ProfileID==ProfileID);

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.ProfileReportActivityCount.Equals(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileReport>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileReportDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileReportDTO> result = new DTResult<ProfileReportDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }               
        
        
        public PaginationSet<ProfileReportDTO> GetProfileReportsByProfileID(int ProfileID, int page, int pagesize)
        {
            
            var basequery = DataLayer.DataLayerFacade.ProfileReportRepository().GetProfileReportsByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileReport>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileReportDTO> result = new PaginationSet<ProfileReportDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileReportDTO>>(queryList.ToList())
            };

            return result;            
        }

        public IEnumerable<ProfileReportDTO> GetProfileReportsByProfileID(int ProfileID)
        {
            return Mapper.Map<IEnumerable<ProfileReportDTO>>(DataLayer.DataLayerFacade.ProfileReportRepository().GetProfileReportsByProfileIDAsIEnumerable(ProfileID));                        
        }

        public IEnumerable<ProfileReportDTO> GetProfileReportsByProfileID(int ProfileID, int RevenueID)
        {
            return Mapper.Map<IEnumerable<ProfileReportDTO>>(DataLayer.DataLayerFacade.ProfileReportRepository().GetProfileReportsByProfileIDAsIEnumerable(ProfileID, RevenueID));
        }

        public DTResult<ProfileReportDTO> FilterResultByRevenueID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int RevenueID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileReportRepository().GetProfileReportsAllAsQueryable().Where(m=> m.RevenueID==RevenueID);

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.ProfileReportActivityCount.Equals(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileReport>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileReportDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileReportDTO> result = new DTResult<ProfileReportDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }               
        
        
        public PaginationSet<ProfileReportDTO> GetProfileReportsByRevenueID(int RevenueID, int page, int pagesize)
        {
            
            var basequery = DataLayer.DataLayerFacade.ProfileReportRepository().GetProfileReportsByRevenueIDAsQueryable(RevenueID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileReport>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileReportDTO> result = new PaginationSet<ProfileReportDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileReportDTO>>(queryList.ToList())
            };

            return result;            
        }

        public ProfileReportDTO Insert(ProfileReportDTO objProfileReport)
        {            
            ProfileReportDTO result = Mapper.Map<ProfileReportDTO>(DataLayer.DataLayerFacade.ProfileReportRepository().Insert(objProfileReport.ProfileReportActivityCount, objProfileReport.ProfileReportRevenue, objProfileReport.ReportTypeID, objProfileReport.ProfileID, objProfileReport.RevenueID));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new ProfileReportDTO();
            }
        }
               
        public bool Delete(int ProfileReportID)
        {            
            bool result = DataLayer.DataLayerFacade.ProfileReportRepository().Delete(ProfileReportID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileReportID, ProfileReportDTO objProfileReport)
        {            
            bool result = DataLayer.DataLayerFacade.ProfileReportRepository().Update(objProfileReport.ProfileReportID, objProfileReport.ProfileReportActivityCount, objProfileReport.ProfileReportRevenue, objProfileReport.ReportTypeID, objProfileReport.ProfileID, objProfileReport.RevenueID);
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