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
	public class CityRepository : ICityRepository, IDisposable
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
		public City Insert(String CityName, Boolean CityStatus, int CountryID, byte[] CityUpdateDate)
		{
            try{
                City obj = new City();	
                
                
            		obj.CityName = CityName;			
            		obj.CityStatus = CityStatus;			
            		obj.CountryID = CountryID;			
            		obj.CityUpdateDate = CityUpdateDate;			
    			context.Cities.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new City();
            }
		}

		public bool Update(int CityID, String CityName, Boolean CityStatus, int CountryID, byte[] CityUpdateDate)
		{
		    City obj = new City();
            obj = (from w in context.Cities where w.CityID == CityID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.Cities.Attach(obj);
	
        		obj.CityName = CityName;			
        		obj.CityStatus = CityStatus;			
        		obj.CountryID = CountryID;			
        		obj.CityUpdateDate = CityUpdateDate;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int CityID)
		{
			City obj = new City();
			obj = (from w in context.Cities where  w.CityID == CityID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.Cities.Attach(obj);
			    context.Cities.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public City GetCity(int CityID)
		{
			return (from w in context.Cities where  w.CityID == CityID  select w).FirstOrDefault();
		}

        public City GetCity(string CityName)
        {
            return (from w in context.Cities where w.CityName == CityName select w).FirstOrDefault();
        }

        //Lists
        public List<City> GetCitiesAll()
		{
			return (from w in context.Cities orderby w.CityID descending select w).ToList();
		}
		public List<City> GetCitiesAll(bool CityStatus)
		{
			return (from w in context.Cities where w.CityStatus == CityStatus orderby w.CityID descending select w).ToList();
		}
        //List Pagings
        public List<City> GetCitiesAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<City>();
            if (filter != null)
            {
                xList = (from w in context.Cities orderby w.CityID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.Cities orderby w.CityID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
		public List<City> GetCitiesAll(bool CityStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			            var xList = new List<City>();
            if (filter != null)
            {
                xList = (from w in context.Cities where w.CityStatus == CityStatus  orderby w.CityID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.Cities where w.CityStatus == CityStatus orderby w.CityID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList; 
		}
        //IQueryable
		public IQueryable<City> GetCitiesAllAsQueryable()
		{
			return (from w in context.Cities orderby w.CityID descending select w).AsQueryable();
		}
		public IQueryable<City> GetCitiesAllAsQueryable(bool CityStatus)
		{
			return (from w in context.Cities where w.CityStatus == CityStatus orderby w.CityID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<City> GetCitiesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<City> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Citys orderby w.CityID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.Cities.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Cities orderby w.CityID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.Cities.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
		public IQueryable<City> GetCitiesAllAsQueryable(bool CityStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			IQueryable<City> xList;
            if (filter != null)
            {
                xList = (from w in context.Cities where w.CityStatus == CityStatus orderby w.CityID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            else {
                xList = (from w in context.Cities where w.CityStatus == CityStatus orderby w.CityID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            return xList; 
		}
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<City> GetCitiesByCountryID(int CountryID, bool CityStatus)
		{
			return (from w in context.Cities where w.CountryID == CountryID && w.CityStatus == CityStatus orderby w.CityID descending select w).ToList();
		}
        public List<City> GetCitiesByCountryID(int CountryID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Cities where w.CountryID == CountryID orderby w.CityID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<City> GetCitiesByCountryID(int CountryID, bool CityStatus, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Cities where w.CountryID == CountryID && w.CityStatus == CityStatus orderby w.CityID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<City> GetCitiesByCountryIDAsQueryable(int CountryID)
		{
			return (from w in context.Cities where w.CountryID == CountryID orderby w.CityID descending select w).AsQueryable();
		}
        public IQueryable<City> GetCitiesByCountryIDAsQueryable(int CountryID, bool CityStatus)
		{
			return (from w in context.Cities where w.CountryID == CountryID && w.CityStatus == CityStatus orderby w.CityID descending select w).AsQueryable();
		}
        public IQueryable<City> GetCitiesByCountryIDAsQueryable(int CountryID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Cities where w.CountryID == CountryID orderby w.CityID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<City> GetCitiesByCountryIDAsQueryable(int CountryID, bool CityStatus, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Cities where w.CountryID == CountryID && w.CityStatus == CityStatus  orderby w.CityID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

	}
}