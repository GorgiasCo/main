using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IProfileRepository
    {
        Profile Insert(String ProfileFullname, string ProfileFullnameEnglish, Boolean ProfileIsPeople, Boolean ProfileIsDeleted, DateTime ProfileDateCreated, String ProfileDescription, int ProfileView, int ProfileLike, String ProfileURL, String ProfileShortDescription, String ProfileImage, String ProfileEmail, Boolean ProfileStatus, Boolean ProfileIsConfirmed, int ProfileTypeID, int ThemeID, int SubscriptionTypeID);
        Profile Insert(String ProfileFullname, string ProfileFullnameEnglish, Boolean ProfileIsPeople, Boolean ProfileIsDeleted, DateTime ProfileDateCreated, String ProfileDescription, int ProfileView, int ProfileLike, String ProfileURL, String ProfileShortDescription, String ProfileImage, String ProfileEmail, Boolean ProfileStatus, Boolean ProfileIsConfirmed, int ProfileTypeID, int ThemeID, int SubscriptionTypeID, int UserID);
        Profile Insert(Profile obj);
        Profile Insert();
        Profile Insert(string DeviceLanguage, string DeviceID, string DeviceToken);
        bool Update(int ProfileID, String ProfileFullname, string ProfileFullnameEnglish, Boolean ProfileIsPeople, Boolean ProfileIsDeleted, DateTime ProfileDateCreated, String ProfileDescription, int ProfileView, int ProfileLike, String ProfileURL, String ProfileShortDescription, String ProfileImage, String ProfileEmail, Boolean ProfileStatus, Boolean ProfileIsConfirmed, int ProfileTypeID, int ThemeID, int SubscriptionTypeID);
        bool Update(int ProfileID, bool Status, int UpdateMode);
        bool Update(int ProfileID, string ProfileFullname);
        bool Update(int ProfileID, string ProfileFullname, string ProfileFullnameEnglish, string ProfileShortDescription, string ProfileEmail, int ProfileTypeID, int IndustryID, int CityID);
        bool Update(int ProfileID, string ProfileFullname, string ProfileFullnameEnglish, string ProfileShortDescription, string ProfileEmail, int ProfileTypeID, int IndustryID, int CityID, DateTime? ProfileBirthday, string ProfileLanguageApp);
        bool Update(int ProfileID, string ProfileFullname, string ProfileFullnameEnglish, string ProfileShortDescription, string ProfileEmail, int ProfileTypeID, int? IndustryID, string IndustryName, int CityID, DateTime? ProfileBirthday, string ProfileLanguageApp);
        bool Update(int ProfileID, string ProfileFullname, string ProfileEmail);
        int UpdateByUserID(int ProfileID, string ProfileFullname, string ProfileEmail);
        bool Delete(int ProfileID);

        Profile GetProfile(int ProfileID);
        string GetProfileFullname(int ProfileID);
        string GetProfileURL(int ProfileID);
        Business.DataTransferObjects.Mobile.V2.LoginProfileMobileModel GetProfileSetting(int ProfileID);
        Business.DataTransferObjects.Mobile.V2.LoginProfileMobileModel GetProfileSetting(int ProfileID, string languageCode);
        Profile GetV2Profile(int ProfileID);
        Business.DataTransferObjects.Mobile.V2.MiniProfileMobileModel GetV2MiniMobileProfile(int ProfileID, int RequestedProfileID, string languageCode);
        Business.DataTransferObjects.Mobile.V2.SettingProfileMobileModel GetV2SettingMobileProfile(int ProfileID, string languageCode);
        IEnumerable<Business.DataTransferObjects.Report.ProfileReport> GetProfileReportCurrent();
        Int64 GetProfileReportCurrentProfileViews();
        IList<Business.DataTransferObjects.Report.ProfileReport> GetProfileReportCurrent(int UserID);
        IList<Business.DataTransferObjects.Report.ProfileReport> GetProfileReportCurrentV2(int UserID);
        IList<Business.DataTransferObjects.Report.ProfileReport> GetProfileReportCurrentByCountry(int CountryID);
        IList<Business.DataTransferObjects.Report.ProfileReport> GetProfileReportCurrentByCountryV2(int CountryID);

        Business.DataTransferObjects.Web.AdminMiniProfile GetMiniProfile(int ProfileID);
        Business.DataTransferObjects.Web.LowAppProfileModel GetLowAppProfile(string ProfileURL);
        Business.DataTransferObjects.Mobile.V2.LoginAttempt getLoginAttempt(string ProfileEmail, int? ProfileID);
        Profile GetProfile(string ProfileEmail);
        int[] GetAdministrationProfiles(int CountryID);
        IEnumerable<Profile> GetProfiles(string ProfileEmail);
        IEnumerable<Profile> getListed(int ProfileID);

        //List
        List<Profile> GetProfilesAll();
        List<Profile> GetProfilesAll(bool ProfileStatus);
        List<Profile> GetProfilesAll(int page = 1, int pageSize = 7, string filter = null);
        List<Profile> GetProfilesAll(bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null);

        List<Profile> GetProfilesByIndustryID(int IndustryID, bool ProfileStatus);
        List<Profile> GetProfilesByIndustryID(int IndustryID, int page = 1, int pageSize = 7, string filter = null);
        List<Profile> GetProfilesByIndustryID(int IndustryID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null);
        List<Profile> GetProfilesByProfileTypeID(int ProfileTypeID, bool ProfileStatus);
        List<Profile> GetProfilesByProfileTypeID(int ProfileTypeID, int page = 1, int pageSize = 7, string filter = null);
        List<Profile> GetProfilesByProfileTypeID(int ProfileTypeID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null);
        List<Profile> GetProfilesByThemeID(int ThemeID, bool ProfileStatus);
        List<Profile> GetProfilesByThemeID(int ThemeID, int page = 1, int pageSize = 7, string filter = null);
        List<Profile> GetProfilesByThemeID(int ThemeID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null);
        List<Profile> GetProfilesBySubscriptionTypeID(int SubscriptionTypeID, bool ProfileStatus);
        List<Profile> GetProfilesBySubscriptionTypeID(int SubscriptionTypeID, int page = 1, int pageSize = 7, string filter = null);
        List<Profile> GetProfilesBySubscriptionTypeID(int SubscriptionTypeID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null);

        //IQueryable
        IQueryable<Profile> GetProfilesAllAsQueryable();
        IQueryable<Profile> GetProfilesAllAsQueryable(int CountryID);
        IQueryable<Profile> GetProfilesAllAsQueryable(bool ProfileStatus);
        IQueryable<Profile> GetProfilesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Profile> GetProfilesAllAsQueryable(bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Profile> GetProfilesByIndustryIDAsQueryable(int IndustryID);
        IQueryable<Profile> GetProfilesByIndustryIDAsQueryable(int IndustryID, bool ProfileStatus);
        IQueryable<Profile> GetProfilesByIndustryIDAsQueryable(int IndustryID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Profile> GetProfilesByIndustryIDAsQueryable(int IndustryID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Profile> GetProfilesByProfileTypeIDAsQueryable(int ProfileTypeID);
        IQueryable<Profile> GetProfilesByProfileTypeIDAsQueryable(int ProfileTypeID, bool ProfileStatus);
        IQueryable<Profile> GetProfilesByProfileTypeIDAsQueryable(int ProfileTypeID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Profile> GetProfilesByProfileTypeIDAsQueryable(int ProfileTypeID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Profile> GetProfilesByThemeIDAsQueryable(int ThemeID);
        IQueryable<Profile> GetProfilesByThemeIDAsQueryable(int ThemeID, bool ProfileStatus);
        IQueryable<Profile> GetProfilesByThemeIDAsQueryable(int ThemeID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Profile> GetProfilesByThemeIDAsQueryable(int ThemeID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Profile> GetProfilesBySubscriptionTypeIDAsQueryable(int SubscriptionTypeID);
        IQueryable<Profile> GetProfilesBySubscriptionTypeIDAsQueryable(int SubscriptionTypeID, bool ProfileStatus);
        IQueryable<Profile> GetProfilesBySubscriptionTypeIDAsQueryable(int SubscriptionTypeID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Profile> GetProfilesBySubscriptionTypeIDAsQueryable(int SubscriptionTypeID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


