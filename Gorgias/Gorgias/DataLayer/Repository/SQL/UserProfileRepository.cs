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
    public class UserProfileRepository : IUserProfileRepository, IDisposable
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
        public UserProfile Insert(int ProfileID, int UserRoleID, int UserID)
        {
            try
            {
                UserProfile obj = new UserProfile();
                obj.ProfileID = ProfileID;
                obj.UserRoleID = UserRoleID;
                obj.UserID = UserID;


                context.UserProfiles.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new UserProfile();
            }
        }

        public bool Update(int ProfileID, int UserRoleID, int UserID)
        {
            UserProfile obj = new UserProfile();
            obj = (from w in context.UserProfiles where w.ProfileID == ProfileID && w.UserRoleID == UserRoleID && w.UserID == UserID select w).FirstOrDefault();
            if (obj != null)
            {
                context.UserProfiles.Attach(obj);

                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async System.Threading.Tasks.Task<bool> Delete(int ProfileID, int UserRoleID, int UserID)
        {
            UserProfile obj = new UserProfile();
            obj = (from w in context.UserProfiles.Include("User") where w.ProfileID == ProfileID && w.UserRoleID == UserRoleID && w.UserID == UserID select w).FirstOrDefault();
            if (obj != null)
            {
                //DataLayer.Authentication.AuthRepository authRepo = new DataLayer.Authentication.AuthRepository();
                //await authRepo.DeleteUser(obj.User.UserEmail);
                context.UserProfiles.Attach(obj);
                context.UserProfiles.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<UserProfile> GetAdministrationUserProfile(string UserEmail)
        {
            return (from w in context.UserProfiles.Include("Profile").Include("User") where w.User.UserEmail == UserEmail orderby w.ProfileID descending select w).ToList();
        }

        public IEnumerable<UserProfile> GetAdministrationCountryUserProfile(int CountryID)
        {
            return (from w in context.UserProfiles.Include("Profile").Include("User") where w.Profile.Addresses.Any(m=> m.City.CountryID == CountryID) && w.Profile.SubscriptionTypeID != 5 orderby w.ProfileID descending select w).ToList().Distinct();
        }

        public IEnumerable<UserProfile> GetUserProfileAgency(int UserID)
        {
            return (from w in context.UserProfiles.Include("Profile") where w.UserID == UserID && w.UserRoleID == 5 orderby w.ProfileID descending select w).ToList();
        }

        public int[] GetAdministrationUserProfile(int UserID)
        {
            return (from w in context.UserProfiles where w.User.UserID == UserID orderby w.ProfileID descending select w.ProfileID).ToArray();
        }

        public UserProfile GetAdministrationUserProfileValidation(int UserID, int ProfileID)
        {
            try
            {
                return (from w in context.UserProfiles where w.UserID == UserID && w.ProfileID == ProfileID orderby w.ProfileID descending select w).FirstOrDefault();
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        public UserProfile GetUserProfile(int ProfileID, int UserRoleID, int UserID)
        {
            return (from w in context.UserProfiles where w.ProfileID == ProfileID && w.UserRoleID == UserRoleID && w.UserID == UserID select w).FirstOrDefault();
        }

        public UserProfile GetUserProfile(int ProfileID, int UserRoleID)
        {
            return (from w in context.UserProfiles where w.ProfileID == ProfileID && w.UserRoleID == UserRoleID select w).FirstOrDefault();
        }

        //Lists
        public List<UserProfile> GetUserProfilesAll()
        {
            return (from w in context.UserProfiles orderby w.ProfileID, w.UserRoleID, w.UserID descending select w).ToList();
        }
        //List Pagings
        public List<UserProfile> GetUserProfilesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<UserProfile>();
            if (filter != null)
            {
                xList = (from w in context.UserProfiles orderby w.ProfileID, w.UserRoleID, w.UserID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.UserProfiles orderby w.ProfileID, w.UserRoleID, w.UserID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<UserProfile> GetUserProfilesAllAsQueryable()
        {
            return (from w in context.UserProfiles.Include("Profile").Include("User") orderby w.ProfileID, w.UserRoleID, w.UserID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<UserProfile> GetUserProfilesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<UserProfile> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.UserProfiles orderby w.UserProfileID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.UserProfiles.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.UserProfiles orderby w.UserProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.UserProfiles.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<UserProfile> GetUserProfilesByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.UserProfiles where w.ProfileID == ProfileID orderby w.ProfileID, w.UserRoleID, w.UserID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<UserProfile> GetUserProfilesByUserRoleID(int UserRoleID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.UserProfiles where w.UserRoleID == UserRoleID orderby w.ProfileID, w.UserRoleID, w.UserID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<UserProfile> GetUserProfilesByUserID(int UserID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.UserProfiles where w.UserID == UserID orderby w.ProfileID, w.UserRoleID, w.UserID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<UserProfile> GetUserProfilesByProfileIDAsQueryable(int ProfileID)
        {
            return (from w in context.UserProfiles where w.ProfileID == ProfileID orderby w.ProfileID, w.UserRoleID, w.UserID descending select w).AsQueryable();
        }
        public IQueryable<UserProfile> GetUserProfilesByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.UserProfiles where w.ProfileID == ProfileID orderby w.ProfileID, w.UserRoleID, w.UserID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<UserProfile> GetUserProfilesByUserRoleIDAsQueryable(int UserRoleID)
        {
            return (from w in context.UserProfiles where w.UserRoleID == UserRoleID orderby w.ProfileID, w.UserRoleID, w.UserID descending select w).AsQueryable();
        }
        public IQueryable<UserProfile> GetUserProfilesByUserRoleIDAsQueryable(int UserRoleID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.UserProfiles where w.UserRoleID == UserRoleID orderby w.ProfileID, w.UserRoleID, w.UserID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<UserProfile> GetUserProfilesByUserIDAsQueryable(int UserID)
        {
            return (from w in context.UserProfiles where w.UserID == UserID orderby w.ProfileID, w.UserRoleID, w.UserID descending select w).AsQueryable();
        }
        public IQueryable<UserProfile> GetUserProfilesByUserIDAsQueryable(int UserID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.UserProfiles where w.UserID == UserID orderby w.ProfileID, w.UserRoleID, w.UserID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}