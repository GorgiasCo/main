using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface ICityRepository
    {
    
    
        City Insert(String CityName, Boolean CityStatus, int CountryID, byte[] CityUpdateDate);
        bool Update(int CityID, String CityName, Boolean CityStatus, int CountryID, byte[] CityUpdateDate);        
        bool Delete(int CityID);

        City GetCity(int CityID);
        City GetCity(string CityName);

        //List
        List<City> GetCitiesAll();
        List<City> GetCitiesAll(bool CityStatus);
        List<City> GetCitiesAll(int page = 1, int pageSize = 7, string filter=null);
        List<City> GetCitiesAll(bool CityStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        List<City> GetCitiesByCountryID(int CountryID, bool CityStatus);
        List<City> GetCitiesByCountryID(int CountryID, int page = 1, int pageSize = 7, string filter=null);       
        List<City> GetCitiesByCountryID(int CountryID, bool CityStatus, int page = 1, int pageSize = 7, string filter=null);               
        
        //IQueryable
        IQueryable<City> GetCitiesAllAsQueryable();
        IQueryable<City> GetCitiesAllAsQueryable(bool CityStatus);
        IQueryable<City> GetCitiesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<City> GetCitiesAllAsQueryable(bool CityStatus, int page = 1, int pageSize = 7, string filter=null);   
        IQueryable<City> GetCitiesByCountryIDAsQueryable(int CountryID);
        IQueryable<City> GetCitiesByCountryIDAsQueryable(int CountryID, bool CityStatus);
        IQueryable<City> GetCitiesByCountryIDAsQueryable(int CountryID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<City> GetCitiesByCountryIDAsQueryable(int CountryID, bool CityStatus, int page = 1, int pageSize = 7, string filter=null);               
    }
}


