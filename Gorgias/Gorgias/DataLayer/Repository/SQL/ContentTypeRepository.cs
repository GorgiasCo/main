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
    public class ContentTypeRepository : IContentTypeRepository, IDisposable
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
        public ContentType Insert(String ContentTypeName, int ContentTypeOrder, Boolean ContentTypeStatus, String ContentTypeLanguageCode, String ContentTypeExpression, int? ContentTypeParentID)
        {
            try
            {
                ContentType obj = new ContentType();

                obj.ContentTypeName = ContentTypeName;
                obj.ContentTypeOrder = ContentTypeOrder;
                obj.ContentTypeStatus = ContentTypeStatus;
                obj.ContentTypeLanguageCode = ContentTypeLanguageCode;
                obj.ContentTypeExpression = ContentTypeExpression;

                if (ContentTypeParentID.HasValue)
                {
                    obj.ContentTypeParentID = ContentTypeParentID;
                } else
                {
                    obj.ContentTypeParentID = null;
                }
                
                context.ContentTypes.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new ContentType();
            }
        }

        public bool Update(int ContentTypeID, String ContentTypeName, int ContentTypeOrder, Boolean ContentTypeStatus, String ContentTypeLanguageCode, String ContentTypeExpression, int? ContentTypeParentID)
        {
            ContentType obj = new ContentType();
            obj = (from w in context.ContentTypes where w.ContentTypeID == ContentTypeID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ContentTypes.Attach(obj);

                obj.ContentTypeName = ContentTypeName;
                obj.ContentTypeOrder = ContentTypeOrder;
                obj.ContentTypeStatus = ContentTypeStatus;
                obj.ContentTypeLanguageCode = ContentTypeLanguageCode;
                obj.ContentTypeExpression = ContentTypeExpression;

                if (ContentTypeParentID.HasValue)
                {
                    obj.ContentTypeParentID = ContentTypeParentID;
                }
                else
                {
                    obj.ContentTypeParentID = null;
                }

                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int ContentTypeID)
        {
            ContentType obj = new ContentType();
            obj = (from w in context.ContentTypes where w.ContentTypeID == ContentTypeID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ContentTypes.Attach(obj);
                context.ContentTypes.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ContentType GetContentType(int ContentTypeID)
        {
            return (from w in context.ContentTypes where w.ContentTypeID == ContentTypeID select w).FirstOrDefault();
        }

        //Lists
        public List<ContentType> GetContentTypesAll()
        {
            return (from w in context.ContentTypes orderby w.ContentTypeID descending select w).ToList();
        }
        public List<ContentType> GetContentTypesAll(bool ContentTypeStatus)
        {
            return (from w in context.ContentTypes where w.ContentTypeStatus == ContentTypeStatus orderby w.ContentTypeID descending select w).ToList();
        }
        //List Pagings
        public List<ContentType> GetContentTypesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ContentType>();
            if (filter != null)
            {
                xList = (from w in context.ContentTypes orderby w.ContentTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ContentTypes orderby w.ContentTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<ContentType> GetContentTypesAll(bool ContentTypeStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ContentType>();
            if (filter != null)
            {
                xList = (from w in context.ContentTypes where w.ContentTypeStatus == ContentTypeStatus orderby w.ContentTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ContentTypes where w.ContentTypeStatus == ContentTypeStatus orderby w.ContentTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<ContentType> GetContentTypesAllAsQueryable()
        {
            return (from w in context.ContentTypes orderby w.ContentTypeID descending select w).AsQueryable();
        }
        public IQueryable<ContentType> GetContentTypesAllAsQueryable(bool ContentTypeStatus)
        {
            return (from w in context.ContentTypes where w.ContentTypeStatus == ContentTypeStatus orderby w.ContentTypeID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<ContentType> GetContentTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ContentType> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ContentTypes orderby w.ContentTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ContentTypes.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ContentTypes orderby w.ContentTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ContentTypes.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<ContentType> GetContentTypesAllAsQueryable(bool ContentTypeStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ContentType> xList;
            if (filter != null)
            {
                xList = (from w in context.ContentTypes where w.ContentTypeStatus == ContentTypeStatus orderby w.ContentTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.ContentTypes where w.ContentTypeStatus == ContentTypeStatus orderby w.ContentTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<ContentType> GetContentTypesByContentTypeParentID(int ContentTypeParentID, bool ContentTypeStatus)
        {
            return (from w in context.ContentTypes where w.ContentTypeParentID == ContentTypeParentID && w.ContentTypeStatus == ContentTypeStatus orderby w.ContentTypeID descending select w).ToList();
        }
        public List<ContentType> GetContentTypesByContentTypeParentID(int ContentTypeParentID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ContentTypes where w.ContentTypeParentID == ContentTypeParentID orderby w.ContentTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ContentType> GetContentTypesByContentTypeParentID(int ContentTypeParentID, bool ContentTypeStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ContentTypes where w.ContentTypeParentID == ContentTypeParentID && w.ContentTypeStatus == ContentTypeStatus orderby w.ContentTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<ContentType> GetContentTypesByContentTypeParentIDAsQueryable(int ContentTypeParentID)
        {
            return (from w in context.ContentTypes where w.ContentTypeParentID == ContentTypeParentID orderby w.ContentTypeID descending select w).AsQueryable();
        }
        public IQueryable<ContentType> GetContentTypesByContentTypeParentIDAsQueryable(int ContentTypeParentID, bool ContentTypeStatus)
        {
            return (from w in context.ContentTypes where w.ContentTypeParentID == ContentTypeParentID && w.ContentTypeStatus == ContentTypeStatus orderby w.ContentTypeID descending select w).AsQueryable();
        }
        public IQueryable<ContentType> GetContentTypesByContentTypeParentIDAsQueryable(int ContentTypeParentID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ContentTypes where w.ContentTypeParentID == ContentTypeParentID orderby w.ContentTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ContentType> GetContentTypesByContentTypeParentIDAsQueryable(int ContentTypeParentID, bool ContentTypeStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ContentTypes where w.ContentTypeParentID == ContentTypeParentID && w.ContentTypeStatus == ContentTypeStatus orderby w.ContentTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}