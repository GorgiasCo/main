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
    public class ProfileFacade
    {
        //V2 Begin
        public Business.DataTransferObjects.Mobile.V2.LoginAttempt getLoginAttempt(string ProfileEmail, int? ProfileID)
        {
            return DataLayer.DataLayerFacade.ProfileRepository().getLoginAttempt(ProfileEmail, ProfileID);
        }
        //V2 End
        public ProfileDTO GetProfile(int ProfileID)
        {
            ProfileDTO result = Mapper.Map<ProfileDTO>(DataLayer.DataLayerFacade.ProfileRepository().GetProfile(ProfileID));
            return result;
        }

        public IEnumerable<Business.DataTransferObjects.Report.ProfileReport> GetProfileReportCurrent()
        {
            return DataLayer.DataLayerFacade.ProfileRepository().GetProfileReportCurrent();
        }

        public Int64 GetProfileReportCurrentProfileViews()
        {
            return DataLayer.DataLayerFacade.ProfileRepository().GetProfileReportCurrentProfileViews();
        }

        public IList<Business.DataTransferObjects.Report.ProfileReport> GetProfileReportCurrent(int UserID)
        {
            return DataLayer.DataLayerFacade.ProfileRepository().GetProfileReportCurrent(UserID);
        }

        public IList<Business.DataTransferObjects.Report.ProfileReport> GetProfileReportCurrentByCountry(int CountryID)
        {
            return DataLayer.DataLayerFacade.ProfileRepository().GetProfileReportCurrentByCountry(CountryID);
        }

        public IEnumerable<ProfileDTO> GetListedProfile(int ProfileID)
        {
            IEnumerable<ProfileDTO> result = Mapper.Map<IEnumerable<ProfileDTO>>(DataLayer.DataLayerFacade.ProfileRepository().getListed(ProfileID));
            return result;
        }

        public ProfileDTO GetProfile(string ProfileEmail)
        {
            ProfileDTO result = Mapper.Map<ProfileDTO>(DataLayer.DataLayerFacade.ProfileRepository().GetProfile(ProfileEmail));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProfileEmail"></param>
        /// <returns>IEnumerable<ProfileDTO></returns>
        public IEnumerable<ProfileDTO> GetProfilesAdministration(string ProfileEmail)
        {
            IEnumerable<ProfileDTO> result = Mapper.Map<IEnumerable<ProfileDTO>>(DataLayer.DataLayerFacade.ProfileRepository().GetProfiles(ProfileEmail));
            return result;
        }

        public int[] GetProfilesAdministration(int CountryID)
        {
            int[] result = DataLayer.DataLayerFacade.ProfileRepository().GetAdministrationProfiles(CountryID);
            return result;
        }

        public DTResult<ProfileDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileRepository().GetProfilesAllAsQueryable().Where(m => m.SubscriptionTypeID != 4);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Profile>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileDTO> result = new DTResult<ProfileDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public DTResult<ProfileDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int CountryID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileRepository().GetProfilesAllAsQueryable().Where(m => m.Addresses.Any(a => a.City.CountryID == CountryID));

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Profile>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileDTO> result = new DTResult<ProfileDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<ProfileDTO> GetProfiles(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileRepository().GetProfilesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Profile>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileDTO> result = new PaginationSet<ProfileDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<ProfileDTO> GetProfiles()
        {
            var basequery = Mapper.Map<List<ProfileDTO>>(DataLayer.DataLayerFacade.ProfileRepository().GetProfilesAllAsQueryable().ToList());
            return basequery;
        }

        /// <summary>
        /// Get Profiles filter by CountryID, need changes to be lighter ;)
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public List<ProfileDTO> GetProfiles(int CountryID)
        {
            var basequery = Mapper.Map<List<ProfileDTO>>(DataLayer.DataLayerFacade.ProfileRepository().GetProfilesAllAsQueryable(CountryID).ToList());
            return basequery;
        }

        public DTResult<ProfileDTO> FilterResultByIndustryID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int IndustryID)
        {
            //Edit because of Industry
            var basequery = DataLayer.DataLayerFacade.ProfileRepository().GetProfilesAllAsQueryable();//.Where(m=> m.IndustryID==IndustryID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Profile>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileDTO> result = new DTResult<ProfileDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ProfileDTO> GetProfilesByIndustryID(int IndustryID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ProfileRepository().GetProfilesByIndustryIDAsQueryable(IndustryID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Profile>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileDTO> result = new PaginationSet<ProfileDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileDTO>>(queryList.ToList())
            };

            return result;
        }
        public DTResult<ProfileDTO> FilterResultByProfileTypeID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileTypeID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileRepository().GetProfilesAllAsQueryable().Where(m => m.ProfileTypeID == ProfileTypeID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Profile>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileDTO> result = new DTResult<ProfileDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ProfileDTO> GetProfilesByProfileTypeID(int ProfileTypeID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ProfileRepository().GetProfilesByProfileTypeIDAsQueryable(ProfileTypeID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Profile>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileDTO> result = new PaginationSet<ProfileDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileDTO>>(queryList.ToList())
            };

            return result;
        }
        public DTResult<ProfileDTO> FilterResultByThemeID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ThemeID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileRepository().GetProfilesAllAsQueryable().Where(m => m.ThemeID == ThemeID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Profile>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileDTO> result = new DTResult<ProfileDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ProfileDTO> GetProfilesByThemeID(int ThemeID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ProfileRepository().GetProfilesByThemeIDAsQueryable(ThemeID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Profile>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileDTO> result = new PaginationSet<ProfileDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileDTO>>(queryList.ToList())
            };

            return result;
        }
        public DTResult<ProfileDTO> FilterResultBySubscriptionTypeID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int SubscriptionTypeID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileRepository().GetProfilesAllAsQueryable().Where(m => m.SubscriptionTypeID == SubscriptionTypeID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Profile>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileDTO> result = new DTResult<ProfileDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ProfileDTO> GetProfilesBySubscriptionTypeID(int SubscriptionTypeID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ProfileRepository().GetProfilesBySubscriptionTypeIDAsQueryable(SubscriptionTypeID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Profile>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileDTO> result = new PaginationSet<ProfileDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileDTO>>(queryList.ToList())
            };

            return result;
        }

        public ProfileDTO Insert(ProfileDTO objProfile)
        {
            ProfileDTO result = Mapper.Map<ProfileDTO>(DataLayer.DataLayerFacade.ProfileRepository().Insert(objProfile.ProfileFullname, objProfile.ProfileFullnameEnglish, objProfile.ProfileIsPeople, objProfile.ProfileIsDeleted, objProfile.ProfileDateCreated, objProfile.ProfileDescription, objProfile.ProfileView, objProfile.ProfileLike, objProfile.ProfileURL, objProfile.ProfileShortDescription, objProfile.ProfileImage, objProfile.ProfileEmail, objProfile.ProfileStatus, objProfile.ProfileIsConfirmed, objProfile.ProfileTypeID, objProfile.ThemeID, objProfile.SubscriptionTypeID));

            if (result != null)
            {
                //Generate QR Code ;)
                Facade.WebFacade().GenerateQRCode(result.ProfileID);
                return result;
            }
            else
            {
                return new ProfileDTO();
            }
        }

        public ProfileDTO Insert(ProfileDTO objProfile, int UserID)
        {
            ProfileDTO result = Mapper.Map<ProfileDTO>(DataLayer.DataLayerFacade.ProfileRepository().Insert(objProfile.ProfileFullname, objProfile.ProfileFullnameEnglish, objProfile.ProfileIsPeople, objProfile.ProfileIsDeleted, objProfile.ProfileDateCreated, objProfile.ProfileDescription, objProfile.ProfileView, objProfile.ProfileLike, objProfile.ProfileURL, objProfile.ProfileShortDescription, objProfile.ProfileImage, objProfile.ProfileEmail, objProfile.ProfileStatus, objProfile.ProfileIsConfirmed, objProfile.ProfileTypeID, objProfile.ThemeID, objProfile.SubscriptionTypeID, UserID));

            if (result != null)
            {
                //Generate QR Code ;)
                //Fix it ;)
                //Facade.WebFacade().GenerateQRCode(result.ProfileID);
                return result;
            }
            else
            {
                return new ProfileDTO();
            }
        }

        public ProfileDTO Insert(Profile objProfile)
        {
            ProfileDTO result = Mapper.Map<ProfileDTO>(DataLayer.DataLayerFacade.ProfileRepository().Insert(objProfile));

            if (result != null)
            {
                //Generate QR Code ;)
                //Facade.WebFacade().GenerateQRCode(result.ProfileID);
                return result;
            }
            else
            {
                return new ProfileDTO();
            }
        }

        public bool Delete(int ProfileID)
        {
            bool result = false;
            try
            {
                result = DataLayer.DataLayerFacade.ProfileRepository().Delete(ProfileID);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool Update(int ProfileID, ProfileDTO objProfile)
        {
            bool result = DataLayer.DataLayerFacade.ProfileRepository().Update(objProfile.ProfileID, objProfile.ProfileFullname, objProfile.ProfileFullnameEnglish, objProfile.ProfileIsPeople, objProfile.ProfileIsDeleted, objProfile.ProfileDateCreated, objProfile.ProfileDescription, objProfile.ProfileView, objProfile.ProfileLike, objProfile.ProfileURL, objProfile.ProfileShortDescription, objProfile.ProfileImage, objProfile.ProfileEmail, objProfile.ProfileStatus, objProfile.ProfileIsConfirmed, objProfile.ProfileTypeID, objProfile.ThemeID, objProfile.SubscriptionTypeID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Business.DataTransferObjects.Mobile.ProfileUpdateModel profileUpdateModel)
        {
            bool result = DataLayer.DataLayerFacade.ProfileRepository().Update(profileUpdateModel.ProfileID, profileUpdateModel.ProfileFullname);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileID, bool Status, int UpdateMode)
        {
            bool result = DataLayer.DataLayerFacade.ProfileRepository().Update(ProfileID, Status, UpdateMode);
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