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
    public class AlbumTypeRepository : IAlbumTypeRepository, IDisposable
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
        public AlbumType Insert(int AlbumTypeID, String AlbumTypeName, Boolean AlbumTypeStatus, int AlbumTypeLimitation)
        {
            try
            {
                AlbumType obj = new AlbumType();
                obj.AlbumTypeID = AlbumTypeID;
                obj.AlbumTypeName = AlbumTypeName;
                obj.AlbumTypeStatus = AlbumTypeStatus;
                obj.AlbumTypeLimitation = AlbumTypeLimitation;
                context.AlbumTypes.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new AlbumType();
            }
        }

        public bool Update(int AlbumTypeID, String AlbumTypeName, Boolean AlbumTypeStatus, int AlbumTypeLimitation)
        {
            AlbumType obj = new AlbumType();
            obj = (from w in context.AlbumTypes where w.AlbumTypeID == AlbumTypeID select w).FirstOrDefault();
            if (obj != null)
            {
                context.AlbumTypes.Attach(obj);
                obj.AlbumTypeName = AlbumTypeName;
                obj.AlbumTypeStatus = AlbumTypeStatus;
                obj.AlbumTypeLimitation = AlbumTypeLimitation;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int AlbumTypeID)
        {
            AlbumType obj = new AlbumType();
            obj = (from w in context.AlbumTypes where w.AlbumTypeID == AlbumTypeID select w).FirstOrDefault();
            if (obj != null)
            {
                context.AlbumTypes.Attach(obj);
                context.AlbumTypes.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public AlbumType GetAlbumType(int AlbumTypeID)
        {
            return (from w in context.AlbumTypes where w.AlbumTypeID == AlbumTypeID select w).FirstOrDefault();
        }

        //Lists
        public List<AlbumType> GetAlbumTypesAll()
        {
            return (from w in context.AlbumTypes orderby w.AlbumTypeID descending select w).ToList();
        }
        public List<AlbumType> GetAlbumTypesAll(bool AlbumTypeStatus)
        {
            return (from w in context.AlbumTypes where w.AlbumTypeStatus == AlbumTypeStatus orderby w.AlbumTypeID descending select w).ToList();
        }
        //List Pagings
        public List<AlbumType> GetAlbumTypesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<AlbumType>();
            if (filter != null)
            {
                xList = (from w in context.AlbumTypes orderby w.AlbumTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.AlbumTypes orderby w.AlbumTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<AlbumType> GetAlbumTypesAll(bool AlbumTypeStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<AlbumType>();
            if (filter != null)
            {
                xList = (from w in context.AlbumTypes where w.AlbumTypeStatus == AlbumTypeStatus orderby w.AlbumTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.AlbumTypes where w.AlbumTypeStatus == AlbumTypeStatus orderby w.AlbumTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<AlbumType> GetAlbumTypesAllAsQueryable()
        {
            return (from w in context.AlbumTypes orderby w.AlbumTypeID descending select w).AsQueryable();
        }

        //V2 ;)
        public IQueryable<Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel> GetAlbumTypesAllAsQueryableByKeyValue(bool AlbumTypeStatus)
        {
            return (from w in context.AlbumTypes where w.AlbumTypeStatus == AlbumTypeStatus && w.AlbumTypeID != 0 orderby w.AlbumTypeID ascending select new Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel { KeyID = w.AlbumTypeID, KeyName = w.AlbumTypeName }).AsQueryable();
        }
        //End V2
        public IQueryable<AlbumType> GetAlbumTypesAllAsQueryable(bool AlbumTypeStatus)
        {
            return (from w in context.AlbumTypes where w.AlbumTypeStatus == AlbumTypeStatus orderby w.AlbumTypeID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<AlbumType> GetAlbumTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<AlbumType> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.AlbumTypes orderby w.AlbumTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.AlbumTypes.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.AlbumTypes orderby w.AlbumTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.AlbumTypes.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<AlbumType> GetAlbumTypesAllAsQueryable(bool AlbumTypeStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<AlbumType> xList;
            if (filter != null)
            {
                xList = (from w in context.AlbumTypes where w.AlbumTypeStatus == AlbumTypeStatus orderby w.AlbumTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.AlbumTypes where w.AlbumTypeStatus == AlbumTypeStatus orderby w.AlbumTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }

    }
}