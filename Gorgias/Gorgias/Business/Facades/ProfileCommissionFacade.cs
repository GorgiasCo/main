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
    public class ProfileCommissionFacade
    {
        public ProfileCommissionDTO GetProfileCommission(int ProfileCommissionID)
        {
            ProfileCommissionDTO result = Mapper.Map<ProfileCommissionDTO>(DataLayer.DataLayerFacade.ProfileCommissionRepository().GetProfileCommission(ProfileCommissionID));
            return result;
        }

        public DTResult<ProfileCommissionDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileCommissionRepository().GetProfileCommissionsAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileCommissionRate.Equals(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileCommission>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileCommissionDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileCommissionDTO> result = new DTResult<ProfileCommissionDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<ProfileCommissionDTO> GetProfileCommissions(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileCommissionRepository().GetProfileCommissionsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileCommission>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileCommissionDTO> result = new PaginationSet<ProfileCommissionDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileCommissionDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<ProfileCommissionDTO> GetProfileCommissions()
        {
            var basequery = Mapper.Map<List<ProfileCommissionDTO>>(DataLayer.DataLayerFacade.ProfileCommissionRepository().GetProfileCommissionsAllAsQueryable());
            return basequery.ToList();
        }


        public DTResult<ProfileCommissionDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileCommissionRepository().GetProfileCommissionsAllAsQueryable().Where(m => m.ProfileID == ProfileID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileCommissionRate.Equals(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileCommission>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileCommissionDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileCommissionDTO> result = new DTResult<ProfileCommissionDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ProfileCommissionDTO> GetProfileCommissionsByProfileID(int ProfileID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ProfileCommissionRepository().GetProfileCommissionsByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileCommission>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileCommissionDTO> result = new PaginationSet<ProfileCommissionDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileCommissionDTO>>(queryList.ToList())
            };

            return result;
        }
        public DTResult<ProfileCommissionDTO> FilterResultByUserID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int UserID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileCommissionRepository().GetProfileCommissionsAllAsQueryable().Where(m => m.UserID == UserID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileCommissionRate.Equals(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileCommission>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileCommissionDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileCommissionDTO> result = new DTResult<ProfileCommissionDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ProfileCommissionDTO> GetProfileCommissionsByUserID(int UserID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ProfileCommissionRepository().GetProfileCommissionsByUserIDAsQueryable(UserID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileCommission>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileCommissionDTO> result = new PaginationSet<ProfileCommissionDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileCommissionDTO>>(queryList.ToList())
            };

            return result;
        }
        public DTResult<ProfileCommissionDTO> FilterResultByUserRoleID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int UserRoleID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileCommissionRepository().GetProfileCommissionsAllAsQueryable().Where(m => m.UserRoleID == UserRoleID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileCommissionRate.Equals(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileCommission>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileCommissionDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileCommissionDTO> result = new DTResult<ProfileCommissionDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ProfileCommissionDTO> GetProfileCommissionsByUserRoleID(int UserRoleID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ProfileCommissionRepository().GetProfileCommissionsByUserRoleIDAsQueryable(UserRoleID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileCommission>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileCommissionDTO> result = new PaginationSet<ProfileCommissionDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileCommissionDTO>>(queryList.ToList())
            };

            return result;
        }

        public ProfileCommissionDTO Insert(ProfileCommissionDTO objProfileCommission)
        {
            ProfileCommissionDTO result = Mapper.Map<ProfileCommissionDTO>(DataLayer.DataLayerFacade.ProfileCommissionRepository().Insert(objProfileCommission.ProfileCommissionRate, objProfileCommission.ProfileCommissionDateCreated, objProfileCommission.ProfileCommissionStatus, objProfileCommission.ProfileID, objProfileCommission.UserID, objProfileCommission.UserRoleID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new ProfileCommissionDTO();
            }
        }

        public bool Delete(int ProfileCommissionID)
        {
            bool result = DataLayer.DataLayerFacade.ProfileCommissionRepository().Delete(ProfileCommissionID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileCommissionID, ProfileCommissionDTO objProfileCommission)
        {
            bool result = DataLayer.DataLayerFacade.ProfileCommissionRepository().Update(objProfileCommission.ProfileCommissionID, objProfileCommission.ProfileCommissionRate, objProfileCommission.ProfileCommissionDateCreated, objProfileCommission.ProfileCommissionStatus, objProfileCommission.ProfileID, objProfileCommission.UserID, objProfileCommission.UserRoleID);
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