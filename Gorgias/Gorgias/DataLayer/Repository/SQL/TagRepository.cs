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
    public class TagRepository : ITagRepository, IDisposable
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
        public Tag Insert(String TagName, Boolean TagStatus, bool TagIsPrimary, int TagWeight)
        {
            try
            {
                Tag obj = new Tag();
                obj.TagIsPrimary = TagIsPrimary;
                obj.TagWeight = TagWeight;
                obj.TagName = TagName;
                obj.TagStatus = TagStatus;
                context.Tags.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Tag();
            }
        }

        public bool Update(int TagID, String TagName, Boolean TagStatus, bool TagIsPrimary, int TagWeight)
        {
            Tag obj = new Tag();
            obj = (from w in context.Tags where w.TagID == TagID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Tags.Attach(obj);
                obj.TagIsPrimary = TagIsPrimary;
                obj.TagWeight = TagWeight;
                obj.TagName = TagName;
                obj.TagStatus = TagStatus;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int TagID)
        {
            Tag obj = new Tag();
            obj = (from w in context.Tags where w.TagID == TagID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Tags.Attach(obj);
                context.Tags.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Tag GetTag(int TagID)
        {
            return (from w in context.Tags where w.TagID == TagID select w).FirstOrDefault();
        }

        public Tag GetTag(string TagName)
        {
            return (from w in context.Tags where w.TagName.Equals(TagName) && w.TagStatus == true select w).FirstOrDefault();
        }

        //Lists
        public List<Tag> GetTagsAll()
        {
            return (from w in context.Tags orderby w.TagID descending select w).ToList();
        }
        public List<Tag> GetTagsAll(bool TagStatus)
        {
            return (from w in context.Tags where w.TagStatus == TagStatus orderby w.TagID descending select w).ToList();
        }
        //List Pagings
        public List<Tag> GetTagsAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Tag>();
            if (filter != null)
            {
                xList = (from w in context.Tags orderby w.TagID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Tags orderby w.TagID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<Tag> GetTagsAll(bool TagStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Tag>();
            if (filter != null)
            {
                xList = (from w in context.Tags where w.TagStatus == TagStatus orderby w.TagID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Tags where w.TagStatus == TagStatus orderby w.TagID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<Tag> GetTagsAllAsQueryable()
        {
            return (from w in context.Tags orderby w.TagID descending select w).AsQueryable();
        }
        public IQueryable<Tag> GetTagsAllAsQueryable(bool TagStatus)
        {
            return (from w in context.Tags where w.TagStatus == TagStatus orderby w.TagID descending select w).AsQueryable();
        }
        public IQueryable<Tag> GetTagsAllAsQueryable(bool TagStatus, bool TagIsPrimary)
        {
            return (from w in context.Tags where w.TagStatus == TagStatus && w.TagIsPrimary == TagIsPrimary orderby w.TagID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<Tag> GetTagsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Tag> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Tags orderby w.TagID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Tags.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Tags orderby w.TagID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Tags.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<Tag> GetTagsAllAsQueryable(bool TagStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Tag> xList;
            if (filter != null)
            {
                xList = (from w in context.Tags where w.TagStatus == TagStatus orderby w.TagID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Tags where w.TagStatus == TagStatus orderby w.TagID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }

    }
}