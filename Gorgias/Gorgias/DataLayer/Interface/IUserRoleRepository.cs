using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IUserRoleRepository
    {
    
    
        UserRole Insert(String UserRoleName, Boolean UserRoleStatus, String UserRoleImage, String UserRoleDescription);
        bool Update(int UserRoleID, String UserRoleName, Boolean UserRoleStatus, String UserRoleImage, String UserRoleDescription);        
        bool Delete(int UserRoleID);

        UserRole GetUserRole(int UserRoleID);

        //List
        List<UserRole> GetUserRolesAll();
        List<UserRole> GetUserRolesAll(bool UserRoleStatus);
        List<UserRole> GetUserRolesAll(int page = 1, int pageSize = 7, string filter=null);
        List<UserRole> GetUserRolesAll(bool UserRoleStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<UserRole> GetUserRolesAllAsQueryable();
        IQueryable<UserRole> GetUserRolesAllAsQueryable(bool UserRoleStatus);
        IQueryable<UserRole> GetUserRolesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<UserRole> GetUserRolesAllAsQueryable(bool UserRoleStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}


