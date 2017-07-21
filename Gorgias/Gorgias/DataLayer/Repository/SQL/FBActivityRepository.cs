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
    public class FBActivityRepository : IFBActivityRepository, IDisposable
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
        public FBActivity Insert(double FBActivityCount, DateTime FBActivityDate, int FBActivityType)
        {
            try
            {
                FBActivity obj = new FBActivity();
                obj.FBActivityCount = FBActivityCount;
                obj.FBActivityDate = FBActivityDate;
                obj.FBActivityType = FBActivityType;
                context.FBActivities.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new FBActivity();
            }
        }

        public bool Update(int FBActivityID, double FBActivityCount, DateTime FBActivityDate, int FBActivityType)
        {
            FBActivity obj = new FBActivity();
            obj = (from w in context.FBActivities where w.FBActivityID == FBActivityID select w).FirstOrDefault();
            if (obj != null)
            {
                context.FBActivities.Attach(obj);

                obj.FBActivityCount = FBActivityCount;
                obj.FBActivityDate = FBActivityDate;
                obj.FBActivityType = FBActivityType;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int FBActivityID)
        {
            FBActivity obj = new FBActivity();
            obj = (from w in context.FBActivities where w.FBActivityID == FBActivityID select w).FirstOrDefault();
            if (obj != null)
            {
                context.FBActivities.Attach(obj);
                context.FBActivities.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public FBActivity GetFBActivity(int FBActivityID)
        {
            return (from w in context.FBActivities where w.FBActivityID == FBActivityID select w).FirstOrDefault();
        }

        //Lists
        public List<FBActivity> GetFBActivitiesAll()
        {
            return (from w in context.FBActivities orderby w.FBActivityDate descending select w).ToList();
        }

        public Business.DataTransferObjects.Report.FBReport GetFBActivitiesAllCurrent()
        {
            int currentMonth = DateTime.UtcNow.Month;
            int currentDay = DateTime.UtcNow.Day - 1;
            return (from w in context.FBActivities where w.FBActivityDate.Month == currentMonth && w.FBActivityType == 5 && w.FBActivityDate.Day == currentDay orderby w.FBActivityDate descending select new Business.DataTransferObjects.Report.FBReport { TotalRevenue = w.FBActivityCount, CurrentDate = w.FBActivityDate}).FirstOrDefault();
        }

        public Business.DataTransferObjects.Report.FBReport GetFBActivitiesAllCurrentMonth()
        {
            int currentMonth = DateTime.UtcNow.Month;
            int currentDay = DateTime.UtcNow.Day;
            var result = (from w in context.FBActivities where w.FBActivityDate.Month == currentMonth && w.FBActivityType == 5 orderby w.FBActivityDate descending select w).Sum(m=> m.FBActivityCount);
            return new Business.DataTransferObjects.Report.FBReport { TotalRevenue = result, CurrentDate = DateTime.UtcNow };
        }

        public Business.DataTransferObjects.Report.FBReport GetFBActivitiesAllCurrentOverall()
        {            
            var result = (from w in context.FBActivities where w.FBActivityType == 5 orderby w.FBActivityDate descending select w).Sum(m => m.FBActivityCount);
            return new Business.DataTransferObjects.Report.FBReport { TotalRevenue = result, CurrentDate = DateTime.UtcNow };
        }

        //List Pagings
        public List<FBActivity> GetFBActivitiesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<FBActivity>();
            if (filter != null)
            {
                xList = (from w in context.FBActivities orderby w.FBActivityDate descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.FBActivities orderby w.FBActivityDate descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<FBActivity> GetFBActivitiesAllAsQueryable()
        {
            return (from w in context.FBActivities orderby w.FBActivityDate descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<FBActivity> GetFBActivitiesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<FBActivity> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.FBActivitys orderby w.FBActivityID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.FBActivities.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.FBActivities orderby w.FBActivityID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.FBActivities.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }

    }
}