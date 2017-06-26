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
	public class LinkTypeRepository : ILinkTypeRepository, IDisposable
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
		public LinkType Insert(String LinkTypeName, Boolean LinkTypeStatus, String LinkTypeImage, String LinkTypeDescription)
		{
            try{
                LinkType obj = new LinkType();	
                
                
            		obj.LinkTypeName = LinkTypeName;			
            		obj.LinkTypeStatus = LinkTypeStatus;			
            		obj.LinkTypeImage = LinkTypeImage;			
            		obj.LinkTypeDescription = LinkTypeDescription;			
    			context.LinkTypes.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new LinkType();
            }
		}

		public bool Update(int LinkTypeID, String LinkTypeName, Boolean LinkTypeStatus, String LinkTypeImage, String LinkTypeDescription)
		{
		    LinkType obj = new LinkType();
            obj = (from w in context.LinkTypes where w.LinkTypeID == LinkTypeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.LinkTypes.Attach(obj);
	
        		obj.LinkTypeName = LinkTypeName;			
        		obj.LinkTypeStatus = LinkTypeStatus;			
        		obj.LinkTypeImage = LinkTypeImage;			
        		obj.LinkTypeDescription = LinkTypeDescription;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int LinkTypeID)
		{
			LinkType obj = new LinkType();
			obj = (from w in context.LinkTypes where  w.LinkTypeID == LinkTypeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.LinkTypes.Attach(obj);
			    context.LinkTypes.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public LinkType GetLinkType(int LinkTypeID)
		{
			return (from w in context.LinkTypes where  w.LinkTypeID == LinkTypeID  select w).FirstOrDefault();
		}

        //Lists
		public List<LinkType> GetLinkTypesAll()
		{
			return (from w in context.LinkTypes orderby w.LinkTypeID descending select w).ToList();
		}
		public List<LinkType> GetLinkTypesAll(bool LinkTypeStatus)
		{
			return (from w in context.LinkTypes where w.LinkTypeStatus == LinkTypeStatus orderby w.LinkTypeID descending select w).ToList();
		}
        //List Pagings
        public List<LinkType> GetLinkTypesAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<LinkType>();
            if (filter != null)
            {
                xList = (from w in context.LinkTypes orderby w.LinkTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.LinkTypes orderby w.LinkTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
		public List<LinkType> GetLinkTypesAll(bool LinkTypeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			            var xList = new List<LinkType>();
            if (filter != null)
            {
                xList = (from w in context.LinkTypes where w.LinkTypeStatus == LinkTypeStatus  orderby w.LinkTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.LinkTypes where w.LinkTypeStatus == LinkTypeStatus orderby w.LinkTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList; 
		}
        //IQueryable
		public IQueryable<LinkType> GetLinkTypesAllAsQueryable()
		{
			return (from w in context.LinkTypes orderby w.LinkTypeID descending select w).AsQueryable();
		}
		public IQueryable<LinkType> GetLinkTypesAllAsQueryable(bool LinkTypeStatus)
		{
			return (from w in context.LinkTypes where w.LinkTypeStatus == LinkTypeStatus orderby w.LinkTypeID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<LinkType> GetLinkTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<LinkType> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.LinkTypes orderby w.LinkTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.LinkTypes.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.LinkTypes orderby w.LinkTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.LinkTypes.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
		public IQueryable<LinkType> GetLinkTypesAllAsQueryable(bool LinkTypeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			IQueryable<LinkType> xList;
            if (filter != null)
            {
                xList = (from w in context.LinkTypes where w.LinkTypeStatus == LinkTypeStatus orderby w.LinkTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            else {
                xList = (from w in context.LinkTypes where w.LinkTypeStatus == LinkTypeStatus orderby w.LinkTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            return xList; 
		}

	}
}