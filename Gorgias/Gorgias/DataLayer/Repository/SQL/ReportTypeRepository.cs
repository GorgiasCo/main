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
    public class ReportTypeRepository : IReportTypeRepository, IDisposable
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
        public ReportType Insert(int ReportTypeID, String ReportTypeName, Boolean ReportTypeIsCountable, Boolean ReportTypeStatus)
        {
            try
            {
                ReportType obj = new ReportType();
                obj.ReportTypeID = ReportTypeID;
                obj.ReportTypeName = ReportTypeName;
                obj.ReportTypeIsCountable = ReportTypeIsCountable;
                obj.ReportTypeStatus = ReportTypeStatus;
                context.ReportTypes.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Update(int ReportTypeID, String ReportTypeName, Boolean ReportTypeIsCountable, Boolean ReportTypeStatus)
        {
            ReportType obj = new ReportType();
            obj = (from w in context.ReportTypes where w.ReportTypeID == ReportTypeID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ReportTypes.Attach(obj);

                obj.ReportTypeName = ReportTypeName;
                obj.ReportTypeIsCountable = ReportTypeIsCountable;
                obj.ReportTypeStatus = ReportTypeStatus;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int ReportTypeID)
        {
            ReportType obj = new ReportType();
            obj = (from w in context.ReportTypes where w.ReportTypeID == ReportTypeID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ReportTypes.Attach(obj);
                context.ReportTypes.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ReportType GetReportType(int ReportTypeID)
        {
            return (from w in context.ReportTypes where w.ReportTypeID == ReportTypeID select w).FirstOrDefault();
        }

        //Lists
        public List<ReportType> GetReportTypesAll()
        {
            return (from w in context.ReportTypes orderby w.ReportTypeID descending select w).ToList();
        }
        public List<ReportType> GetReportTypesAll(bool ReportTypeStatus)
        {
            return (from w in context.ReportTypes where w.ReportTypeStatus == ReportTypeStatus orderby w.ReportTypeID descending select w).ToList();
        }
        //List Pagings
        public List<ReportType> GetReportTypesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ReportType>();
            if (filter != null)
            {
                xList = (from w in context.ReportTypes orderby w.ReportTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ReportTypes orderby w.ReportTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<ReportType> GetReportTypesAll(bool ReportTypeStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ReportType>();
            if (filter != null)
            {
                xList = (from w in context.ReportTypes where w.ReportTypeStatus == ReportTypeStatus orderby w.ReportTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ReportTypes where w.ReportTypeStatus == ReportTypeStatus orderby w.ReportTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<ReportType> GetReportTypesAllAsQueryable()
        {
            return (from w in context.ReportTypes orderby w.ReportTypeID descending select w).AsQueryable();
        }
        public IQueryable<ReportType> GetReportTypesAllAsQueryable(bool ReportTypeStatus)
        {
            return (from w in context.ReportTypes where w.ReportTypeStatus == ReportTypeStatus orderby w.ReportTypeID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<ReportType> GetReportTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ReportType> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ReportTypes orderby w.ReportTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ReportTypes.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ReportTypes orderby w.ReportTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ReportTypes.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<ReportType> GetReportTypesAllAsQueryable(bool ReportTypeStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ReportType> xList;
            if (filter != null)
            {
                xList = (from w in context.ReportTypes where w.ReportTypeStatus == ReportTypeStatus orderby w.ReportTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.ReportTypes where w.ReportTypeStatus == ReportTypeStatus orderby w.ReportTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }

    }
}