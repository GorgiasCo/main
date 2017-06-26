using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IUserRepository
    {
        User Insert(string UserFullname, string UserEmail);
        User Insert(String UserFullname, String UserEmail, Boolean UserStatus, Boolean UserIsBlocked, int? CountryID);
        bool Update(int UserID);
        bool Update(int UserID, String UserFullname, String UserEmail, Boolean UserStatus, Boolean UserIsBlocked, DateTime UserDateCreated, DateTime UserDateConfirmed, int? CountryID);
        System.Threading.Tasks.Task<bool> Delete(int UserID);

        User GetUser(int UserID);
        User GetUserForAdministration(int UserID);
        User GetUserForAdministration(string UserEmail);
        bool GetUserByProfile(int UserID, int ProfileID);

        //List
        List<User> GetUsersAll();
        List<User> GetUsersAll(bool UserStatus);
        List<User> GetUsersAll(int page = 1, int pageSize = 7, string filter = null);
        List<User> GetUsersAll(bool UserStatus, int page = 1, int pageSize = 7, string filter = null);


        //IQueryable
        IQueryable<User> GetUsersAllAsQueryable();
        IQueryable<User> GetUsersAllAsQueryable(int CountryID);
        IQueryable<User> GetUsersAllAsQueryable(bool UserStatus);
        IQueryable<User> GetUsersAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<User> GetUsersAllAsQueryable(bool UserStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


