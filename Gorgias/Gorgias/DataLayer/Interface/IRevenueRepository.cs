using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IRevenueRepository
    {
        Revenue Insert(DateTime RevenueDateCreated, double RevenueAmount, Int64 RevenueTotalViews, double RevenueMemberShare);
        bool Update(int RevenueID, DateTime RevenueDateCreated, double RevenueAmount, Int64 RevenueTotalViews, double RevenueMemberShare);
        bool Delete(int RevenueID);

        Revenue GetRevenue(int RevenueID);
        Revenue GetRevenueCurrent();
        Revenue GetRevenuePreviousDay();
        Revenue GetRevenueCurrentReport();
        Revenue GetRevenueCurrentReportForProfileReport();

        //List
        List<Revenue> GetRevenuesAll();
        List<Revenue> GetRevenuesAll(int page = 1, int pageSize = 7, string filter = null);
        
        //IQueryable
        IQueryable<Revenue> GetRevenuesAllAsQueryable();
        IQueryable<Revenue> GetRevenuesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
    }
}


