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
    public class ActivityTypeRepository : IActivityTypeRepository, IDisposable
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
        public ActivityType Insert(String ActivityTypeName, String ActivityTypeLanguageCode, Boolean ActivityTypeStatus, int? ActivityTypeParentID)
        {
            try
            {
                ActivityType obj = new ActivityType();


                obj.ActivityTypeName = ActivityTypeName;
                obj.ActivityTypeLanguageCode = ActivityTypeLanguageCode;
                obj.ActivityTypeStatus = ActivityTypeStatus;
                if (ActivityTypeParentID.HasValue)
                {
                    obj.ActivityTypeParentID = ActivityTypeParentID.Value;
                } else
                {
                    obj.ActivityTypeParentID = null;
                }
                
                context.ActivityTypes.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new ActivityType();
            }
        }

        public bool Update(int ActivityTypeID, String ActivityTypeName, String ActivityTypeLanguageCode, Boolean ActivityTypeStatus, int? ActivityTypeParentID)
        {
            ActivityType obj = new ActivityType();
            obj = (from w in context.ActivityTypes where w.ActivityTypeID == ActivityTypeID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ActivityTypes.Attach(obj);

                obj.ActivityTypeName = ActivityTypeName;
                obj.ActivityTypeLanguageCode = ActivityTypeLanguageCode;
                obj.ActivityTypeStatus = ActivityTypeStatus;
                if (ActivityTypeParentID.HasValue)
                {
                    obj.ActivityTypeParentID = ActivityTypeParentID.Value;
                }
                else
                {
                    obj.ActivityTypeParentID = null;
                }

                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int ActivityTypeID)
        {
            ActivityType obj = new ActivityType();
            obj = (from w in context.ActivityTypes where w.ActivityTypeID == ActivityTypeID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ActivityTypes.Attach(obj);
                context.ActivityTypes.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActivityType GetActivityType(int ActivityTypeID)
        {
            return (from w in context.ActivityTypes where w.ActivityTypeID == ActivityTypeID select w).FirstOrDefault();
        }

        //Lists
        public List<ActivityType> GetActivityTypesAll()
        {
            return (from w in context.ActivityTypes orderby w.ActivityTypeID descending select w).ToList();
        }
        public List<ActivityType> GetActivityTypesAll(bool ActivityTypeStatus)
        {
            return (from w in context.ActivityTypes where w.ActivityTypeStatus == ActivityTypeStatus orderby w.ActivityTypeID descending select w).ToList();
        }
        //List Pagings
        public List<ActivityType> GetActivityTypesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ActivityType>();
            if (filter != null)
            {
                xList = (from w in context.ActivityTypes orderby w.ActivityTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ActivityTypes orderby w.ActivityTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<ActivityType> GetActivityTypesAll(bool ActivityTypeStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ActivityType>();
            if (filter != null)
            {
                xList = (from w in context.ActivityTypes where w.ActivityTypeStatus == ActivityTypeStatus orderby w.ActivityTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ActivityTypes where w.ActivityTypeStatus == ActivityTypeStatus orderby w.ActivityTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable string languageCode
        public IQueryable<ActivityType> GetActivityTypesAllAsQueryable()
        {
            return (from w in context.ActivityTypes orderby w.ActivityTypeID descending select w).AsQueryable();
        }

        public IQueryable<Business.DataTransferObjects.ActivityTypeDTO> GetActivityTypesAllAsQueryable(string languageCode)
        {
            return (from w in context.ActivityTypes orderby w.ActivityTypeID descending select new Business.DataTransferObjects.ActivityTypeDTO { ActivityTypeID = w.ActivityTypeID, ActivityTypeName = w.ActivityTypeName, Multilanguage = w.ActivityTypeChilds.Where(m=> m.ActivityTypeLanguageCode == languageCode).FirstOrDefault().ActivityTypeName }).AsQueryable();
        }

        public IQueryable<ActivityType> GetActivityTypesAllAsQueryable(bool ActivityTypeStatus)
        {
            return (from w in context.ActivityTypes where w.ActivityTypeStatus == ActivityTypeStatus orderby w.ActivityTypeID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<ActivityType> GetActivityTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ActivityType> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ActivityTypes orderby w.ActivityTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ActivityTypes.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ActivityTypes orderby w.ActivityTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ActivityTypes.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<ActivityType> GetActivityTypesAllAsQueryable(bool ActivityTypeStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ActivityType> xList;
            if (filter != null)
            {
                xList = (from w in context.ActivityTypes where w.ActivityTypeStatus == ActivityTypeStatus orderby w.ActivityTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.ActivityTypes where w.ActivityTypeStatus == ActivityTypeStatus orderby w.ActivityTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<ActivityType> GetActivityTypesByActivityTypeParentID(int ActivityTypeParentID, bool ActivityTypeStatus)
        {
            return (from w in context.ActivityTypes where w.ActivityTypeParentID == ActivityTypeParentID && w.ActivityTypeStatus == ActivityTypeStatus orderby w.ActivityTypeID descending select w).ToList();
        }
        public List<ActivityType> GetActivityTypesByActivityTypeParentID(int ActivityTypeParentID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ActivityTypes where w.ActivityTypeParentID == ActivityTypeParentID orderby w.ActivityTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ActivityType> GetActivityTypesByActivityTypeParentID(int ActivityTypeParentID, bool ActivityTypeStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ActivityTypes where w.ActivityTypeParentID == ActivityTypeParentID && w.ActivityTypeStatus == ActivityTypeStatus orderby w.ActivityTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<ActivityType> GetActivityTypesByActivityTypeParentIDAsQueryable(int ActivityTypeParentID)
        {
            return (from w in context.ActivityTypes where w.ActivityTypeParentID == ActivityTypeParentID orderby w.ActivityTypeID descending select w).AsQueryable();
        }
        public IQueryable<ActivityType> GetActivityTypesByActivityTypeParentIDAsQueryable(int ActivityTypeParentID, bool ActivityTypeStatus)
        {
            return (from w in context.ActivityTypes where w.ActivityTypeParentID == ActivityTypeParentID && w.ActivityTypeStatus == ActivityTypeStatus orderby w.ActivityTypeID descending select w).AsQueryable();
        }
        public IQueryable<ActivityType> GetActivityTypesByActivityTypeParentIDAsQueryable(int ActivityTypeParentID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ActivityTypes where w.ActivityTypeParentID == ActivityTypeParentID orderby w.ActivityTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ActivityType> GetActivityTypesByActivityTypeParentIDAsQueryable(int ActivityTypeParentID, bool ActivityTypeStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ActivityTypes where w.ActivityTypeParentID == ActivityTypeParentID && w.ActivityTypeStatus == ActivityTypeStatus orderby w.ActivityTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}