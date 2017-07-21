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
    public class RevenueRepository : IRevenueRepository, IDisposable
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
        public Revenue Insert(DateTime RevenueDateCreated, double RevenueAmount, Int64 RevenueTotalViews, double RevenueMemberShare)
        {
            try
            {
                Revenue obj = new Revenue();
                obj.RevenueDateCreated = RevenueDateCreated;
                obj.RevenueAmount = RevenueAmount;
                obj.RevenueTotalViews = RevenueTotalViews;
                obj.RevenueMemberShare = RevenueMemberShare;
                context.Revenues.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Revenue();
            }
        }

        public bool Update(int RevenueID, DateTime RevenueDateCreated, double RevenueAmount, Int64 RevenueTotalViews, double RevenueMemberShare)
        {
            Revenue obj = new Revenue();
            obj = (from w in context.Revenues where w.RevenueID == RevenueID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Revenues.Attach(obj);

                obj.RevenueDateCreated = RevenueDateCreated;
                obj.RevenueAmount = RevenueAmount;
                obj.RevenueTotalViews = RevenueTotalViews;
                obj.RevenueMemberShare = RevenueMemberShare;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int RevenueID)
        {
            Revenue obj = new Revenue();
            obj = (from w in context.Revenues where w.RevenueID == RevenueID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Revenues.Attach(obj);
                context.Revenues.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Revenue GetRevenue(int RevenueID)
        {
            return (from w in context.Revenues where w.RevenueID == RevenueID select w).FirstOrDefault();
        }

        public Revenue GetRevenueCurrent()
        {
            int currentDay = DateTime.UtcNow.Day - 1;
            int currentMonth = DateTime.UtcNow.Month;
            int currentYear = DateTime.UtcNow.Year;
            var result = (from w in context.Revenues.Include("ProfileReports") where w.RevenueDateCreated.Day == currentDay && w.RevenueDateCreated.Month == currentMonth && w.RevenueDateCreated.Year == currentYear select w).First();
            if (result.ProfileReports.Count != 0)
            {
                throw new Exception("The Revenue and Profile Report was done before for " + DateTime.UtcNow);
            }
            else
            {
                return result;
            }
        }

        public Revenue GetRevenuePreviousDay()
        {
            int currentDay = DateTime.UtcNow.Day - 1;
            int currentMonth = DateTime.UtcNow.Month;
            int currentYear = DateTime.UtcNow.Year;
            var result = (from w in context.Revenues.Include("ProfileReports") where w.RevenueDateCreated.Day == currentDay && w.RevenueDateCreated.Month == currentMonth && w.RevenueDateCreated.Year == currentYear select w).First();
            return result;
        }

        public Revenue GetRevenueCurrentReport()
        {
            int currentDay = DateTime.UtcNow.Day - 1;
            int currentMonth = DateTime.UtcNow.Month;
            int currentYear = DateTime.UtcNow.Year;
            //var result = (from w in context.Revenues.Include("ProfileReports") orderby w.RevenueID descending select w).First();
            var result = (from w in context.Revenues.Include("ProfileReports") where w.RevenueDateCreated.Day == currentDay && w.RevenueDateCreated.Month == currentMonth && w.RevenueDateCreated.Year == currentYear orderby w.RevenueID descending select w).First();

            return result;
        }

        public Revenue GetRevenueCurrentReportForProfileReport()
        {
            int currentDay = DateTime.UtcNow.Day;
            int currentMonth = DateTime.UtcNow.Month;
            int currentYear = DateTime.UtcNow.Year;
            //var result = (from w in context.Revenues.Include("ProfileReports") orderby w.RevenueID descending select w).First();
            var result = (from w in context.Revenues.Include("ProfileReports") where w.RevenueDateCreated.Day == currentDay && w.RevenueDateCreated.Month == currentMonth && w.RevenueDateCreated.Year == currentYear orderby w.RevenueID descending select w).First();

            return result;
        }


        //Lists
        public List<Revenue> GetRevenuesAll()
        {
            return (from w in context.Revenues orderby w.RevenueID descending select w).ToList();
        }
        //List Pagings
        public List<Revenue> GetRevenuesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Revenue>();
            if (filter != null)
            {
                xList = (from w in context.Revenues orderby w.RevenueID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Revenues orderby w.RevenueID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<Revenue> GetRevenuesAllAsQueryable()
        {
            return (from w in context.Revenues orderby w.RevenueID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<Revenue> GetRevenuesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Revenue> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Revenues orderby w.RevenueID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Revenues.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Revenues orderby w.RevenueID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Revenues.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }

    }
}