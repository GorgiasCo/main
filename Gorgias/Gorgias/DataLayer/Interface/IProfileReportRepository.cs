using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IProfileReportRepository
    {
    
    
        ProfileReport Insert(int ProfileReportActivityCount, double ProfileReportRevenue, int ReportTypeID, int ProfileID, int RevenueID);
        bool Update(int ProfileReportID, int ProfileReportActivityCount, double ProfileReportRevenue, int ReportTypeID, int ProfileID, int RevenueID);        
        bool Delete(int ProfileReportID);

        ProfileReport GetProfileReport(int ProfileReportID);

        //List
        List<ProfileReport> GetProfileReportsAll();
        List<ProfileReport> GetProfileReportsAll(int page = 1, int pageSize = 7, string filter=null);
        
        List<ProfileReport> GetProfileReportsByReportTypeID(int ReportTypeID, int page = 1, int pageSize = 7, string filter=null);       
        List<ProfileReport> GetProfileReportsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        List<ProfileReport> GetProfileReportsByRevenueID(int RevenueID, int page = 1, int pageSize = 7, string filter=null);       
        
        //IQueryable
        IQueryable<ProfileReport> GetProfileReportsAllAsQueryable();
        IQueryable<ProfileReport> GetProfileReportsAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<ProfileReport> GetProfileReportsByReportTypeIDAsQueryable(int ReportTypeID);
        IQueryable<ProfileReport> GetProfileReportsByReportTypeIDAsQueryable(int ReportTypeID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<ProfileReport> GetProfileReportsByProfileIDAsQueryable(int ProfileID);
        IQueryable<ProfileReport> GetProfileReportsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<ProfileReport> GetProfileReportsByRevenueIDAsQueryable(int RevenueID);
        IQueryable<ProfileReport> GetProfileReportsByRevenueIDAsQueryable(int RevenueID, int page = 1, int pageSize = 7, string filter=null);
        IEnumerable<ProfileReport> GetProfileReportsByProfileIDAsIEnumerable(int ProfileID);
    }
}


