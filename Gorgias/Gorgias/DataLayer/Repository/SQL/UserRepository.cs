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
    public class UserRepository : IUserRepository, IDisposable
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
        public User Insert(string UserFullname, string UserEmail)
        {
            try
            {
                User obj = new User();
                obj.UserFullname = UserFullname;
                obj.UserEmail = UserEmail;
                obj.UserStatus = false;
                obj.UserIsBlocked = false;
                obj.UserDateCreated = DateTime.Now;
                obj.UserDateConfirmed = DateTime.Now;
                                
                context.Users.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new User();
            }
        }

        public User Insert(String UserFullname, String UserEmail, Boolean UserStatus, Boolean UserIsBlocked, int? CountryID)
        {
            User existUser = GetUser(UserEmail);
            if (existUser != null)
            {
                return new User();
            }
            try
            {
                User obj = new User();
                obj.UserFullname = UserFullname;
                obj.UserEmail = UserEmail;
                obj.UserStatus = UserStatus;
                obj.UserIsBlocked = UserIsBlocked;
                obj.UserDateCreated = DateTime.Now;
                obj.UserDateConfirmed = DateTime.Now;
                if (CountryID.HasValue)
                {
                    obj.CountryID = CountryID;
                }                
                context.Users.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new User();
            }
        }

        public bool Update(int UserID, String UserFullname, String UserEmail, Boolean UserStatus, Boolean UserIsBlocked, DateTime UserDateCreated, DateTime UserDateConfirmed, int? CountryID)
        {
            User obj = new User();
            obj = (from w in context.Users where w.UserID == UserID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Users.Attach(obj);

                obj.UserFullname = UserFullname;
                obj.UserEmail = UserEmail;
                obj.UserStatus = UserStatus;
                obj.UserIsBlocked = UserIsBlocked;
                if (CountryID.HasValue)
                {
                    obj.CountryID = CountryID;
                } else
                {
                    obj.CountryID = null;
                }
                //obj.UserDateCreated = UserDateCreated;
                //obj.UserDateConfirmed = UserDateConfirmed;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int UserID)
        {
            User obj = new User();
            obj = (from w in context.Users where w.UserID == UserID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Users.Attach(obj);
                obj.UserStatus = true;                
                obj.UserDateConfirmed = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async System.Threading.Tasks.Task<bool> Delete(int UserID)
        {
            User obj = new User();
            obj = (from w in context.Users where w.UserID == UserID select w).FirstOrDefault();

            if (obj != null)
            {
                //Delete from ASP User Table ;)
                DataLayer.Authentication.AuthRepository authRepo = new DataLayer.Authentication.AuthRepository();
                await authRepo.DeleteUser(obj.UserEmail);

                context.Users.Attach(obj);
                context.Users.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public User GetUser(int UserID)
        {
            return (from w in context.Users where w.UserID == UserID select w).FirstOrDefault();
        }

        public User GetUserForAdministration(int UserID)
        {
            return (from w in context.Users.Include("UserProfiles") where w.UserID == UserID && w.UserStatus == true select w).FirstOrDefault();
        }

        public User GetUserForAdministration(string UserEmail)
        {
            return (from w in context.Users.Include("UserProfiles.Profile") where w.UserEmail.ToLower() == UserEmail.ToLower() && w.UserStatus == true select w).FirstOrDefault();
        }

        public bool GetUserByProfile(int UserID, int ProfileID)
        {
            try {
                User obj = (from w in context.Users where w.UserID == UserID && w.UserProfiles.Any(m => m.ProfileID == ProfileID) select w).First();
                return true;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }                       
        }

        public User GetUser(string UserEmail)
        {
            return (from w in context.Users where w.UserEmail == UserEmail select w).FirstOrDefault();
        }

        //Lists
        public List<User> GetUsersAll()
        {
            return (from w in context.Users orderby w.UserID descending select w).ToList();
        }
        public List<User> GetUsersAll(bool UserStatus)
        {
            return (from w in context.Users where w.UserStatus == UserStatus orderby w.UserID descending select w).ToList();
        }
        //List Pagings
        public List<User> GetUsersAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<User>();
            if (filter != null)
            {
                xList = (from w in context.Users orderby w.UserID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Users orderby w.UserID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<User> GetUsersAll(bool UserStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<User>();
            if (filter != null)
            {
                xList = (from w in context.Users where w.UserStatus == UserStatus orderby w.UserID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Users where w.UserStatus == UserStatus orderby w.UserID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<User> GetUsersAllAsQueryable()
        {
            return (from w in context.Users orderby w.UserID descending select w).AsQueryable();
        }

        public IQueryable<User> GetUsersAllAsQueryable(int CountryID)
        {
            return (from w in context.Users where w.CountryID == CountryID orderby w.UserID descending select w).AsQueryable();
        }
        public IQueryable<User> GetUsersAllAsQueryable(bool UserStatus)
        {
            return (from w in context.Users where w.UserStatus == UserStatus orderby w.UserID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<User> GetUsersAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<User> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Users orderby w.UserID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Users.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Users orderby w.UserID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Users.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<User> GetUsersAllAsQueryable(bool UserStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<User> xList;
            if (filter != null)
            {
                xList = (from w in context.Users where w.UserStatus == UserStatus orderby w.UserID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Users where w.UserStatus == UserStatus orderby w.UserID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }

    }
}