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
	public class ProfileTypeRepository : IProfileTypeRepository, IDisposable
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
		public ProfileType Insert(String ProfileTypeName, Boolean ProfileTypeStatus, String ProfileTypeImage, String ProfileTypeDescription, int ProfileTypeParentID)
		{
            try{
                ProfileType obj = new ProfileType();	
                
                
            		obj.ProfileTypeName = ProfileTypeName;			
            		obj.ProfileTypeStatus = ProfileTypeStatus;			
            		obj.ProfileTypeImage = ProfileTypeImage;			
            		obj.ProfileTypeDescription = ProfileTypeDescription;			
            		obj.ProfileTypeParentID = ProfileTypeParentID;			
    			context.ProfileTypes.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new ProfileType();
            }
		}

		public bool Update(int ProfileTypeID, String ProfileTypeName, Boolean ProfileTypeStatus, String ProfileTypeImage, String ProfileTypeDescription, int ProfileTypeParentID)
		{
		    ProfileType obj = new ProfileType();
            obj = (from w in context.ProfileTypes where w.ProfileTypeID == ProfileTypeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.ProfileTypes.Attach(obj);
	
        		obj.ProfileTypeName = ProfileTypeName;			
        		obj.ProfileTypeStatus = ProfileTypeStatus;			
        		obj.ProfileTypeImage = ProfileTypeImage;			
        		obj.ProfileTypeDescription = ProfileTypeDescription;			
        		obj.ProfileTypeParentID = ProfileTypeParentID;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int ProfileTypeID)
		{
			ProfileType obj = new ProfileType();
			obj = (from w in context.ProfileTypes where  w.ProfileTypeID == ProfileTypeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.ProfileTypes.Attach(obj);
			    context.ProfileTypes.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public ProfileType GetProfileType(int ProfileTypeID)
		{
			return (from w in context.ProfileTypes where  w.ProfileTypeID == ProfileTypeID  select w).FirstOrDefault();
		}

        //Lists
		public List<ProfileType> GetProfileTypesAll()
		{
			return (from w in context.ProfileTypes orderby w.ProfileTypeID descending select w).ToList();
		}
		public List<ProfileType> GetProfileTypesAll(bool ProfileTypeStatus)
		{
			return (from w in context.ProfileTypes where w.ProfileTypeStatus == ProfileTypeStatus orderby w.ProfileTypeID descending select w).ToList();
		}
        //List Pagings
        public List<ProfileType> GetProfileTypesAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<ProfileType>();
            if (filter != null)
            {
                xList = (from w in context.ProfileTypes orderby w.ProfileTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.ProfileTypes orderby w.ProfileTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
		public List<ProfileType> GetProfileTypesAll(bool ProfileTypeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			            var xList = new List<ProfileType>();
            if (filter != null)
            {
                xList = (from w in context.ProfileTypes where w.ProfileTypeStatus == ProfileTypeStatus  orderby w.ProfileTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.ProfileTypes where w.ProfileTypeStatus == ProfileTypeStatus orderby w.ProfileTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList; 
		}
        //IQueryable
		public IQueryable<ProfileType> GetProfileTypesAllAsQueryable()
		{
			return (from w in context.ProfileTypes orderby w.ProfileTypeID descending select w).AsQueryable();
		}
		public IQueryable<ProfileType> GetProfileTypesAllAsQueryable(bool ProfileTypeStatus)
		{
			return (from w in context.ProfileTypes where w.ProfileTypeStatus == ProfileTypeStatus orderby w.ProfileTypeID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<ProfileType> GetProfileTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<ProfileType> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileTypes orderby w.ProfileTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.ProfileTypes.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ProfileTypes orderby w.ProfileTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.ProfileTypes.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
		public IQueryable<ProfileType> GetProfileTypesAllAsQueryable(bool ProfileTypeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			IQueryable<ProfileType> xList;
            if (filter != null)
            {
                xList = (from w in context.ProfileTypes where w.ProfileTypeStatus == ProfileTypeStatus orderby w.ProfileTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            else {
                xList = (from w in context.ProfileTypes where w.ProfileTypeStatus == ProfileTypeStatus orderby w.ProfileTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            return xList; 
		}

	}
}