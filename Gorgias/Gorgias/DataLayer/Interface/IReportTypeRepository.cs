using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IReportTypeRepository
    {
        ReportType Insert(int ReportTypeID, string ReportTypeName, bool ReportTypeIsCountable, bool ReportTypeStatus);
        bool Update(int ReportTypeID, string ReportTypeName, bool ReportTypeIsCountable, bool ReportTypeStatus);
        bool Delete(int ReportTypeID);

        ReportType GetReportType(int ReportTypeID);

        //List
        List<ReportType> GetReportTypesAll();
        List<ReportType> GetReportTypesAll(bool ReportTypeStatus);
        List<ReportType> GetReportTypesAll(int page = 1, int pageSize = 7, string filter = null);
        List<ReportType> GetReportTypesAll(bool ReportTypeStatus, int page = 1, int pageSize = 7, string filter = null);

        //IQueryable
        IQueryable<ReportType> GetReportTypesAllAsQueryable();
        IQueryable<ReportType> GetReportTypesAllAsQueryable(bool ReportTypeStatus);
        IQueryable<ReportType> GetReportTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ReportType> GetReportTypesAllAsQueryable(bool ReportTypeStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


