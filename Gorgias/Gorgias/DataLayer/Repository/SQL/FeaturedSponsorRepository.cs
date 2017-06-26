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
    public class FeaturedSponsorRepository : IFeaturedSponsorRepository, IDisposable
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
        public FeaturedSponsor Insert(int FeatureID, int ProfileID, int FeaturedSponsorMode, int FeaturedRole)
        {
            try
            {
                FeaturedSponsor obj = new FeaturedSponsor();
                obj.FeatureID = FeatureID;
                obj.ProfileID = ProfileID;
                obj.FeaturedSponsorMode = FeaturedSponsorMode;
                obj.FeaturedRole = FeaturedRole;
                context.FeaturedSponsors.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new FeaturedSponsor();
            }
        }

        public bool Update(int FeatureID, int ProfileID, int FeaturedSponsorMode, int FeaturedRole)
        {
            FeaturedSponsor obj = new FeaturedSponsor();
            obj = (from w in context.FeaturedSponsors where w.FeatureID == FeatureID && w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.FeaturedSponsors.Attach(obj);

                obj.FeaturedSponsorMode = FeaturedSponsorMode;
                obj.FeaturedRole = FeaturedRole;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int FeatureID, int ProfileID)
        {
            FeaturedSponsor obj = new FeaturedSponsor();
            obj = (from w in context.FeaturedSponsors where w.FeatureID == FeatureID && w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.FeaturedSponsors.Attach(obj);
                context.FeaturedSponsors.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public FeaturedSponsor GetFeaturedSponsor(int FeatureID, int ProfileID)
        {
            return (from w in context.FeaturedSponsors where w.FeatureID == FeatureID && w.ProfileID == ProfileID select w).FirstOrDefault();
        }

        //Lists
        public List<FeaturedSponsor> GetFeaturedSponsorsAll()
        {
            return (from w in context.FeaturedSponsors orderby w.FeatureID, w.ProfileID descending select w).ToList();
        }
        //List Pagings
        public List<FeaturedSponsor> GetFeaturedSponsorsAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<FeaturedSponsor>();
            if (filter != null)
            {
                xList = (from w in context.FeaturedSponsors orderby w.FeatureID, w.ProfileID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.FeaturedSponsors orderby w.FeatureID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<FeaturedSponsor> GetFeaturedSponsorsAllAsQueryable()
        {
            return (from w in context.FeaturedSponsors.Include("Profile").Include("Feature") orderby w.FeatureID, w.ProfileID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<FeaturedSponsor> GetFeaturedSponsorsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<FeaturedSponsor> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.FeaturedSponsors orderby w.FeaturedSponsorID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.FeaturedSponsors.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.FeaturedSponsors orderby w.FeaturedSponsorID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.FeaturedSponsors.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<FeaturedSponsor> GetFeaturedSponsorsByFeatureID(int FeatureID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.FeaturedSponsors where w.FeatureID == FeatureID orderby w.FeatureID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<FeaturedSponsor> GetFeaturedSponsorsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.FeaturedSponsors where w.ProfileID == ProfileID orderby w.FeatureID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<FeaturedSponsor> GetFeaturedSponsorsByFeatureIDAsQueryable(int FeatureID)
        {
            return (from w in context.FeaturedSponsors where w.FeatureID == FeatureID orderby w.FeatureID, w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<FeaturedSponsor> GetFeaturedSponsorsByFeatureIDAsQueryable(int FeatureID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.FeaturedSponsors where w.FeatureID == FeatureID orderby w.FeatureID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<FeaturedSponsor> GetFeaturedSponsorsByProfileIDAsQueryable(int ProfileID)
        {
            return (from w in context.FeaturedSponsors where w.ProfileID == ProfileID orderby w.FeatureID, w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<FeaturedSponsor> GetFeaturedSponsorsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.FeaturedSponsors where w.ProfileID == ProfileID orderby w.FeatureID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}