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
	public class AttributeRepository : IAttributeRepository, IDisposable
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
		public Attribute Insert(String AttributeName, String AttributeCaption, Boolean AttributeStatus, int AttributeMode, String AttributeRule, String AttributeType, String AttributeImage, String AttributeDescription)
		{
            try{
                Attribute obj = new Attribute();	
                
                
            		obj.AttributeName = AttributeName;			
            		obj.AttributeCaption = AttributeCaption;			
            		obj.AttributeStatus = AttributeStatus;			
            		obj.AttributeMode = AttributeMode;			
            		obj.AttributeRule = AttributeRule;			
            		obj.AttributeType = AttributeType;			
            		obj.AttributeImage = AttributeImage;			
            		obj.AttributeDescription = AttributeDescription;			
    			context.Attributes.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new Attribute();
            }
		}

		public bool Update(int AttributeID, String AttributeName, String AttributeCaption, Boolean AttributeStatus, int AttributeMode, String AttributeRule, String AttributeType, String AttributeImage, String AttributeDescription)
		{
		    Attribute obj = new Attribute();
            obj = (from w in context.Attributes where w.AttributeID == AttributeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.Attributes.Attach(obj);
	
        		obj.AttributeName = AttributeName;			
        		obj.AttributeCaption = AttributeCaption;			
        		obj.AttributeStatus = AttributeStatus;			
        		obj.AttributeMode = AttributeMode;			
        		obj.AttributeRule = AttributeRule;			
        		obj.AttributeType = AttributeType;			
        		obj.AttributeImage = AttributeImage;			
        		obj.AttributeDescription = AttributeDescription;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int AttributeID)
		{
			Attribute obj = new Attribute();
			obj = (from w in context.Attributes where  w.AttributeID == AttributeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.Attributes.Attach(obj);
			    context.Attributes.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public Attribute GetAttribute(int AttributeID)
		{
			return (from w in context.Attributes where  w.AttributeID == AttributeID  select w).FirstOrDefault();
		}

        //Lists
		public List<Attribute> GetAttributesAll()
		{
			return (from w in context.Attributes orderby w.AttributeID descending select w).ToList();
		}
		public List<Attribute> GetAttributesAll(bool AttributeStatus)
		{
			return (from w in context.Attributes where w.AttributeStatus == AttributeStatus orderby w.AttributeID descending select w).ToList();
		}
        //List Pagings
        public List<Attribute> GetAttributesAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<Attribute>();
            if (filter != null)
            {
                xList = (from w in context.Attributes orderby w.AttributeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.Attributes orderby w.AttributeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
		public List<Attribute> GetAttributesAll(bool AttributeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			            var xList = new List<Attribute>();
            if (filter != null)
            {
                xList = (from w in context.Attributes where w.AttributeStatus == AttributeStatus  orderby w.AttributeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.Attributes where w.AttributeStatus == AttributeStatus orderby w.AttributeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList; 
		}
        //IQueryable
		public IQueryable<Attribute> GetAttributesAllAsQueryable()
		{
			return (from w in context.Attributes orderby w.AttributeID descending select w).AsQueryable();
		}
		public IQueryable<Attribute> GetAttributesAllAsQueryable(bool AttributeStatus)
		{
			return (from w in context.Attributes where w.AttributeStatus == AttributeStatus orderby w.AttributeID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<Attribute> GetAttributesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<Attribute> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Attributes orderby w.AttributeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.Attributes.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Attributes orderby w.AttributeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.Attributes.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
		public IQueryable<Attribute> GetAttributesAllAsQueryable(bool AttributeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			IQueryable<Attribute> xList;
            if (filter != null)
            {
                xList = (from w in context.Attributes where w.AttributeStatus == AttributeStatus orderby w.AttributeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            else {
                xList = (from w in context.Attributes where w.AttributeStatus == AttributeStatus orderby w.AttributeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            return xList; 
		}

	}
}