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
	public class ExternalLinkRepository : IExternalLinkRepository, IDisposable
	{
     	// To detect redundant calls
		private bool disposedValue = false;

		private GorgiasEntities context = new GorgiasEntities();

		// IDisposable
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue) {
				if (!this.disposedValue) {
					if (disposing) {
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
		public ExternalLink Insert(int LinkTypeID, int ProfileID, String ExternalLinkURL)
		{
            try{
                ExternalLink obj = new ExternalLink();	
                obj.LinkTypeID = LinkTypeID;			
                obj.ProfileID = ProfileID;			
                
                
            		obj.ExternalLinkURL = ExternalLinkURL;			
    			context.ExternalLinks.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new ExternalLink();
            }
		}

		public bool Update(int LinkTypeID, int ProfileID, String ExternalLinkURL)
		{
		    ExternalLink obj = new ExternalLink();
            obj = (from w in context.ExternalLinks where w.LinkTypeID == LinkTypeID  && w.ProfileID == ProfileID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.ExternalLinks.Attach(obj);
	
        		obj.ExternalLinkURL = ExternalLinkURL;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int LinkTypeID, int ProfileID)
		{
			ExternalLink obj = new ExternalLink();
			obj = (from w in context.ExternalLinks where  w.LinkTypeID == LinkTypeID  && w.ProfileID == ProfileID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.ExternalLinks.Attach(obj);
			    context.ExternalLinks.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public ExternalLink GetExternalLink(int LinkTypeID, int ProfileID)
		{
			return (from w in context.ExternalLinks where  w.LinkTypeID == LinkTypeID  && w.ProfileID == ProfileID  select w).FirstOrDefault();
		}

        //Lists
		public List<ExternalLink> GetExternalLinksAll()
		{
			return (from w in context.ExternalLinks orderby w.LinkTypeID, w.ProfileID descending select w).ToList();
		}
        //List Pagings
        public List<ExternalLink> GetExternalLinksAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<ExternalLink>();
            if (filter != null)
            {
                xList = (from w in context.ExternalLinks orderby w.LinkTypeID, w.ProfileID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.ExternalLinks orderby w.LinkTypeID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
        //IQueryable
		public IQueryable<ExternalLink> GetExternalLinksAllAsQueryable()
		{
			return (from w in context.ExternalLinks.Include("Profile").Include("LinkType") orderby w.LinkTypeID, w.ProfileID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<ExternalLink> GetExternalLinksAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<ExternalLink> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ExternalLinks orderby w.ExternalLinkID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.ExternalLinks.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ExternalLinks orderby w.ExternalLinkID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.ExternalLinks.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<ExternalLink> GetExternalLinksByLinkTypeID(int LinkTypeID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ExternalLinks where w.LinkTypeID == LinkTypeID orderby w.LinkTypeID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ExternalLink> GetExternalLinksByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ExternalLinks where w.ProfileID == ProfileID orderby w.LinkTypeID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<ExternalLink> GetExternalLinksByLinkTypeIDAsQueryable(int LinkTypeID)
		{
			return (from w in context.ExternalLinks where w.LinkTypeID == LinkTypeID orderby w.LinkTypeID, w.ProfileID descending select w).AsQueryable();
		}
        public IQueryable<ExternalLink> GetExternalLinksByLinkTypeIDAsQueryable(int LinkTypeID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ExternalLinks where w.LinkTypeID == LinkTypeID orderby w.LinkTypeID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ExternalLink> GetExternalLinksByProfileIDAsQueryable(int ProfileID)
		{
			return (from w in context.ExternalLinks.Include("Profile").Include("LinkType") where w.ProfileID == ProfileID orderby w.LinkTypeID, w.ProfileID descending select w).AsQueryable();
		}
        public IQueryable<ExternalLink> GetExternalLinksByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ExternalLinks.Include("LinkType") where w.ProfileID == ProfileID orderby w.LinkTypeID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

	}
}