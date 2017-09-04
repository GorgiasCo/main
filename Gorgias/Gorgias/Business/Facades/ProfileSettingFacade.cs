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
    public class ProfileSettingFacade
    {
        public ProfileSettingDTO GetProfileSetting(int ProfileID)
        {
            ProfileSettingDTO result = Mapper.Map<ProfileSettingDTO>(DataLayer.DataLayerFacade.ProfileSettingRepository().GetProfileSetting(ProfileID));
            return result;
        }

        public DTResult<ProfileSettingDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileSettingRepository().GetProfileSettingsAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileLanguageApp.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileSetting>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileSettingDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileSettingDTO> result = new DTResult<ProfileSettingDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<ProfileSettingDTO> GetProfileSettings(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileSettingRepository().GetProfileSettingsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileSetting>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileSettingDTO> result = new PaginationSet<ProfileSettingDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileSettingDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<ProfileSettingDTO> GetProfileSettings()
        {
            var basequery = Mapper.Map<List<ProfileSettingDTO>>(DataLayer.DataLayerFacade.ProfileSettingRepository().GetProfileSettingsAllAsQueryable());
            return basequery.ToList();
        }



        public ProfileSettingDTO Insert(ProfileSettingDTO objProfileSetting)
        {
            ProfileSettingDTO result = Mapper.Map<ProfileSettingDTO>(DataLayer.DataLayerFacade.ProfileSettingRepository().Insert(objProfileSetting.ProfileID,objProfileSetting.ProfileLanguageApp, objProfileSetting.ProfileCityID, objProfileSetting.ProfileBirthday));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new ProfileSettingDTO();
            }
        }

        public bool Delete(int ProfileID)
        {
            bool result = DataLayer.DataLayerFacade.ProfileSettingRepository().Delete(ProfileID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileID, ProfileSettingDTO objProfileSetting)
        {
            bool result = DataLayer.DataLayerFacade.ProfileSettingRepository().Update(objProfileSetting.ProfileID, objProfileSetting.ProfileLanguageApp, objProfileSetting.ProfileCityID, objProfileSetting.ProfileBirthday);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //V2
        public bool Update(Business.DataTransferObjects.Mobile.V2.ProfileSettingMobileModel objProfileSetting)
        {
            bool result;
            if(objProfileSetting.ProfileCityID != null)
            {
                result = DataLayer.DataLayerFacade.ProfileSettingRepository().Update(objProfileSetting.ProfileID, objProfileSetting.ProfileLanguageApp, objProfileSetting.ProfileCityID.Value, objProfileSetting.ProfileBirthday.Value);
            }
            else
            {
                result = DataLayer.DataLayerFacade.ProfileSettingRepository().Update(objProfileSetting.ProfileID, objProfileSetting.ProfileLanguageApp);
            }
            
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