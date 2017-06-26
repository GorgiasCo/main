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
	public class SocialNetworkRepository : ISocialNetworkRepository, IDisposable
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
		public SocialNetwork Insert(String SocialNetworkName, Boolean SocialNetworkStatus, String SocialNetworkURL, String SocialNetworkImage, String SocialNetworkDescription)
		{
            try{
                SocialNetwork obj = new SocialNetwork();	
                
                
            		obj.SocialNetworkName = SocialNetworkName;			
            		obj.SocialNetworkStatus = SocialNetworkStatus;			
            		obj.SocialNetworkURL = SocialNetworkURL;			
            		obj.SocialNetworkImage = SocialNetworkImage;			
            		obj.SocialNetworkDescription = SocialNetworkDescription;			
    			context.SocialNetworks.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new SocialNetwork();
            }
		}

		public bool Update(int SocialNetworkID, String SocialNetworkName, Boolean SocialNetworkStatus, String SocialNetworkURL, String SocialNetworkImage, String SocialNetworkDescription)
		{
		    SocialNetwork obj = new SocialNetwork();
            obj = (from w in context.SocialNetworks where w.SocialNetworkID == SocialNetworkID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.SocialNetworks.Attach(obj);
	
        		obj.SocialNetworkName = SocialNetworkName;			
        		obj.SocialNetworkStatus = SocialNetworkStatus;			
        		obj.SocialNetworkURL = SocialNetworkURL;			
        		obj.SocialNetworkImage = SocialNetworkImage;			
        		obj.SocialNetworkDescription = SocialNetworkDescription;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int SocialNetworkID)
		{
			SocialNetwork obj = new SocialNetwork();
			obj = (from w in context.SocialNetworks where  w.SocialNetworkID == SocialNetworkID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.SocialNetworks.Attach(obj);
			    context.SocialNetworks.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public SocialNetwork GetSocialNetwork(int SocialNetworkID)
		{
			return (from w in context.SocialNetworks where  w.SocialNetworkID == SocialNetworkID  select w).FirstOrDefault();
		}

        //Lists
		public List<SocialNetwork> GetSocialNetworksAll()
		{
			return (from w in context.SocialNetworks orderby w.SocialNetworkID descending select w).ToList();
		}
		public List<SocialNetwork> GetSocialNetworksAll(bool SocialNetworkStatus)
		{
			return (from w in context.SocialNetworks where w.SocialNetworkStatus == SocialNetworkStatus orderby w.SocialNetworkID descending select w).ToList();
		}
        //List Pagings
        public List<SocialNetwork> GetSocialNetworksAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<SocialNetwork>();
            if (filter != null)
            {
                xList = (from w in context.SocialNetworks orderby w.SocialNetworkID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.SocialNetworks orderby w.SocialNetworkID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
		public List<SocialNetwork> GetSocialNetworksAll(bool SocialNetworkStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			            var xList = new List<SocialNetwork>();
            if (filter != null)
            {
                xList = (from w in context.SocialNetworks where w.SocialNetworkStatus == SocialNetworkStatus  orderby w.SocialNetworkID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.SocialNetworks where w.SocialNetworkStatus == SocialNetworkStatus orderby w.SocialNetworkID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList; 
		}
        //IQueryable
		public IQueryable<SocialNetwork> GetSocialNetworksAllAsQueryable()
		{
			return (from w in context.SocialNetworks orderby w.SocialNetworkID descending select w).AsQueryable();
		}
		public IQueryable<SocialNetwork> GetSocialNetworksAllAsQueryable(bool SocialNetworkStatus)
		{
			return (from w in context.SocialNetworks where w.SocialNetworkStatus == SocialNetworkStatus orderby w.SocialNetworkID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<SocialNetwork> GetSocialNetworksAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<SocialNetwork> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.SocialNetworks orderby w.SocialNetworkID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.SocialNetworks.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.SocialNetworks orderby w.SocialNetworkID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.SocialNetworks.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
		public IQueryable<SocialNetwork> GetSocialNetworksAllAsQueryable(bool SocialNetworkStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			IQueryable<SocialNetwork> xList;
            if (filter != null)
            {
                xList = (from w in context.SocialNetworks where w.SocialNetworkStatus == SocialNetworkStatus orderby w.SocialNetworkID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            else {
                xList = (from w in context.SocialNetworks where w.SocialNetworkStatus == SocialNetworkStatus orderby w.SocialNetworkID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            return xList; 
		}

	}
}