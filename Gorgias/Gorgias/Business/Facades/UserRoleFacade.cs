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
    public class UserRoleFacade
    {
        public UserRoleDTO GetUserRole(int UserRoleID)
        {
            UserRoleDTO result = Mapper.Map<UserRoleDTO>(DataLayer.DataLayerFacade.UserRoleRepository().GetUserRole(UserRoleID));
            return result;
        }

        public DTResult<UserRoleDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.UserRoleRepository().GetUserRolesAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.UserRoleName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<UserRole>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<UserRoleDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<UserRoleDTO> result = new DTResult<UserRoleDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<UserRoleDTO> GetUserRoles(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.UserRoleRepository().GetUserRolesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<UserRole>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<UserRoleDTO> result = new PaginationSet<UserRoleDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<UserRoleDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<UserRoleDTO> GetUserRoles()
        {
            var basequery = Mapper.Map<List<UserRoleDTO>>(DataLayer.DataLayerFacade.UserRoleRepository().GetUserRolesAllAsQueryable());
            return basequery.ToList();
        }

        /// <summary>
        /// Get User roles for Country Distributor ;) 
        /// </summary>
        /// <returns></returns>
        public List<UserRoleDTO> GetUserRolesForCountry()
        {
            var basequery = Mapper.Map<List<UserRoleDTO>>(DataLayer.DataLayerFacade.UserRoleRepository().GetUserRolesAllAsQueryable(true));
            return basequery.ToList();
        }

        public UserRoleDTO Insert(UserRoleDTO objUserRole)
        {
            UserRoleDTO result = Mapper.Map<UserRoleDTO>(DataLayer.DataLayerFacade.UserRoleRepository().Insert(objUserRole.UserRoleName, objUserRole.UserRoleStatus, objUserRole.UserRoleImage, objUserRole.UserRoleDescription));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new UserRoleDTO();
            }
        }

        public bool Delete(int UserRoleID)
        {
            bool result = DataLayer.DataLayerFacade.UserRoleRepository().Delete(UserRoleID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int UserRoleID, UserRoleDTO objUserRole)
        {
            bool result = DataLayer.DataLayerFacade.UserRoleRepository().Update(objUserRole.UserRoleID, objUserRole.UserRoleName, objUserRole.UserRoleStatus, objUserRole.UserRoleImage, objUserRole.UserRoleDescription);
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