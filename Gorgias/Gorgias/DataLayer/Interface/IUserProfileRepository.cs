using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IUserProfileRepository
    {
    
    
        UserProfile Insert(int ProfileID, int UserRoleID, int UserID);
        bool Update(int ProfileID, int UserRoleID, int UserID);
        System.Threading.Tasks.Task<bool> Delete(int ProfileID, int UserRoleID, int UserID);
        UserProfile GetUserProfile(int ProfileID, int UserRoleID, int UserID);
        UserProfile GetUserProfile(int ProfileID, int UserRoleID);
        IEnumerable<UserProfile> GetAdministrationUserProfile(string UserEmail);
        IEnumerable<UserProfile> GetAdministrationCountryUserProfile(int CountryID);
        IEnumerable<UserProfile> GetUserProfileAgency(int UserID);
        int[] GetAdministrationUserProfile(int UserID);
        UserProfile GetAdministrationUserProfileValidation(int UserID, int ProfileID);
        //List
        List<UserProfile> GetUserProfilesAll();
        List<UserProfile> GetUserProfilesAll(int page = 1, int pageSize = 7, string filter=null);
        
        List<UserProfile> GetUserProfilesByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        List<UserProfile> GetUserProfilesByUserRoleID(int UserRoleID, int page = 1, int pageSize = 7, string filter=null);       
        List<UserProfile> GetUserProfilesByUserID(int UserID, int page = 1, int pageSize = 7, string filter=null);       
        
        //IQueryable
        IQueryable<UserProfile> GetUserProfilesAllAsQueryable();
        IQueryable<UserProfile> GetUserProfilesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<UserProfile> GetUserProfilesByProfileIDAsQueryable(int ProfileID);
        IQueryable<UserProfile> GetUserProfilesByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<UserProfile> GetUserProfilesByUserRoleIDAsQueryable(int UserRoleID);
        IQueryable<UserProfile> GetUserProfilesByUserRoleIDAsQueryable(int UserRoleID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<UserProfile> GetUserProfilesByUserIDAsQueryable(int UserID);
        IQueryable<UserProfile> GetUserProfilesByUserIDAsQueryable(int UserID, int page = 1, int pageSize = 7, string filter=null);       
    }
}


