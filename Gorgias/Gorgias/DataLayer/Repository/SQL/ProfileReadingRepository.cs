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
    public class ProfileReadingRepository : IProfileReadingRepository, IDisposable
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
        public ProfileReading Insert(String ProfileReadingLanguageCode, DateTime ProfileReadingDatetime, int ProfileID)
        {
            try
            {
                ProfileReading obj = new ProfileReading();


                obj.ProfileReadingLanguageCode = ProfileReadingLanguageCode;
                obj.ProfileReadingDatetime = ProfileReadingDatetime;
                obj.ProfileID = ProfileID;
                context.ProfileReadings.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new ProfileReading();
            }
        }

        public bool Update(int ProfileReadingID, String ProfileReadingLanguageCode, DateTime ProfileReadingDatetime, int ProfileID)
        {
            ProfileReading obj = new ProfileReading();
            obj = (from w in context.ProfileReadings where w.ProfileReadingID == ProfileReadingID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileReadings.Attach(obj);

                obj.ProfileReadingLanguageCode = ProfileReadingLanguageCode;
                obj.ProfileReadingDatetime = ProfileReadingDatetime;
                obj.ProfileID = ProfileID;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int ProfileReadingID)
        {
            ProfileReading obj = new ProfileReading();
            obj = (from w in context.ProfileReadings where w.ProfileReadingID == ProfileReadingID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ProfileReadings.Attach(obj);
                context.ProfileReadings.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteByProfileID(int ProfileID)
        {
            List<ProfileReading> obj = new List<ProfileReading>();
            obj = (from w in context.ProfileReadings where w.ProfileID == ProfileID select w).ToList();
            foreach(ProfileReading objProfileReading in obj)
            {
                Delete(objProfileReading.ProfileReadingID);
            }
            return true;
        }

        public ProfileReading GetProfileReading(int ProfileReadingID)
        {
            return (from w in context.ProfileReadings where w.ProfileReadingID == ProfileReadingID select w).FirstOrDefault();
        }

        //Lists
        public List<ProfileReading> GetProfileReadingsAll()
        {
            return (from w in context.ProfileReadings orderby w.ProfileReadingID descending select w).ToList();
        }
        //List Pagings
        public List<ProfileReading> GetProfileReadingsAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ProfileReading>();
            if (filter != null)
            {
                xList = (from w in context.ProfileReadings orderby w.ProfileReadingID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ProfileReadings orderby w.ProfileReadingID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<ProfileReading> GetProfileReadingsAllAsQueryable()
        {
            return (from w in context.ProfileReadings orderby w.ProfileReadingID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<ProfileReading> GetProfileReadingsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ProfileReading> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileReadings orderby w.ProfileReadingID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileReadings.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileReadings orderby w.ProfileReadingID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ProfileReadings.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<ProfileReading> GetProfileReadingsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileReadings where w.ProfileID == ProfileID orderby w.ProfileReadingID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<ProfileReading> GetProfileReadingsByProfileIDAsQueryable(int ProfileID)
        {
            return (from w in context.ProfileReadings where w.ProfileID == ProfileID orderby w.ProfileReadingID descending select w).AsQueryable();
        }
        public IQueryable<ProfileReading> GetProfileReadingsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ProfileReadings where w.ProfileID == ProfileID orderby w.ProfileReadingID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}