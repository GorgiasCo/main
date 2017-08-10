using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IProfileCommissionRepository
    {
    
    
        ProfileCommission Insert(double ProfileCommissionRate, DateTime ProfileCommissionDateCreated, bool ProfileCommissionStatus, int ProfileID, int UserID, int UserRoleID);
        bool Update(int ProfileCommissionID, double ProfileCommissionRate, DateTime ProfileCommissionDateCreated, bool ProfileCommissionStatus, int ProfileID, int UserID, int UserRoleID);        
        bool Delete(int ProfileCommissionID);
        bool DeleteOnly(int ProfileCommissionID);
        ProfileCommission GetProfileCommission(int ProfileCommissionID);

        //List
        List<ProfileCommission> GetProfileCommissionsAll();
        List<ProfileCommission> GetProfileCommissionsAll(bool ProfileCommissionStatus);
        List<ProfileCommission> GetProfileCommissionsAll(int page = 1, int pageSize = 7, string filter=null);
        List<ProfileCommission> GetProfileCommissionsAll(bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        List<ProfileCommission> GetProfileCommissionsByProfileID(int ProfileID, bool ProfileCommissionStatus);
        List<ProfileCommission> GetProfileCommissionsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        List<ProfileCommission> GetProfileCommissionsByProfileID(int ProfileID, bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter=null);               
        List<ProfileCommission> GetProfileCommissionsByUserID(int UserID, bool ProfileCommissionStatus);
        List<ProfileCommission> GetProfileCommissionsByUserID(int UserID, int page = 1, int pageSize = 7, string filter=null);       
        List<ProfileCommission> GetProfileCommissionsByUserID(int UserID, bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter=null);               
        List<ProfileCommission> GetProfileCommissionsByUserRoleID(int UserRoleID, bool ProfileCommissionStatus);
        List<ProfileCommission> GetProfileCommissionsByUserRoleID(int UserRoleID, int page = 1, int pageSize = 7, string filter=null);       
        List<ProfileCommission> GetProfileCommissionsByUserRoleID(int UserRoleID, bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter=null);               
        
        //IQueryable
        IQueryable<ProfileCommission> GetProfileCommissionsAllAsQueryable();
        IQueryable<ProfileCommission> GetProfileCommissionsAllAsQueryable(bool ProfileCommissionStatus);
        IQueryable<ProfileCommission> GetProfileCommissionsAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<ProfileCommission> GetProfileCommissionsAllAsQueryable(bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter=null);   
        IQueryable<ProfileCommission> GetProfileCommissionsByProfileIDAsQueryable(int ProfileID);
        IQueryable<ProfileCommission> GetProfileCommissionsByProfileIDAsQueryable(int ProfileID, bool ProfileCommissionStatus);
        IQueryable<ProfileCommission> GetProfileCommissionsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<ProfileCommission> GetProfileCommissionsByProfileIDAsQueryable(int ProfileID, bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter=null);               
        IQueryable<ProfileCommission> GetProfileCommissionsByUserIDAsQueryable(int UserID);
        IQueryable<ProfileCommission> GetProfileCommissionsByUserIDAsQueryable(int UserID, bool ProfileCommissionStatus);
        IQueryable<ProfileCommission> GetProfileCommissionsByUserIDAsQueryable(int UserID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<ProfileCommission> GetProfileCommissionsByUserIDAsQueryable(int UserID, bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter=null);               
        IQueryable<ProfileCommission> GetProfileCommissionsByUserRoleIDAsQueryable(int UserRoleID);
        IQueryable<ProfileCommission> GetProfileCommissionsByUserRoleIDAsQueryable(int UserRoleID, bool ProfileCommissionStatus);
        IQueryable<ProfileCommission> GetProfileCommissionsByUserRoleIDAsQueryable(int UserRoleID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<ProfileCommission> GetProfileCommissionsByUserRoleIDAsQueryable(int UserRoleID, bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter=null);               
    }
}


