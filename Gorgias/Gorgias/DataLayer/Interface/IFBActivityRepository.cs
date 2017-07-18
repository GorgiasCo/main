using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IFBActivityRepository
    {
    
    
        FBActivity Insert(decimal FBActivityCount, DateTime FBActivityDate, int FBActivityType);
        bool Update(int FBActivityID, decimal FBActivityCount, DateTime FBActivityDate, int FBActivityType);        
        bool Delete(int FBActivityID);

        FBActivity GetFBActivity(int FBActivityID);

        //List
        List<FBActivity> GetFBActivitiesAll();
        Business.DataTransferObjects.Report.FBReport GetFBActivitiesAllCurrent();
        Business.DataTransferObjects.Report.FBReport GetFBActivitiesAllCurrentMonth();
        Business.DataTransferObjects.Report.FBReport GetFBActivitiesAllCurrentOverall();
        List<FBActivity> GetFBActivitiesAll(int page = 1, int pageSize = 7, string filter=null);
        
        
        //IQueryable
        IQueryable<FBActivity> GetFBActivitiesAllAsQueryable();
        IQueryable<FBActivity> GetFBActivitiesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
    }
}


