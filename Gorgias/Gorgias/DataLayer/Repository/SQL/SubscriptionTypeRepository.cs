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
	public class SubscriptionTypeRepository : ISubscriptionTypeRepository, IDisposable
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
		public SubscriptionType Insert(String SubscriptionTypeName, Decimal SubscriptionTypeFee, Boolean SubscriptionTypeStatus, String SubscriptionTypeImage, String SubscriptionTypeDescription)
		{
            try{
                SubscriptionType obj = new SubscriptionType();	
                
                
            		obj.SubscriptionTypeName = SubscriptionTypeName;			
            		obj.SubscriptionTypeFee = SubscriptionTypeFee;			
            		obj.SubscriptionTypeStatus = SubscriptionTypeStatus;			
            		obj.SubscriptionTypeImage = SubscriptionTypeImage;			
            		obj.SubscriptionTypeDescription = SubscriptionTypeDescription;			
    			context.SubscriptionTypes.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new SubscriptionType();
            }
		}

		public bool Update(int SubscriptionTypeID, String SubscriptionTypeName, Decimal SubscriptionTypeFee, Boolean SubscriptionTypeStatus, String SubscriptionTypeImage, String SubscriptionTypeDescription)
		{
		    SubscriptionType obj = new SubscriptionType();
            obj = (from w in context.SubscriptionTypes where w.SubscriptionTypeID == SubscriptionTypeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.SubscriptionTypes.Attach(obj);
	
        		obj.SubscriptionTypeName = SubscriptionTypeName;			
        		obj.SubscriptionTypeFee = SubscriptionTypeFee;			
        		obj.SubscriptionTypeStatus = SubscriptionTypeStatus;			
        		obj.SubscriptionTypeImage = SubscriptionTypeImage;			
        		obj.SubscriptionTypeDescription = SubscriptionTypeDescription;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int SubscriptionTypeID)
		{
			SubscriptionType obj = new SubscriptionType();
			obj = (from w in context.SubscriptionTypes where  w.SubscriptionTypeID == SubscriptionTypeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.SubscriptionTypes.Attach(obj);
			    context.SubscriptionTypes.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public SubscriptionType GetSubscriptionType(int SubscriptionTypeID)
		{
			return (from w in context.SubscriptionTypes where  w.SubscriptionTypeID == SubscriptionTypeID  select w).FirstOrDefault();
		}

        //Lists
		public List<SubscriptionType> GetSubscriptionTypesAll()
		{
			return (from w in context.SubscriptionTypes orderby w.SubscriptionTypeID descending select w).ToList();
		}
		public List<SubscriptionType> GetSubscriptionTypesAll(bool SubscriptionTypeStatus)
		{
			return (from w in context.SubscriptionTypes where w.SubscriptionTypeStatus == SubscriptionTypeStatus orderby w.SubscriptionTypeID descending select w).ToList();
		}
        //List Pagings
        public List<SubscriptionType> GetSubscriptionTypesAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<SubscriptionType>();
            if (filter != null)
            {
                xList = (from w in context.SubscriptionTypes orderby w.SubscriptionTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.SubscriptionTypes orderby w.SubscriptionTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
		public List<SubscriptionType> GetSubscriptionTypesAll(bool SubscriptionTypeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			            var xList = new List<SubscriptionType>();
            if (filter != null)
            {
                xList = (from w in context.SubscriptionTypes where w.SubscriptionTypeStatus == SubscriptionTypeStatus  orderby w.SubscriptionTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.SubscriptionTypes where w.SubscriptionTypeStatus == SubscriptionTypeStatus orderby w.SubscriptionTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList; 
		}
        //IQueryable
		public IQueryable<SubscriptionType> GetSubscriptionTypesAllAsQueryable()
		{
			return (from w in context.SubscriptionTypes orderby w.SubscriptionTypeID descending select w).AsQueryable();
		}
		public IQueryable<SubscriptionType> GetSubscriptionTypesAllAsQueryable(bool SubscriptionTypeStatus)
		{
			return (from w in context.SubscriptionTypes where w.SubscriptionTypeStatus == SubscriptionTypeStatus orderby w.SubscriptionTypeID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<SubscriptionType> GetSubscriptionTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<SubscriptionType> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.SubscriptionTypes orderby w.SubscriptionTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.SubscriptionTypes.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.SubscriptionTypes orderby w.SubscriptionTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.SubscriptionTypes.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
		public IQueryable<SubscriptionType> GetSubscriptionTypesAllAsQueryable(bool SubscriptionTypeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			IQueryable<SubscriptionType> xList;
            if (filter != null)
            {
                xList = (from w in context.SubscriptionTypes where w.SubscriptionTypeStatus == SubscriptionTypeStatus orderby w.SubscriptionTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            else {
                xList = (from w in context.SubscriptionTypes where w.SubscriptionTypeStatus == SubscriptionTypeStatus orderby w.SubscriptionTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            return xList; 
		}

	}
}