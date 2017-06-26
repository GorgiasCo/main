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
    public class UserProfileFacade
    {
        public UserProfileDTO GetUserProfile(int ProfileID, int UserRoleID, int UserID)
        {
            UserProfileDTO result = Mapper.Map<UserProfileDTO>(DataLayer.DataLayerFacade.UserProfileRepository().GetUserProfile(ProfileID, UserRoleID, UserID));
            return result;
        }

        public UserProfileDTO GetUserProfile(int ProfileID, int UserRoleID)
        {
            UserProfileDTO result = Mapper.Map<UserProfileDTO>(DataLayer.DataLayerFacade.UserProfileRepository().GetUserProfile(ProfileID, UserRoleID));
            return result;
        }

        public IEnumerable<UserProfileDTO> GetAdministrationUserProfile(string UserEmail)
        {
            IEnumerable<UserProfileDTO> result = Mapper.Map<IEnumerable<UserProfileDTO>>(DataLayer.DataLayerFacade.UserProfileRepository().GetAdministrationUserProfile(UserEmail));
            return result;
        }

        public IEnumerable<UserProfileDTO> GetAdministrationCountryUserProfile(int CountryID)
        {
            IEnumerable<UserProfileDTO> result = Mapper.Map<IEnumerable<UserProfileDTO>>(DataLayer.DataLayerFacade.UserProfileRepository().GetAdministrationCountryUserProfile(CountryID));
            return result;
        }

        public IEnumerable<UserProfileDTO> GetUserProfileAgency(int UserID)
        {
            IEnumerable<UserProfileDTO> result = Mapper.Map<IEnumerable<UserProfileDTO>>(DataLayer.DataLayerFacade.UserProfileRepository().GetUserProfileAgency(UserID));
            return result;
        }

        public UserProfileDTO GetAdministrationUserProfileValidation(int UserID, int ProfileID)
        {
            UserProfileDTO result = Mapper.Map<UserProfileDTO>(DataLayer.DataLayerFacade.UserProfileRepository().GetAdministrationUserProfileValidation(UserID, ProfileID));
            return result;
        }

        public int[] GetAdministrationUserProfileReport(int UserID)
        {
            int[] result = DataLayer.DataLayerFacade.UserProfileRepository().GetAdministrationUserProfile(UserID);
            return result;
        }

        public DTResult<UserProfileDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.UserProfileRepository().GetUserProfilesAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.User.UserFullname.ToLower().Contains(search.ToLower()) || p.Profile.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<UserProfile>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<UserProfileDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<UserProfileDTO> result = new DTResult<UserProfileDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public DTResult<UserProfileDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int CountryID)
        {
            var basequery = DataLayer.DataLayerFacade.UserProfileRepository().GetUserProfilesAllAsQueryable().Where(m=> m.Profile.Addresses.Any(a=> a.City.CountryID == CountryID));

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.User.UserFullname.ToLower().Contains(search.ToLower()) || p.Profile.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<UserProfile>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<UserProfileDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<UserProfileDTO> result = new DTResult<UserProfileDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<UserProfileDTO> GetUserProfiles(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.UserProfileRepository().GetUserProfilesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<UserProfile>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<UserProfileDTO> result = new PaginationSet<UserProfileDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<UserProfileDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<UserProfileDTO> GetUserProfiles()
        {
            var basequery = Mapper.Map<List<UserProfileDTO>>(DataLayer.DataLayerFacade.UserProfileRepository().GetUserProfilesAllAsQueryable());
            return basequery.ToList();
        }


        public DTResult<UserProfileDTO> FilterResultByUserRoleID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int UserRoleID)
        {
            var basequery = DataLayer.DataLayerFacade.UserProfileRepository().GetUserProfilesAllAsQueryable().Where(m => m.UserRoleID == UserRoleID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.UserRole.UserRoleName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<UserProfile>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<UserProfileDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<UserProfileDTO> result = new DTResult<UserProfileDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<UserProfileDTO> GetUserProfilesByUserRoleID(int UserRoleID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.UserProfileRepository().GetUserProfilesByUserRoleIDAsQueryable(UserRoleID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<UserProfile>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<UserProfileDTO> result = new PaginationSet<UserProfileDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<UserProfileDTO>>(queryList.ToList())
            };

            return result;
        }
        public DTResult<UserProfileDTO> FilterResultByUserID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int UserID)
        {
            var basequery = DataLayer.DataLayerFacade.UserProfileRepository().GetUserProfilesAllAsQueryable().Where(m => m.UserID == UserID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.UserRole.UserRoleName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<UserProfile>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<UserProfileDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<UserProfileDTO> result = new DTResult<UserProfileDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<UserProfileDTO> GetUserProfilesByUserID(int UserID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.UserProfileRepository().GetUserProfilesByUserIDAsQueryable(UserID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<UserProfile>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<UserProfileDTO> result = new PaginationSet<UserProfileDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<UserProfileDTO>>(queryList.ToList())
            };

            return result;
        }

        public async System.Threading.Tasks.Task<UserProfileDTO> Insert(UserProfileDTO objUserProfile)
        {
            //Register User 
            //Send Reset Password to them to login

            UserProfileDTO result = Mapper.Map<UserProfileDTO>(DataLayer.DataLayerFacade.UserProfileRepository().Insert(objUserProfile.ProfileID, objUserProfile.UserRoleID, objUserProfile.UserID));
            //Profile registrationProfile = DataLayer.DataLayerFacade.ProfileRepository().GetProfile(result.ProfileID);

            //Business.DataTransferObjects.Authentication.UserModel userModel = new Business.DataTransferObjects.Authentication.UserModel();
            //userModel.Password = "HelloGorgias";
            //userModel.UserName = registrationProfile.ProfileEmail;

            //DataLayer.Authentication.AuthRepository _repo = new DataLayer.Authentication.AuthRepository();
            //IdentityResult resultAccountRegistration = await _repo.RegisterUserAdmin(userModel);

            if (result != null)
            {
                return result;
            }
            else
            {
                return new UserProfileDTO();
            }
        }

        public async System.Threading.Tasks.Task<bool> Delete(int ProfileID, int UserRoleID, int UserID)
        {
            bool result = await DataLayer.DataLayerFacade.UserProfileRepository().Delete(ProfileID, UserRoleID, UserID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileID, int UserRoleID, int UserID, UserProfileDTO objUserProfile)
        {
            bool result = DataLayer.DataLayerFacade.UserProfileRepository().Update(objUserProfile.ProfileID, objUserProfile.UserRoleID, objUserProfile.UserID);
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