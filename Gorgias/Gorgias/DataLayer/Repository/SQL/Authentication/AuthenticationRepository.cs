using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.DataLayer.Repository.SQL.Authentication
{
    public class AuthenticationRepository : IDisposable
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


        public User GetUser(string Username)
        {
            return (from w in context.Users where w.UserEmail == Username select w).FirstOrDefault();
        }

        public User GetUser(int UserID)
        {
            return (from w in context.Users where w.UserID == UserID select w).FirstOrDefault();
        }

        public UserRole GetRole(int UserRoleID) {
            return (from w in context.UserRoles where w.UserRoleID == UserRoleID select w).FirstOrDefault();
        }

        public List<UserRole> GetUserRoles(string Username) {
            var resultProfile = (from w in context.UserProfiles where w.User.UserEmail == Username select w).ToList();
            List<UserRole> result = new List<UserRole>();
            foreach (UserProfile userProfile in resultProfile) {
                result.Add(userProfile.UserRole);
            }
            return result;
        }

        public void InsertUser(User user) {

        }

        public void InsertUserRole(int UserID, int RoleID)
        {

        }



    }
}