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
	public class NewsletterRepository : INewsletterRepository, IDisposable
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
		public Newsletter Insert(String NewsletterName, String NewsletterNote, Boolean NewsletterStatus)
		{
            try{
                Newsletter obj = new Newsletter();	
                
                
            		obj.NewsletterName = NewsletterName;			
            		obj.NewsletterNote = NewsletterNote;			
            		obj.NewsletterStatus = NewsletterStatus;			
    			context.Newsletters.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new Newsletter();
            }
		}

		public bool Update(int NewsletterID, String NewsletterName, String NewsletterNote, Boolean NewsletterStatus)
		{
		    Newsletter obj = new Newsletter();
            obj = (from w in context.Newsletters where w.NewsletterID == NewsletterID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.Newsletters.Attach(obj);
	
        		obj.NewsletterName = NewsletterName;			
        		obj.NewsletterNote = NewsletterNote;			
        		obj.NewsletterStatus = NewsletterStatus;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int NewsletterID)
		{
			Newsletter obj = new Newsletter();
			obj = (from w in context.Newsletters where  w.NewsletterID == NewsletterID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.Newsletters.Attach(obj);
			    context.Newsletters.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public Newsletter GetNewsletter(int NewsletterID)
		{
			return (from w in context.Newsletters where  w.NewsletterID == NewsletterID  select w).FirstOrDefault();
		}

        public Newsletter GetNewsletter(string NewsletterName)
        {
            return (from w in context.Newsletters where w.NewsletterName == NewsletterName select w).FirstOrDefault();
        }

        //Lists
        public List<Newsletter> GetNewslettersAll()
		{
			return (from w in context.Newsletters orderby w.NewsletterID descending select w).ToList();
		}
		public List<Newsletter> GetNewslettersAll(bool NewsletterStatus)
		{
			return (from w in context.Newsletters where w.NewsletterStatus == NewsletterStatus orderby w.NewsletterID descending select w).ToList();
		}
        //List Pagings
        public List<Newsletter> GetNewslettersAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<Newsletter>();
            if (filter != null)
            {
                xList = (from w in context.Newsletters orderby w.NewsletterID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.Newsletters orderby w.NewsletterID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
		public List<Newsletter> GetNewslettersAll(bool NewsletterStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			            var xList = new List<Newsletter>();
            if (filter != null)
            {
                xList = (from w in context.Newsletters where w.NewsletterStatus == NewsletterStatus  orderby w.NewsletterID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.Newsletters where w.NewsletterStatus == NewsletterStatus orderby w.NewsletterID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList; 
		}
        //IQueryable
		public IQueryable<Newsletter> GetNewslettersAllAsQueryable()
		{
			return (from w in context.Newsletters orderby w.NewsletterID descending select w).AsQueryable();
		}
		public IQueryable<Newsletter> GetNewslettersAllAsQueryable(bool NewsletterStatus)
		{
			return (from w in context.Newsletters where w.NewsletterStatus == NewsletterStatus orderby w.NewsletterID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<Newsletter> GetNewslettersAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<Newsletter> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Newsletters orderby w.NewsletterID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.Newsletters.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Newsletters orderby w.NewsletterID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.Newsletters.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
		public IQueryable<Newsletter> GetNewslettersAllAsQueryable(bool NewsletterStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			IQueryable<Newsletter> xList;
            if (filter != null)
            {
                xList = (from w in context.Newsletters where w.NewsletterStatus == NewsletterStatus orderby w.NewsletterID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            else {
                xList = (from w in context.Newsletters where w.NewsletterStatus == NewsletterStatus orderby w.NewsletterID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            return xList; 
		}

	}
}