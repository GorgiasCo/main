using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Gorgias;
using Gorgias.DataLayer.Interface;
using Gorgias.Infrastruture.EntityFramework;
using System.Linq;
namespace Gorgias.DataLayer.Repository.SQL
{
    public class ProfileSettingRepository : IProfileSettingRepository, IDisposable
    {
        // To detect redundant calls
        private bool disposedValue = false;

        private GorgiasEntities context = new GorgiasEntities();

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (!this.disposedValue)
                {
                    if (disposing)
                    {
                        context.Dispose();
                    }
                }
                this.disposedValue = true;
            }
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        //CRUD Functions
        public ProfileSetting Insert(int ProfileID, String ProfileLanguageApp, int? ProfileCityID, DateTime? ProfileBirthday)
        {
            try
            {
                ProfileSetting obj = new ProfileSetting();
                obj.ProfileLanguageApp = ProfileLanguageApp;
                obj.ProfileCityID = ProfileCityID;
                obj.ProfileBirthday = ProfileBirthday;
                obj.ProfileID = ProfileID;
                context.ProfileSettings.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new ProfileSetting();
            }
        }

        public bool Update(int ProfileID, String ProfileLanguageApp, int? ProfileCityID, DateTime? ProfileBirthday)
        {
            ProfileSetting obj = new ProfileSetting();
            obj = (from w in context.ProfileSettings where w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileSettings.Attach(obj);

                obj.ProfileLanguageApp = ProfileLanguageApp;
                obj.ProfileCityID = ProfileCityID;
                obj.ProfileBirthday = ProfileBirthday;
                context.SaveChanges();
                return true;
            }
            else
            {
                Insert(ProfileID, ProfileLanguageApp, ProfileCityID, ProfileBirthday);
                return true;
            }
        }

        public bool Update(int ProfileID, String ProfileLanguageApp)
        {
            ProfileSetting obj = new ProfileSetting();
            obj = (from w in context.ProfileSettings where w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileSettings.Attach(obj);
                obj.ProfileLanguageApp = ProfileLanguageApp;                
                context.SaveChanges();
                return true;
            }
            else
            {
                Insert(ProfileID, ProfileLanguageApp, null, null);
                return true;                
            }
        }

        public bool Delete(int ProfileID)
        {
            ProfileSetting obj = new ProfileSetting();
            obj = (from w in context.ProfileSettings where w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileSettings.Attach(obj);
                context.ProfileSettings.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ProfileSetting GetProfileSetting(int ProfileID)
        {
            return (from w in context.ProfileSettings where w.ProfileID == ProfileID select w).FirstOrDefault();
        }

        //Lists
        public List<ProfileSetting> GetProfileSettingsAll()
        {
            return (from w in context.ProfileSettings orderby w.ProfileID descending select w).ToList();
        }
        //List Pagings
        public List<ProfileSetting> GetProfileSettingsAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ProfileSetting>();
            if (filter != null)
            {
                xList = (from w in context.ProfileSettings orderby w.ProfileID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ProfileSettings orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<ProfileSetting> GetProfileSettingsAllAsQueryable()
        {
            return (from w in context.ProfileSettings orderby w.ProfileID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<ProfileSetting> GetProfileSettingsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ProfileSetting> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileSettings orderby w.ProfileSettingID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileSettings.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileSettings orderby w.ProfileSettingID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileSettings.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<ProfileSetting> GetProfileSettingsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileSettings where w.ProfileID == ProfileID orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<ProfileSetting> GetProfileSettingsByProfileIDAsQueryable(int ProfileID)
        {
            return (from w in context.ProfileSettings where w.ProfileID == ProfileID orderby w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<ProfileSetting> GetProfileSettingsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileSettings where w.ProfileID == ProfileID orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}