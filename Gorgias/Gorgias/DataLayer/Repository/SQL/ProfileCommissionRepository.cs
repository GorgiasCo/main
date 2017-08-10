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
    public class ProfileCommissionRepository : IProfileCommissionRepository, IDisposable
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
        public ProfileCommission Insert(double ProfileCommissionRate, DateTime ProfileCommissionDateCreated, Boolean ProfileCommissionStatus, int ProfileID, int UserID, int UserRoleID)
        {
            var previousResult = GetProfileCommissionsByProfileID(ProfileID, true);
            double totalCommission = previousResult.Sum(m => m.ProfileCommissionRate);

            if(previousResult.Any(m=> m.UserRoleID == UserRoleID && m.ProfileID == ProfileID && m.ProfileCommissionStatus == true && m.UserID == UserID))
            {
                throw new Exception("Inviti is exist, cant have more than one invite");
            }

            if (totalCommission + ProfileCommissionRate > 100)
            {
                throw new Exception("Total cant be more than 100%");
            }

            if(UserRoleID == 4)
            {
                var userInviti = previousResult.Where(m => m.UserRoleID == 4).FirstOrDefault();
                if (userInviti != null)
                {
                    throw new Exception("Inviti is exist, cant have more than one invite");
                }
            }

            if (UserRoleID == 6)
            {
                var userInviti = previousResult.Where(m => m.UserRoleID == 6).FirstOrDefault();
                if (userInviti != null)
                {
                    throw new Exception("Country Distributer is exist, cant have more than one invite");
                }
            }

            try
            {
                ProfileCommission obj = new ProfileCommission();
                obj.ProfileCommissionRate = ProfileCommissionRate;
                obj.ProfileCommissionDateCreated = ProfileCommissionDateCreated;
                obj.ProfileCommissionStatus = ProfileCommissionStatus;
                obj.ProfileID = ProfileID;
                obj.UserID = UserID;
                obj.UserRoleID = UserRoleID;
                context.ProfileCommissions.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Update(int ProfileCommissionID, double ProfileCommissionRate, DateTime ProfileCommissionDateCreated, Boolean ProfileCommissionStatus, int ProfileID, int UserID, int UserRoleID)
        {
            ProfileCommission obj = new ProfileCommission();
            obj = (from w in context.ProfileCommissions where w.ProfileCommissionID == ProfileCommissionID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileCommissions.Attach(obj);

                obj.ProfileCommissionRate = ProfileCommissionRate;
                obj.ProfileCommissionDateCreated = ProfileCommissionDateCreated;
                obj.ProfileCommissionStatus = ProfileCommissionStatus;
                obj.ProfileID = ProfileID;
                obj.UserID = UserID;
                obj.UserRoleID = UserRoleID;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int ProfileCommissionID)
        {
            ProfileCommission obj = new ProfileCommission();
            obj = (from w in context.ProfileCommissions where w.ProfileCommissionID == ProfileCommissionID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileCommissions.Attach(obj);
                context.ProfileCommissions.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteOnly(int ProfileCommissionID)
        {
            ProfileCommission obj = new ProfileCommission();
            obj = (from w in context.ProfileCommissions where w.ProfileCommissionID == ProfileCommissionID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileCommissions.Attach(obj);                
                obj.ProfileCommissionStatus = false;                
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ProfileCommission GetProfileCommission(int ProfileCommissionID)
        {
            return (from w in context.ProfileCommissions where w.ProfileCommissionID == ProfileCommissionID select w).FirstOrDefault();
        }

        //Lists
        public List<ProfileCommission> GetProfileCommissionsAll()
        {
            return (from w in context.ProfileCommissions orderby w.ProfileCommissionID descending select w).ToList();
        }
        public List<ProfileCommission> GetProfileCommissionsAll(bool ProfileCommissionStatus)
        {
            return (from w in context.ProfileCommissions where w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).ToList();
        }
        //List Pagings
        public List<ProfileCommission> GetProfileCommissionsAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ProfileCommission>();
            if (filter != null)
            {
                xList = (from w in context.ProfileCommissions orderby w.ProfileCommissionID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ProfileCommissions orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<ProfileCommission> GetProfileCommissionsAll(bool ProfileCommissionStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ProfileCommission>();
            if (filter != null)
            {
                xList = (from w in context.ProfileCommissions where w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ProfileCommissions where w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<ProfileCommission> GetProfileCommissionsAllAsQueryable()
        {
            return (from w in context.ProfileCommissions.Include("Profile").Include("User").Include("UserRole") orderby w.ProfileCommissionID descending select w).AsQueryable();
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsAllAsQueryable(bool ProfileCommissionStatus)
        {
            return (from w in context.ProfileCommissions where w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<ProfileCommission> GetProfileCommissionsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ProfileCommission> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileCommissions orderby w.ProfileCommissionID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileCommissions.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileCommissions orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileCommissions.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsAllAsQueryable(bool ProfileCommissionStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ProfileCommission> xList;
            if (filter != null)
            {
                xList = (from w in context.ProfileCommissions where w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.ProfileCommissions where w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<ProfileCommission> GetProfileCommissionsByProfileID(int ProfileID, bool ProfileCommissionStatus)
        {
            return (from w in context.ProfileCommissions where w.ProfileID == ProfileID && w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).ToList();
        }
        public List<ProfileCommission> GetProfileCommissionsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileCommissions where w.ProfileID == ProfileID orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileCommission> GetProfileCommissionsByProfileID(int ProfileID, bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileCommissions where w.ProfileID == ProfileID && w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileCommission> GetProfileCommissionsByUserID(int UserID, bool ProfileCommissionStatus)
        {
            return (from w in context.ProfileCommissions where w.UserID == UserID && w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).ToList();
        }
        public List<ProfileCommission> GetProfileCommissionsByUserID(int UserID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileCommissions where w.UserID == UserID orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileCommission> GetProfileCommissionsByUserID(int UserID, bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileCommissions where w.UserID == UserID && w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileCommission> GetProfileCommissionsByUserRoleID(int UserRoleID, bool ProfileCommissionStatus)
        {
            return (from w in context.ProfileCommissions where w.UserRoleID == UserRoleID && w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).ToList();
        }
        public List<ProfileCommission> GetProfileCommissionsByUserRoleID(int UserRoleID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileCommissions where w.UserRoleID == UserRoleID orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileCommission> GetProfileCommissionsByUserRoleID(int UserRoleID, bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileCommissions where w.UserRoleID == UserRoleID && w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsByProfileIDAsQueryable(int ProfileID)
        {
            return (from w in context.ProfileCommissions.Include("Profile").Include("User").Include("UserRole") where w.ProfileID == ProfileID orderby w.ProfileCommissionID descending select w).AsQueryable();
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsByProfileIDAsQueryable(int ProfileID, bool ProfileCommissionStatus)
        {
            return (from w in context.ProfileCommissions where w.ProfileID == ProfileID && w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).AsQueryable();
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileCommissions where w.ProfileID == ProfileID orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsByProfileIDAsQueryable(int ProfileID, bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileCommissions where w.ProfileID == ProfileID && w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsByUserIDAsQueryable(int UserID)
        {
            return (from w in context.ProfileCommissions where w.UserID == UserID orderby w.ProfileCommissionID descending select w).AsQueryable();
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsByUserIDAsQueryable(int UserID, bool ProfileCommissionStatus)
        {
            return (from w in context.ProfileCommissions where w.UserID == UserID && w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).AsQueryable();
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsByUserIDAsQueryable(int UserID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileCommissions where w.UserID == UserID orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsByUserIDAsQueryable(int UserID, bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileCommissions where w.UserID == UserID && w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsByUserRoleIDAsQueryable(int UserRoleID)
        {
            return (from w in context.ProfileCommissions where w.UserRoleID == UserRoleID orderby w.ProfileCommissionID descending select w).AsQueryable();
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsByUserRoleIDAsQueryable(int UserRoleID, bool ProfileCommissionStatus)
        {
            return (from w in context.ProfileCommissions where w.UserRoleID == UserRoleID && w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).AsQueryable();
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsByUserRoleIDAsQueryable(int UserRoleID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileCommissions where w.UserRoleID == UserRoleID orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileCommission> GetProfileCommissionsByUserRoleIDAsQueryable(int UserRoleID, bool ProfileCommissionStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileCommissions where w.UserRoleID == UserRoleID && w.ProfileCommissionStatus == ProfileCommissionStatus orderby w.ProfileCommissionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}