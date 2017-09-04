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
    public class ProfileTokenRepository : IProfileTokenRepository, IDisposable
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
        public ProfileToken Insert(String ProfileTokenString, DateTime ProfileTokenRegistration, int ProfileID)
        {
            try
            {
                ProfileToken obj = new ProfileToken();
                obj.ProfileTokenString = ProfileTokenString;
                obj.ProfileTokenRegistration = ProfileTokenRegistration;
                obj.ProfileID = ProfileID;
                context.ProfileTokens.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new ProfileToken();
            }
        }

        public bool Update(int ProfileTokenID, String ProfileTokenString, DateTime ProfileTokenRegistration, int ProfileID)
        {
            ProfileToken obj = new ProfileToken();
            obj = (from w in context.ProfileTokens where w.ProfileTokenID == ProfileTokenID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileTokens.Attach(obj);

                obj.ProfileTokenString = ProfileTokenString;
                obj.ProfileTokenRegistration = ProfileTokenRegistration;
                obj.ProfileID = ProfileID;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int ProfileTokenID)
        {
            ProfileToken obj = new ProfileToken();
            obj = (from w in context.ProfileTokens where w.ProfileTokenID == ProfileTokenID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileTokens.Attach(obj);
                context.ProfileTokens.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ProfileToken GetProfileToken(int ProfileTokenID)
        {
            return (from w in context.ProfileTokens where w.ProfileTokenID == ProfileTokenID select w).FirstOrDefault();
        }

        //Lists
        public List<ProfileToken> GetProfileTokensAll()
        {
            return (from w in context.ProfileTokens orderby w.ProfileTokenID descending select w).ToList();
        }
        //List Pagings
        public List<ProfileToken> GetProfileTokensAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ProfileToken>();
            if (filter != null)
            {
                xList = (from w in context.ProfileTokens orderby w.ProfileTokenID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ProfileTokens orderby w.ProfileTokenID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<ProfileToken> GetProfileTokensAllAsQueryable()
        {
            return (from w in context.ProfileTokens orderby w.ProfileTokenID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<ProfileToken> GetProfileTokensAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ProfileToken> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileTokens orderby w.ProfileTokenID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileTokens.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileTokens orderby w.ProfileTokenID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileTokens.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<ProfileToken> GetProfileTokensByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileTokens where w.ProfileID == ProfileID orderby w.ProfileTokenID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<ProfileToken> GetProfileTokensByProfileIDAsQueryable(int ProfileID)
        {
            return (from w in context.ProfileTokens where w.ProfileID == ProfileID orderby w.ProfileTokenID descending select w).AsQueryable();
        }
        public IQueryable<ProfileToken> GetProfileTokensByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileTokens where w.ProfileID == ProfileID orderby w.ProfileTokenID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}