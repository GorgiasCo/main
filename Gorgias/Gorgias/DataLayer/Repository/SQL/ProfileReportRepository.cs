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
	public class ProfileReportRepository : IProfileReportRepository, IDisposable
	{
     	// To detect redundant calls
		private bool disposedValue = false;

		private GorgiasEntities context = new GorgiasEntities();

		// IDisposable
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue) {
				if (!this.disposedValue) {
					if (disposing) {
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
		public ProfileReport Insert(int ProfileReportActivityCount, double ProfileReportRevenue, int ReportTypeID, int ProfileID, int RevenueID)
		{
            try{
                ProfileReport obj = new ProfileReport();	
                
                
            		obj.ProfileReportActivityCount = ProfileReportActivityCount;			
            		obj.ProfileReportRevenue = ProfileReportRevenue;			
            		obj.ReportTypeID = ReportTypeID;			
            		obj.ProfileID = ProfileID;			
            		obj.RevenueID = RevenueID;			
    			context.ProfileReports.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new ProfileReport();
            }
		}

		public bool Update(int ProfileReportID, int ProfileReportActivityCount, double ProfileReportRevenue, int ReportTypeID, int ProfileID, int RevenueID)
		{
		    ProfileReport obj = new ProfileReport();
            obj = (from w in context.ProfileReports where w.ProfileReportID == ProfileReportID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.ProfileReports.Attach(obj);
	
        		obj.ProfileReportActivityCount = ProfileReportActivityCount;			
        		obj.ProfileReportRevenue = ProfileReportRevenue;			
        		obj.ReportTypeID = ReportTypeID;			
        		obj.ProfileID = ProfileID;			
        		obj.RevenueID = RevenueID;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int ProfileReportID)
		{
			ProfileReport obj = new ProfileReport();
			obj = (from w in context.ProfileReports where  w.ProfileReportID == ProfileReportID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.ProfileReports.Attach(obj);
			    context.ProfileReports.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public ProfileReport GetProfileReport(int ProfileReportID)
		{
			return (from w in context.ProfileReports where  w.ProfileReportID == ProfileReportID  select w).FirstOrDefault();
		}

        //Lists
		public List<ProfileReport> GetProfileReportsAll()
		{
			return (from w in context.ProfileReports orderby w.ProfileReportID descending select w).ToList();
		}
        //List Pagings
        public List<ProfileReport> GetProfileReportsAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<ProfileReport>();
            if (filter != null)
            {
                xList = (from w in context.ProfileReports orderby w.ProfileReportID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.ProfileReports orderby w.ProfileReportID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
        //IQueryable
		public IQueryable<ProfileReport> GetProfileReportsAllAsQueryable()
		{
			return (from w in context.ProfileReports.Include("Profile").Include("ReportType") orderby w.ProfileReportID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<ProfileReport> GetProfileReportsAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<ProfileReport> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileReports orderby w.ProfileReportID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.ProfileReports.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileReports orderby w.ProfileReportID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.ProfileReports.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<ProfileReport> GetProfileReportsByReportTypeID(int ReportTypeID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ProfileReports where w.ReportTypeID == ReportTypeID orderby w.ProfileReportID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileReport> GetProfileReportsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ProfileReports where w.ProfileID == ProfileID orderby w.ProfileReportID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileReport> GetProfileReportsByRevenueID(int RevenueID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ProfileReports where w.RevenueID == RevenueID orderby w.ProfileReportID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<ProfileReport> GetProfileReportsByReportTypeIDAsQueryable(int ReportTypeID)
		{
			return (from w in context.ProfileReports where w.ReportTypeID == ReportTypeID orderby w.ProfileReportID descending select w).AsQueryable();
		}
        public IQueryable<ProfileReport> GetProfileReportsByReportTypeIDAsQueryable(int ReportTypeID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ProfileReports where w.ReportTypeID == ReportTypeID orderby w.ProfileReportID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileReport> GetProfileReportsByProfileIDAsQueryable(int ProfileID)
		{
			return (from w in context.ProfileReports where w.ProfileID == ProfileID orderby w.ProfileReportID descending select w).AsQueryable();
		}
        //Why all ;)
        public IEnumerable<ProfileReport> GetProfileReportsByProfileIDAsIEnumerable(int ProfileID)
        {
            return (from w in context.ProfileReports where w.ProfileID == ProfileID orderby w.ProfileReportID descending select w).AsEnumerable();
        }
        public IEnumerable<ProfileReport> GetProfileReportsByProfileIDAsIEnumerable(int ProfileID, int RevenueID)
        {
            return (from w in context.ProfileReports where w.ProfileID == ProfileID && w.RevenueID == RevenueID orderby w.ProfileReportID descending select w).AsEnumerable();
        }
        public IQueryable<ProfileReport> GetProfileReportsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ProfileReports where w.ProfileID == ProfileID orderby w.ProfileReportID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileReport> GetProfileReportsByRevenueIDAsQueryable(int RevenueID)
		{
			return (from w in context.ProfileReports where w.RevenueID == RevenueID orderby w.ProfileReportID descending select w).AsQueryable();
		}
        public IQueryable<ProfileReport> GetProfileReportsByRevenueIDAsQueryable(int RevenueID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ProfileReports where w.RevenueID == RevenueID orderby w.ProfileReportID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

	}
}