using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Gorgias;
using Gorgias.DataLayer.Interface;
using Gorgias.Infrastruture.EntityFramework;
using System.Linq;
using EntityFramework.Extensions;

namespace Gorgias.DataLayer.Repository.SQL
{
    public class ContentRepository : IContentRepository, IDisposable
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
        public Content Insert(String ContentTitle, String ContentURL, int ContentType, Boolean ContentStatus, Boolean ContentIsDeleted, DateTime? ContentCreatedDate, int AlbumID)
        {
            try
            {
                Content obj = new Content();
                obj.ContentTitle = ContentTitle;
                obj.ContentURL = ContentURL;
                obj.ContentType = ContentType;
                obj.ContentStatus = ContentStatus;
                obj.ContentIsDeleted = ContentIsDeleted;
                if (ContentCreatedDate.HasValue)
                {
                    obj.ContentCreatedDate = ContentCreatedDate;
                } else
                {
                    obj.ContentCreatedDate = DateTime.Now;
                }
                obj.AlbumID = AlbumID;
                context.Contents.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Content();
            }
        }

        public bool Update(int ContentID, String ContentTitle, String ContentURL, int ContentType, Boolean ContentStatus, Boolean ContentIsDeleted, int AlbumID)
        {
            Content obj = new Content();
            obj = (from w in context.Contents where w.ContentID == ContentID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Contents.Attach(obj);

                obj.ContentTitle = ContentTitle;
                obj.ContentURL = ContentURL;
                obj.ContentType = ContentType;
                obj.ContentStatus = ContentStatus;
                obj.ContentIsDeleted = ContentIsDeleted;
                obj.AlbumID = AlbumID;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ContentID, int ContentLike)
        {
            Content obj = new Content();
            obj = (from w in context.Contents where w.ContentID == ContentID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Contents.Attach(obj);
                obj.ContentLike = obj.ContentLike + ContentLike;             
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Business.DataTransferObjects.Mobile.V2.ContentLikeMobileModel[] contents)
        {
            List<int> contentIDs = new List<int>();
            foreach (Business.DataTransferObjects.Mobile.V2.ContentLikeMobileModel obj in contents)
            {
                contentIDs.Add(obj.ContentID);
            }
            try {
                using (var db = context)
                {
                    var result = db.Contents.Where(w=> contentIDs.Contains(w.ContentID)).ToList();//.UpdateAsync(mu => new Content() { ContentLike = mu.ContentLike});
                    result.ForEach(m => m.ContentLike = m.ContentLike + contents.Where(w => w.ContentID == m.ContentID).First().ContentLikes);
                    //db.Contents.AddRange(result);
                    db.SaveChanges();
                }
                return true;
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }     

        public bool Delete(int ContentID)
        {
            Content obj = new Content();
            obj = (from w in context.Contents where w.ContentID == ContentID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Contents.Attach(obj);
                context.Contents.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Content GetContent(int ContentID)
        {
            return (from w in context.Contents where w.ContentID == ContentID select w).FirstOrDefault();
        }

        //Lists
        public List<Content> GetContentsAll()
        {
            return (from w in context.Contents orderby w.ContentID descending select w).ToList();
        }
        public List<Content> GetContentsAll(bool ContentStatus)
        {
            return (from w in context.Contents where w.ContentStatus == ContentStatus orderby w.ContentID descending select w).ToList();
        }
        //List Pagings
        public List<Content> GetContentsAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Content>();
            if (filter != null)
            {
                xList = (from w in context.Contents orderby w.ContentID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Contents orderby w.ContentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<Content> GetContentsAll(bool ContentStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Content>();
            if (filter != null)
            {
                xList = (from w in context.Contents where w.ContentStatus == ContentStatus orderby w.ContentID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Contents where w.ContentStatus == ContentStatus orderby w.ContentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<Content> GetContentsAllAsQueryable()
        {
            return (from w in context.Contents orderby w.ContentID descending select w).AsQueryable();
        }
        public IQueryable<Content> GetContentsAllAsQueryable(bool ContentStatus)
        {
            return (from w in context.Contents where w.ContentStatus == ContentStatus orderby w.ContentID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<Content> GetContentsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Content> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Contents orderby w.ContentID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Contents.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Contents orderby w.ContentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Contents.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<Content> GetContentsAllAsQueryable(bool ContentStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Content> xList;
            if (filter != null)
            {
                xList = (from w in context.Contents where w.ContentStatus == ContentStatus orderby w.ContentID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Contents where w.ContentStatus == ContentStatus orderby w.ContentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<Content> GetContentsByAlbumID(int AlbumID, bool ContentStatus)
        {
            return (from w in context.Contents where w.AlbumID == AlbumID && w.ContentStatus == ContentStatus orderby w.ContentID descending select w).ToList();
        }
        public List<Content> GetContentsByAlbumID(int AlbumID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Contents where w.AlbumID == AlbumID orderby w.ContentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Content> GetContentsByAlbumID(int AlbumID, bool ContentStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Contents where w.AlbumID == AlbumID && w.ContentStatus == ContentStatus orderby w.ContentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<Content> GetContentsByAlbumIDAsQueryable(int AlbumID)
        {
            return (from w in context.Contents where w.AlbumID == AlbumID orderby w.ContentID descending select w).AsQueryable();
        }
        public IQueryable<Content> GetContentsByAlbumIDAsQueryable(int AlbumID, bool ContentStatus)
        {
            return (from w in context.Contents where w.AlbumID == AlbumID && w.ContentStatus == ContentStatus orderby w.ContentID descending select w).AsQueryable();
        }
        public IQueryable<Content> GetContentsByAlbumIDAsQueryable(int AlbumID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Contents where w.AlbumID == AlbumID orderby w.ContentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Content> GetContentsByAlbumIDAsQueryable(int AlbumID, bool ContentStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Contents where w.AlbumID == AlbumID && w.ContentStatus == ContentStatus orderby w.ContentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}