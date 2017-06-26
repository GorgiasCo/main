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
	public class RequestTypeRepository : IRequestTypeRepository, IDisposable
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
		public RequestType Insert(String RequestTypeName, Boolean RequestTypeStatus, Boolean RequestIsRestricted)
		{
            try{
                RequestType obj = new RequestType();	
                
                
            		obj.RequestTypeName = RequestTypeName;			
            		obj.RequestTypeStatus = RequestTypeStatus;			
            		obj.RequestIsRestricted = RequestIsRestricted;			
    			context.RequestTypes.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new RequestType();
            }
		}

		public bool Update(int RequestTypeID, String RequestTypeName, Boolean RequestTypeStatus, Boolean RequestIsRestricted)
		{
		    RequestType obj = new RequestType();
            obj = (from w in context.RequestTypes where w.RequestTypeID == RequestTypeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.RequestTypes.Attach(obj);
	
        		obj.RequestTypeName = RequestTypeName;			
        		obj.RequestTypeStatus = RequestTypeStatus;			
        		obj.RequestIsRestricted = RequestIsRestricted;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int RequestTypeID)
		{
			RequestType obj = new RequestType();
			obj = (from w in context.RequestTypes where  w.RequestTypeID == RequestTypeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.RequestTypes.Attach(obj);
			    context.RequestTypes.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public RequestType GetRequestType(int RequestTypeID)
		{
			return (from w in context.RequestTypes where  w.RequestTypeID == RequestTypeID  select w).FirstOrDefault();
		}

        //Lists
		public List<RequestType> GetRequestTypesAll()
		{
			return (from w in context.RequestTypes orderby w.RequestTypeID descending select w).ToList();
		}
		public List<RequestType> GetRequestTypesAll(bool RequestTypeStatus)
		{
			return (from w in context.RequestTypes where w.RequestTypeStatus == RequestTypeStatus orderby w.RequestTypeID descending select w).ToList();
		}
        //List Pagings
        public List<RequestType> GetRequestTypesAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<RequestType>();
            if (filter != null)
            {
                xList = (from w in context.RequestTypes orderby w.RequestTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.RequestTypes orderby w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
		public List<RequestType> GetRequestTypesAll(bool RequestTypeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			            var xList = new List<RequestType>();
            if (filter != null)
            {
                xList = (from w in context.RequestTypes where w.RequestTypeStatus == RequestTypeStatus  orderby w.RequestTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.RequestTypes where w.RequestTypeStatus == RequestTypeStatus orderby w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList; 
		}
        //IQueryable
		public IQueryable<RequestType> GetRequestTypesAllAsQueryable()
		{
			return (from w in context.RequestTypes orderby w.RequestTypeID descending select w).AsQueryable();
		}
		public IQueryable<RequestType> GetRequestTypesAllAsQueryable(bool RequestTypeStatus)
		{
			return (from w in context.RequestTypes where w.RequestTypeStatus == RequestTypeStatus orderby w.RequestTypeID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<RequestType> GetRequestTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<RequestType> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.RequestTypes orderby w.RequestTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.RequestTypes.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.RequestTypes orderby w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.RequestTypes.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
		public IQueryable<RequestType> GetRequestTypesAllAsQueryable(bool RequestTypeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			IQueryable<RequestType> xList;
            if (filter != null)
            {
                xList = (from w in context.RequestTypes where w.RequestTypeStatus == RequestTypeStatus orderby w.RequestTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            else {
                xList = (from w in context.RequestTypes where w.RequestTypeStatus == RequestTypeStatus orderby w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            return xList; 
		}

	}
}