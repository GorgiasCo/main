using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IProfileSettingRepository
    {
        ProfileSetting Insert(int ProfileID, String ProfileLanguageApp, int? ProfileCityID, DateTime? ProfileBirthday);
        bool Update(int ProfileID, String ProfileLanguageApp, int? ProfileCityID, DateTime? ProfileBirthday);
        bool Update(int ProfileID, String ProfileLanguageApp);
        bool Delete(int ProfileID);

        ProfileSetting GetProfileSetting(int ProfileID);

        //List
        List<ProfileSetting> GetProfileSettingsAll();
        List<ProfileSetting> GetProfileSettingsAll(int page = 1, int pageSize = 7, string filter = null);

        List<ProfileSetting> GetProfileSettingsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null);

        //IQueryable
        IQueryable<ProfileSetting> GetProfileSettingsAllAsQueryable();
        IQueryable<ProfileSetting> GetProfileSettingsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ProfileSetting> GetProfileSettingsByProfileIDAsQueryable(int ProfileID);
        IQueryable<ProfileSetting> GetProfileSettingsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null);
    }
}


