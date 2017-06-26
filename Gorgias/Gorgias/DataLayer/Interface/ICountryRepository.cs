using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface ICountryRepository
    {
    
    
        Country Insert(String CountryName, String CountryShortName, Boolean CountryStatus, String CountryPhoneCode, String CountryImage, String CountryDescription);
        bool Update(int CountryID, String CountryName, String CountryShortName, Boolean CountryStatus, String CountryPhoneCode, String CountryImage, String CountryDescription);        
        bool Delete(int CountryID);

        Country GetCountry(int CountryID);

        //List
        List<Country> GetCountriesAll();
        List<Country> GetCountriesAll(bool CountryStatus);
        List<Country> GetCountriesAll(int page = 1, int pageSize = 7, string filter=null);
        List<Country> GetCountriesAll(bool CountryStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<Country> GetCountriesAllAsQueryable();
        IQueryable<Country> GetCountriesAllAsQueryable(bool CountryStatus);
        IQueryable<Country> GetCountriesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<Country> GetCountriesAllAsQueryable(bool CountryStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}


