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
    public class FeatureRepository : IFeatureRepository, IDisposable
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
        public Feature Insert(String FeatureTitle, DateTime FeatureDateCreated, DateTime FeatureDateExpired, Boolean FeatureStatus, Boolean FeatureIsDeleted, String FeatureImage, String FeatureDescription, int ProfileID)
        {
            try
            {
                Feature obj = new Feature();


                obj.FeatureTitle = FeatureTitle;
                obj.FeatureDateCreated = FeatureDateCreated;
                obj.FeatureDateExpired = FeatureDateExpired;
                obj.FeatureStatus = FeatureStatus;
                obj.FeatureIsDeleted = FeatureIsDeleted;
                obj.FeatureImage = FeatureImage;
                obj.FeatureDescription = FeatureDescription;
                obj.ProfileID = ProfileID;
                context.Features.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Feature();
            }
        }

        public bool Update(int FeatureID, String FeatureTitle, DateTime FeatureDateCreated, DateTime FeatureDateExpired, Boolean FeatureStatus, Boolean FeatureIsDeleted, String FeatureImage, String FeatureDescription, int ProfileID)
        {
            Feature obj = new Feature();
            obj = (from w in context.Features where w.FeatureID == FeatureID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Features.Attach(obj);
                obj.FeatureTitle = FeatureTitle;
                obj.FeatureDateCreated = FeatureDateCreated;
                obj.FeatureDateExpired = FeatureDateExpired;
                obj.FeatureStatus = FeatureStatus;
                obj.FeatureIsDeleted = FeatureIsDeleted;
                obj.FeatureImage = FeatureImage;
                obj.FeatureDescription = FeatureDescription;
                obj.ProfileID = ProfileID;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int FeatureID)
        {
            Feature obj = new Feature();
            obj = (from w in context.Features where w.FeatureID == FeatureID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Features.Attach(obj);
                context.Features.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Feature GetFeature(int FeatureID)
        {
            return (from w in context.Features where w.FeatureID == FeatureID select w).FirstOrDefault();
        }

        //Lists
        public List<Feature> GetFeaturesAll()
        {
            return (from w in context.Features orderby w.FeatureID descending select w).ToList();
        }
        public List<Feature> GetFeaturesAll(bool FeatureStatus)
        {
            return (from w in context.Features where w.FeatureStatus == FeatureStatus orderby w.FeatureID descending select w).ToList();
        }
        //List Pagings
        public List<Feature> GetFeaturesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Feature>();
            if (filter != null)
            {
                xList = (from w in context.Features orderby w.FeatureID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Features orderby w.FeatureID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<Feature> GetFeaturesAll(bool FeatureStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Feature>();
            if (filter != null)
            {
                xList = (from w in context.Features where w.FeatureStatus == FeatureStatus orderby w.FeatureID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Features where w.FeatureStatus == FeatureStatus orderby w.FeatureID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<Feature> GetFeaturesAllAsQueryable()
        {
            return (from w in context.Features orderby w.FeatureID descending select w).AsQueryable();
        }
        public IQueryable<Feature> GetFeaturesAllAsQueryable(bool FeatureStatus)
        {
            return (from w in context.Features where w.FeatureStatus == FeatureStatus orderby w.FeatureID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<Feature> GetFeaturesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Feature> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Features orderby w.FeatureID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Features.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Features orderby w.FeatureID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Features.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<Feature> GetFeaturesAllAsQueryable(bool FeatureStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Feature> xList;
            if (filter != null)
            {
                xList = (from w in context.Features where w.FeatureStatus == FeatureStatus orderby w.FeatureID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Features where w.FeatureStatus == FeatureStatus orderby w.FeatureID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }

    }
}