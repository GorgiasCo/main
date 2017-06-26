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
    public class ProfileAttributeRepository : IProfileAttributeRepository, IDisposable
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
        public ProfileAttribute Insert(int AttributeID, int ProfileID, String ProfileAttributeNote)
        {
            try
            {
                ProfileAttribute obj = new ProfileAttribute();
                obj.AttributeID = AttributeID;
                obj.ProfileID = ProfileID;


                obj.ProfileAttributeNote = ProfileAttributeNote;
                context.ProfileAttributes.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new ProfileAttribute();
            }
        }

        public bool Update(int AttributeID, int ProfileID, String ProfileAttributeNote)
        {
            ProfileAttribute obj = new ProfileAttribute();
            obj = (from w in context.ProfileAttributes where w.AttributeID == AttributeID && w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileAttributes.Attach(obj);

                obj.ProfileAttributeNote = ProfileAttributeNote;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int AttributeID, int ProfileID)
        {
            ProfileAttribute obj = new ProfileAttribute();
            obj = (from w in context.ProfileAttributes where w.AttributeID == AttributeID && w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileAttributes.Attach(obj);
                context.ProfileAttributes.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ProfileAttribute GetProfileAttribute(int AttributeID, int ProfileID)
        {
            return (from w in context.ProfileAttributes where w.AttributeID == AttributeID && w.ProfileID == ProfileID select w).FirstOrDefault();
        }

        //Lists
        public List<ProfileAttribute> GetProfileAttributesAll()
        {
            return (from w in context.ProfileAttributes orderby w.AttributeID, w.ProfileID descending select w).ToList();
        }
        //List Pagings
        public List<ProfileAttribute> GetProfileAttributesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ProfileAttribute>();
            if (filter != null)
            {
                xList = (from w in context.ProfileAttributes orderby w.AttributeID, w.ProfileID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ProfileAttributes orderby w.AttributeID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<ProfileAttribute> GetProfileAttributesAllAsQueryable()
        {
            return (from w in context.ProfileAttributes.Include("Profile") orderby w.AttributeID, w.ProfileID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<ProfileAttribute> GetProfileAttributesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ProfileAttribute> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileAttributes orderby w.ProfileAttributeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileAttributes.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileAttributes orderby w.ProfileAttributeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileAttributes.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<ProfileAttribute> GetProfileAttributesByAttributeID(int AttributeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileAttributes where w.AttributeID == AttributeID orderby w.AttributeID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileAttribute> GetProfileAttributesByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileAttributes where w.ProfileID == ProfileID orderby w.AttributeID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<ProfileAttribute> GetProfileAttributesByAttributeIDAsQueryable(int AttributeID)
        {
            return (from w in context.ProfileAttributes where w.AttributeID == AttributeID orderby w.AttributeID, w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<ProfileAttribute> GetProfileAttributesByAttributeIDAsQueryable(int AttributeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileAttributes where w.AttributeID == AttributeID orderby w.AttributeID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileAttribute> GetProfileAttributesByProfileIDAsQueryable(int ProfileID)
        {
            return (from w in context.ProfileAttributes.Include("Profile").Include("Attribute") where w.ProfileID == ProfileID orderby w.AttributeID, w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<ProfileAttribute> GetProfileAttributesByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileAttributes where w.ProfileID == ProfileID orderby w.AttributeID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}