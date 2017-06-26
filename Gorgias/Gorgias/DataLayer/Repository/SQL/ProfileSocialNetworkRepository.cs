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
	public class ProfileSocialNetworkRepository : IProfileSocialNetworkRepository, IDisposable
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
		public ProfileSocialNetwork Insert(int SocialNetworkID, int ProfileID, String ProfileSocialNetworkURL)
		{
            try{
                ProfileSocialNetwork obj = new ProfileSocialNetwork();	
                obj.SocialNetworkID = SocialNetworkID;			
                obj.ProfileID = ProfileID;			
                
                
            		obj.ProfileSocialNetworkURL = ProfileSocialNetworkURL;			
    			context.ProfileSocialNetworks.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new ProfileSocialNetwork();
            }
		}

		public bool Update(int SocialNetworkID, int ProfileID, String ProfileSocialNetworkURL)
		{
		    ProfileSocialNetwork obj = new ProfileSocialNetwork();
            obj = (from w in context.ProfileSocialNetworks where w.SocialNetworkID == SocialNetworkID  && w.ProfileID == ProfileID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.ProfileSocialNetworks.Attach(obj);
	
        		obj.ProfileSocialNetworkURL = ProfileSocialNetworkURL;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int SocialNetworkID, int ProfileID)
		{
			ProfileSocialNetwork obj = new ProfileSocialNetwork();
			obj = (from w in context.ProfileSocialNetworks where  w.SocialNetworkID == SocialNetworkID  && w.ProfileID == ProfileID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.ProfileSocialNetworks.Attach(obj);
			    context.ProfileSocialNetworks.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public ProfileSocialNetwork GetProfileSocialNetwork(int SocialNetworkID, int ProfileID)
		{
			return (from w in context.ProfileSocialNetworks where  w.SocialNetworkID == SocialNetworkID  && w.ProfileID == ProfileID  select w).FirstOrDefault();
		}

        //Lists
		public List<ProfileSocialNetwork> GetProfileSocialNetworksAll()
		{
			return (from w in context.ProfileSocialNetworks orderby w.SocialNetworkID, w.ProfileID descending select w).ToList();
		}
        //List Pagings
        public List<ProfileSocialNetwork> GetProfileSocialNetworksAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<ProfileSocialNetwork>();
            if (filter != null)
            {
                xList = (from w in context.ProfileSocialNetworks orderby w.SocialNetworkID, w.ProfileID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.ProfileSocialNetworks orderby w.SocialNetworkID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
        //IQueryable
		public IQueryable<ProfileSocialNetwork> GetProfileSocialNetworksAllAsQueryable()
		{
			return (from w in context.ProfileSocialNetworks.Include("Profile").Include("SocialNetwork") orderby w.SocialNetworkID, w.ProfileID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<ProfileSocialNetwork> GetProfileSocialNetworksAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<ProfileSocialNetwork> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileSocialNetworks orderby w.ProfileSocialNetworkID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.ProfileSocialNetworks.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileSocialNetworks orderby w.ProfileSocialNetworkID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.ProfileSocialNetworks.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<ProfileSocialNetwork> GetProfileSocialNetworksBySocialNetworkID(int SocialNetworkID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ProfileSocialNetworks where w.SocialNetworkID == SocialNetworkID orderby w.SocialNetworkID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ProfileSocialNetwork> GetProfileSocialNetworksByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ProfileSocialNetworks where w.ProfileID == ProfileID orderby w.SocialNetworkID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<ProfileSocialNetwork> GetProfileSocialNetworksBySocialNetworkIDAsQueryable(int SocialNetworkID)
		{
			return (from w in context.ProfileSocialNetworks where w.SocialNetworkID == SocialNetworkID orderby w.SocialNetworkID, w.ProfileID descending select w).AsQueryable();
		}
        public IQueryable<ProfileSocialNetwork> GetProfileSocialNetworksBySocialNetworkIDAsQueryable(int SocialNetworkID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ProfileSocialNetworks where w.SocialNetworkID == SocialNetworkID orderby w.SocialNetworkID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ProfileSocialNetwork> GetProfileSocialNetworksByProfileIDAsQueryable(int ProfileID)
		{
			return (from w in context.ProfileSocialNetworks.Include("Profile").Include("SocialNetwork") where w.ProfileID == ProfileID orderby w.SocialNetworkID, w.ProfileID descending select w).AsQueryable();
		}
        public IQueryable<ProfileSocialNetwork> GetProfileSocialNetworksByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.ProfileSocialNetworks where w.ProfileID == ProfileID orderby w.SocialNetworkID, w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

	}
}