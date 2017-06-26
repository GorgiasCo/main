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
    public class UserFacade
    {                
        public UserDTO GetUser(int UserID)
        {
            UserDTO result = Mapper.Map<UserDTO>(DataLayer.DataLayerFacade.UserRepository().GetUser(UserID));             
            return result;
        }

        public UserCustomDTO GetUser(string UserEmail)
        {
            UserCustomDTO result = Mapper.Map<UserCustomDTO>(DataLayer.DataLayerFacade.UserRepository().GetUserForAdministration(UserEmail));
            return result;
        }

        public UserCustomDTO GetUserForReport(int UserID)
        {
            UserCustomDTO result = Mapper.Map<UserCustomDTO>(DataLayer.DataLayerFacade.UserRepository().GetUserForAdministration(UserID));
            return result;
        }

        public bool GetUserProfileValidity(int UserID, int ProfileID)
        {
            bool result = DataLayer.DataLayerFacade.UserRepository().GetUserByProfile(UserID,ProfileID);
            return result;
        }

        public DTResult<UserDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.UserRepository().GetUsersAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.UserFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<User>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<UserDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<UserDTO> result = new DTResult<UserDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public DTResult<UserDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int CountryID)
        {
            var basequery = DataLayer.DataLayerFacade.UserRepository().GetUsersAllAsQueryable().Where(m=> m.CountryID == CountryID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.UserFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<User>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<UserDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<UserDTO> result = new DTResult<UserDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<UserDTO> GetUsers(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.UserRepository().GetUsersAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<User>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<UserDTO> result = new PaginationSet<UserDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<UserDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<UserDTO> GetUsers()
        {           
            var basequery = Mapper.Map <List<UserDTO>>(DataLayer.DataLayerFacade.UserRepository().GetUsersAllAsQueryable());
            return basequery.ToList();
        }

        public List<UserDTO> GetUsers(int CountryID)
        {
            var basequery = Mapper.Map<List<UserDTO>>(DataLayer.DataLayerFacade.UserRepository().GetUsersAllAsQueryable(CountryID));
            return basequery.ToList();
        }

        public UserDTO Insert(string UserFullname, string UserEmail)
        {
            UserDTO result = Mapper.Map<UserDTO>(DataLayer.DataLayerFacade.UserRepository().Insert(UserFullname, UserEmail));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new UserDTO();
            }
        }

        public async System.Threading.Tasks.Task<UserDTO> Insert(UserDTO objUser)
        {            
            UserDTO result = Mapper.Map<UserDTO>(DataLayer.DataLayerFacade.UserRepository().Insert(objUser.UserFullname, objUser.UserEmail, objUser.UserStatus, objUser.UserIsBlocked, objUser.CountryID));

            Business.DataTransferObjects.Authentication.UserModel userModel = new Business.DataTransferObjects.Authentication.UserModel();
            userModel.Password = "HelloGorgias";
            userModel.UserName = result.UserEmail;

            DataLayer.Authentication.AuthRepository _repo = new DataLayer.Authentication.AuthRepository();
            IdentityResult resultAccountRegistration = await _repo.RegisterUserAdmin(userModel);

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new UserDTO();
            }
        }
               
        public async System.Threading.Tasks.Task<bool> Delete(int UserID)
        {            
            bool result = await DataLayer.DataLayerFacade.UserRepository().Delete(UserID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int UserID, UserDTO objUser)
        {            
            bool result = DataLayer.DataLayerFacade.UserRepository().Update(objUser.UserID, objUser.UserFullname, objUser.UserEmail, objUser.UserStatus, objUser.UserIsBlocked, objUser.UserDateCreated, objUser.UserDateConfirmed, objUser.CountryID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int UserID)
        {
            bool result = DataLayer.DataLayerFacade.UserRepository().Update(UserID);
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