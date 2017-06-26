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
    public class ProfileTagRepository : IProfileTagRepository, IDisposable
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
        public ProfileTag Insert(int TagID, int ProfileID, Boolean ProfileTagStatus)
        {
            try
            {
                ProfileTag obj = new ProfileTag();
                obj.TagID = TagID;
                obj.ProfileID = ProfileID;
                obj.ProfileTagStatus = ProfileTagStatus;
                context.ProfileTags.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new ProfileTag();
            }
        }


        public bool Update(int TagID, int ProfileID, Boolean ProfileTagStatus)
        {
            ProfileTag obj = new ProfileTag();
            obj = (from w in context.ProfileTags where w.TagID == TagID && w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileTags.Attach(obj);
                obj.ProfileTagStatus = ProfileTagStatus;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int TagID, int ProfileID)
        {
            ProfileTag obj = new ProfileTag();
            obj = (from w in context.ProfileTags where w.TagID == TagID && w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileTags.Attach(obj);
                context.ProfileTags.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ProfileTag GetProfileTag(int TagID, int ProfileID)
        {
            return (from w in context.ProfileTags where w.TagID == TagID && w.ProfileID == ProfileID select w).FirstOrDefault();
        }

        //Lists
        public List<ProfileTag> GetProfileTagsAll()
        {
            return (from w in context.ProfileTags orderby w.TagID, w.ProfileID descending select w).ToList();
        }
        public List<ProfileTag> GetProfileTagsAll(bool ProfileTagStatus)
        {
            return (from w in context.ProfileTags where w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).ToList();
        }
        //List Pagings
        public List<ProfileTag> GetProfileTagsAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ProfileTag>();
            if (filter != null)
            {
                xList = (from w in context.ProfileTags orderby w.TagID, w.ProfileID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ProfileTags orderby w.TagID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<ProfileTag> GetProfileTagsAll(bool ProfileTagStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ProfileTag>();
            if (filter != null)
            {
                xList = (from w in context.ProfileTags where w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ProfileTags where w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<ProfileTag> GetProfileTagsAllAsQueryable()
        {
            return (from w in context.ProfileTags.Include("Profile").Include("Tag") orderby w.TagID, w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<ProfileTag> GetProfileTagsAllAsQueryable(bool ProfileTagStatus)
        {
            return (from w in context.ProfileTags where w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<ProfileTag> GetProfileTagsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ProfileTag> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileTags orderby w.ProfileTagID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileTags.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileTags orderby w.ProfileTagID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileTags.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<ProfileTag> GetProfileTagsAllAsQueryable(bool ProfileTagStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ProfileTag> xList;
            if (filter != null)
            {
                xList = (from w in context.ProfileTags where w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.ProfileTags where w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<ProfileTag> GetProfileTagsByTagID(int TagID, bool ProfileTagStatus)
        {
            return (from w in context.ProfileTags where w.TagID == TagID && w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).ToList();
        }
        public List<ProfileTag> GetProfileTagsByTagID(int TagID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileTags where w.TagID == TagID orderby w.TagID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileTag> GetProfileTagsByTagID(int TagID, bool ProfileTagStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileTags where w.TagID == TagID && w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileTag> GetProfileTagsByProfileID(int ProfileID, bool ProfileTagStatus)
        {
            return (from w in context.ProfileTags where w.ProfileID == ProfileID && w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).ToList();
        }
        public List<ProfileTag> GetProfileTagsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileTags where w.ProfileID == ProfileID orderby w.TagID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileTag> GetProfileTagsByProfileID(int ProfileID, bool ProfileTagStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileTags where w.ProfileID == ProfileID && w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<ProfileTag> GetProfileTagsByTagIDAsQueryable(int TagID)
        {
            return (from w in context.ProfileTags where w.TagID == TagID orderby w.TagID, w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<ProfileTag> GetProfileTagsByTagIDAsQueryable(int TagID, bool ProfileTagStatus)
        {
            return (from w in context.ProfileTags where w.TagID == TagID && w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<ProfileTag> GetProfileTagsByTagIDAsQueryable(int TagID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileTags where w.TagID == TagID orderby w.TagID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileTag> GetProfileTagsByTagIDAsQueryable(int TagID, bool ProfileTagStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileTags where w.TagID == TagID && w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileTag> GetProfileTagsByProfileIDAsQueryable(int ProfileID)
        {
            return (from w in context.ProfileTags.Include("Tag") where w.ProfileID == ProfileID orderby w.TagID, w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<ProfileTag> GetProfileTagsByProfileIDAsQueryable(int ProfileID, bool ProfileTagStatus)
        {
            return (from w in context.ProfileTags where w.ProfileID == ProfileID && w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<ProfileTag> GetProfileTagsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileTags where w.ProfileID == ProfileID orderby w.TagID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileTag> GetProfileTagsByProfileIDAsQueryable(int ProfileID, bool ProfileTagStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileTags where w.ProfileID == ProfileID && w.ProfileTagStatus == ProfileTagStatus orderby w.TagID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}