using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Gorgias;
using Gorgias.DataLayer.Interface;
using Gorgias.Infrastruture.EntityFramework;
using System.Linq;
using System.Data.Entity.Spatial;

namespace Gorgias.DataLayer.Repository.SQL
{
    public class ProfileActivityRepository : IProfileActivityRepository, IDisposable
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
        public ProfileActivity Insert(int ProfileID, int AlbumID, int ActivityTypeID, int ProfileActivityCount, bool ProfileActivityIsFirst)
        {
            try
            {
                ProfileActivity obj = new ProfileActivity();
                obj.ProfileID = ProfileID;
                obj.AlbumID = AlbumID;
                obj.ActivityTypeID = ActivityTypeID;
                obj.ProfileActivityCount = ProfileActivityCount;
                obj.ProfileActivityIsFirst = ProfileActivityIsFirst;
                obj.ProfileActivityDate = DateTime.UtcNow;
                context.ProfileActivities.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new ProfileActivity();
            }
        }

        public ProfileActivity Insert(int ProfileID, int AlbumID, int ActivityTypeID, int ProfileActivityCount, bool ProfileActivityIsFirst, string ProfileActivityShare)
        {
            try
            {
                ProfileActivity obj = new ProfileActivity();
                obj.ProfileID = ProfileID;
                obj.AlbumID = AlbumID;
                obj.ActivityTypeID = ActivityTypeID;
                obj.ProfileActivityCount = ProfileActivityCount;
                obj.ProfileActivityIsFirst = ProfileActivityIsFirst;
                obj.ProfileActivityDate = DateTime.UtcNow;

                obj.Share = new Share{ ProfileActivityShare = ProfileActivityShare };

                ProfileActivity shareActivity = (from x in context.ProfileActivities where x.Share.ProfileActivityShare.Equals(ProfileActivityShare) select x).FirstOrDefault();
                if(shareActivity != null)
                {
                    obj.ProfileActivityParent = shareActivity;
                }

                context.ProfileActivities.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new ProfileActivity();
            }
        }

        public ProfileActivity Insert(int ProfileID, int AlbumID, int ActivityTypeID, int ProfileActivityCount, bool ProfileActivityIsFirst, DbGeography ProfileActivityLocation)
        {
            try
            {
                ProfileActivity obj = new ProfileActivity();
                obj.ProfileID = ProfileID;
                obj.AlbumID = AlbumID;
                obj.ActivityTypeID = ActivityTypeID;
                obj.ProfileActivityCount = ProfileActivityCount;
                obj.ProfileActivityIsFirst = ProfileActivityIsFirst;
                obj.ProfileActivityDate = DateTime.UtcNow;

                obj.Location = new Location { ProfileActivityLocation  = ProfileActivityLocation };

                context.ProfileActivities.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new ProfileActivity();
            }
        }

        public bool Update(int ProfileID, int AlbumID, int ActivityTypeID, int ProfileActivityCount, bool ProfileActivityIsFirst)
        {
            ProfileActivity obj = new ProfileActivity();
            obj = (from w in context.ProfileActivities where w.ProfileID == ProfileID && w.AlbumID == AlbumID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileActivities.Attach(obj);

                obj.ActivityTypeID = ActivityTypeID;
                obj.ProfileActivityCount = ProfileActivityCount;
                obj.ProfileActivityIsFirst = ProfileActivityIsFirst;
                obj.ProfileActivityDate = DateTime.UtcNow;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int ProfileID, int AlbumID)
        {
            ProfileActivity obj = new ProfileActivity();
            obj = (from w in context.ProfileActivities where w.ProfileID == ProfileID && w.AlbumID == AlbumID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileActivities.Attach(obj);
                context.ProfileActivities.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ProfileActivity GetProfileActivity(int ProfileID, int AlbumID)
        {
            return (from w in context.ProfileActivities where w.ProfileID == ProfileID && w.AlbumID == AlbumID select w).FirstOrDefault();
        }

        //Lists
        public List<ProfileActivity> GetProfileActivitiesAll()
        {
            return (from w in context.ProfileActivities orderby w.ProfileID, w.AlbumID descending select w).ToList();
        }
        //List Pagings
        public List<ProfileActivity> GetProfileActivitiesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ProfileActivity>();
            if (filter != null)
            {
                xList = (from w in context.ProfileActivities orderby w.ProfileID, w.AlbumID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ProfileActivities orderby w.ProfileID, w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<ProfileActivity> GetProfileActivitiesAllAsQueryable()
        {
            return (from w in context.ProfileActivities orderby w.ProfileID, w.AlbumID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<ProfileActivity> GetProfileActivitiesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ProfileActivity> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileActivitys orderby w.ProfileActivityID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileActivities.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileActivities orderby w.ProfileActivityID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileActivities.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<ProfileActivity> GetProfileActivitiesByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileActivities where w.ProfileID == ProfileID orderby w.ProfileID, w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileActivity> GetProfileActivitiesByAlbumID(int AlbumID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileActivities where w.AlbumID == AlbumID orderby w.ProfileID, w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileActivity> GetProfileActivitiesByActivityTypeID(int ActivityTypeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileActivities where w.ActivityTypeID == ActivityTypeID orderby w.ProfileID, w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<ProfileActivity> GetProfileActivitiesByProfileIDAsQueryable(int ProfileID)
        {
            return (from w in context.ProfileActivities where w.ProfileID == ProfileID orderby w.ProfileID, w.AlbumID descending select w).AsQueryable();
        }
        public IQueryable<ProfileActivity> GetProfileActivitiesByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileActivities where w.ProfileID == ProfileID orderby w.ProfileID, w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileActivity> GetProfileActivitiesByAlbumIDAsQueryable(int AlbumID)
        {
            return (from w in context.ProfileActivities where w.AlbumID == AlbumID orderby w.ProfileID, w.AlbumID descending select w).AsQueryable();
        }
        public IQueryable<ProfileActivity> GetProfileActivitiesByAlbumIDAsQueryable(int AlbumID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileActivities where w.AlbumID == AlbumID orderby w.ProfileID, w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileActivity> GetProfileActivitiesByActivityTypeIDAsQueryable(int ActivityTypeID)
        {
            return (from w in context.ProfileActivities where w.ActivityTypeID == ActivityTypeID orderby w.ProfileID, w.AlbumID descending select w).AsQueryable();
        }
        public IQueryable<ProfileActivity> GetProfileActivitiesByActivityTypeIDAsQueryable(int ActivityTypeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileActivities where w.ActivityTypeID == ActivityTypeID orderby w.ProfileID, w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}