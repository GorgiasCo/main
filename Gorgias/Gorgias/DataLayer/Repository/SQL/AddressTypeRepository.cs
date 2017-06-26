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
	public class AddressTypeRepository : IAddressTypeRepository, IDisposable
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
		public AddressType Insert(String AddressTypeName, String AddressTypeImage, Boolean AddressTypeStatus)
		{
            try{
                AddressType obj = new AddressType();	
                
                
            		obj.AddressTypeName = AddressTypeName;			
            		obj.AddressTypeImage = AddressTypeImage;			
            		obj.AddressTypeStatus = AddressTypeStatus;			
    			context.AddressTypes.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new AddressType();
            }
		}

		public bool Update(int AddressTypeID, String AddressTypeName, String AddressTypeImage, Boolean AddressTypeStatus)
		{
		    AddressType obj = new AddressType();
            obj = (from w in context.AddressTypes where w.AddressTypeID == AddressTypeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.AddressTypes.Attach(obj);
	
        		obj.AddressTypeName = AddressTypeName;			
        		obj.AddressTypeImage = AddressTypeImage;			
        		obj.AddressTypeStatus = AddressTypeStatus;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int AddressTypeID)
		{
			AddressType obj = new AddressType();
			obj = (from w in context.AddressTypes where  w.AddressTypeID == AddressTypeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.AddressTypes.Attach(obj);
			    context.AddressTypes.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public AddressType GetAddressType(int AddressTypeID)
		{
			return (from w in context.AddressTypes where  w.AddressTypeID == AddressTypeID  select w).FirstOrDefault();
		}

        //Lists
		public List<AddressType> GetAddressTypesAll()
		{
			return (from w in context.AddressTypes orderby w.AddressTypeID descending select w).ToList();
		}
		public List<AddressType> GetAddressTypesAll(bool AddressTypeStatus)
		{
			return (from w in context.AddressTypes where w.AddressTypeStatus == AddressTypeStatus orderby w.AddressTypeID descending select w).ToList();
		}
        //List Pagings
        public List<AddressType> GetAddressTypesAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<AddressType>();
            if (filter != null)
            {
                xList = (from w in context.AddressTypes orderby w.AddressTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.AddressTypes orderby w.AddressTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
		public List<AddressType> GetAddressTypesAll(bool AddressTypeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			            var xList = new List<AddressType>();
            if (filter != null)
            {
                xList = (from w in context.AddressTypes where w.AddressTypeStatus == AddressTypeStatus  orderby w.AddressTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.AddressTypes where w.AddressTypeStatus == AddressTypeStatus orderby w.AddressTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList; 
		}
        //IQueryable
		public IQueryable<AddressType> GetAddressTypesAllAsQueryable()
		{
			return (from w in context.AddressTypes orderby w.AddressTypeID descending select w).AsQueryable();
		}
		public IQueryable<AddressType> GetAddressTypesAllAsQueryable(bool AddressTypeStatus)
		{
			return (from w in context.AddressTypes where w.AddressTypeStatus == AddressTypeStatus orderby w.AddressTypeID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<AddressType> GetAddressTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<AddressType> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.AddressTypes orderby w.AddressTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.AddressTypes.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.AddressTypes orderby w.AddressTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.AddressTypes.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
		public IQueryable<AddressType> GetAddressTypesAllAsQueryable(bool AddressTypeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			IQueryable<AddressType> xList;
            if (filter != null)
            {
                xList = (from w in context.AddressTypes where w.AddressTypeStatus == AddressTypeStatus orderby w.AddressTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            else {
                xList = (from w in context.AddressTypes where w.AddressTypeStatus == AddressTypeStatus orderby w.AddressTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            return xList; 
		}

	}
}