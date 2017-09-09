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
    public class CountryRepository : ICountryRepository, IDisposable
    {
        // To detect redundant calls
        private bool disposedValue = false;

        private GorgiasEntities context = new GorgiasEntities();

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (!this.disposedValue)
                {
                    if (disposing)
                    {
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
        public Country Insert(String CountryName, String CountryShortName, Boolean CountryStatus, String CountryPhoneCode, String CountryImage, String CountryDescription, int? CountryParentID, string CountryLanguageCode)
        {
            try
            {
                Country obj = new Country();
                obj.CountryName = CountryName;
                obj.CountryShortName = CountryShortName;
                obj.CountryStatus = CountryStatus;
                obj.CountryPhoneCode = CountryPhoneCode;
                obj.CountryImage = CountryImage;
                obj.CountryDescription = CountryDescription;

                if (CountryParentID.HasValue)
                {
                    obj.CountryParentID = CountryParentID;
                }
                else
                {
                    obj.CountryParentID = null;
                }
                obj.CountryLanguageCode = CountryLanguageCode;

                context.Countries.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Country();
            }
        }

        public bool Update(int CountryID, String CountryName, String CountryShortName, Boolean CountryStatus, String CountryPhoneCode, String CountryImage, String CountryDescription, int? CountryParentID, string CountryLanguageCode)
        {
            Country obj = new Country();
            obj = (from w in context.Countries where w.CountryID == CountryID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Countries.Attach(obj);

                obj.CountryName = CountryName;
                obj.CountryShortName = CountryShortName;
                obj.CountryStatus = CountryStatus;
                obj.CountryPhoneCode = CountryPhoneCode;
                obj.CountryImage = CountryImage;
                obj.CountryDescription = CountryDescription;

                if (CountryParentID.HasValue)
                {
                    obj.CountryParentID = CountryParentID;
                }
                else
                {
                    obj.CountryParentID = null;
                }
                obj.CountryLanguageCode = CountryLanguageCode;

                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int CountryID)
        {
            Country obj = new Country();
            obj = (from w in context.Countries where w.CountryID == CountryID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Countries.Attach(obj);
                context.Countries.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Country GetCountry(int CountryID)
        {
            return (from w in context.Countries where w.CountryID == CountryID select w).FirstOrDefault();
        }

        //Lists
        public List<Country> GetCountriesAll()
        {
            return (from w in context.Countries orderby w.CountryID descending select w).ToList();
        }
        public List<Country> GetCountriesAll(bool CountryStatus)
        {
            return (from w in context.Countries where w.CountryStatus == CountryStatus orderby w.CountryID descending select w).ToList();
        }
        //List Pagings
        public List<Country> GetCountriesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Country>();
            if (filter != null)
            {
                xList = (from w in context.Countries orderby w.CountryID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Countries orderby w.CountryID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<Country> GetCountriesAll(bool CountryStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Country>();
            if (filter != null)
            {
                xList = (from w in context.Countries where w.CountryStatus == CountryStatus orderby w.CountryID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Countries where w.CountryStatus == CountryStatus orderby w.CountryID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<Country> GetCountriesAllAsQueryable()
        {
            return (from w in context.Countries orderby w.CountryID descending select w).AsQueryable();
        }

        public IQueryable<Country> GetCountriesAllAsQueryable(bool CountryStatus)
        {
            return (from w in context.Countries where w.CountryStatus == CountryStatus orderby w.CountryID descending select w).AsQueryable();
        }
        public IQueryable<Business.DataTransferObjects.CountryDTO> GetCountriesAllAsQueryable(string languageCode)
        {
            return (from w in context.Countries
                    where w.CountryParentID == null && w.CountryStatus == true
                    orderby w.CountryName ascending
                    select new Business.DataTransferObjects.CountryDTO
                    {
                         CountryName = w.CountryName,
                         CountryID = w.CountryID,
                         Multilanguage = w.CountryChilds.Where(m=> m.CountryLanguageCode == languageCode).FirstOrDefault().CountryName
                    }).AsQueryable();
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.CountryMobileModel> GetCountriesAsQueryable(string languageCode)
        {
            return (from w in context.Countries
                    where w.CountryParentID == null && w.CountryStatus == true
                    orderby w.CountryName ascending
                    select new Business.DataTransferObjects.Mobile.V2.CountryMobileModel
                    {
                        CountryName = w.CountryName,
                        CountryID = w.CountryID,
                        Multilanguage = w.CountryChilds.Where(m => m.CountryLanguageCode == languageCode).FirstOrDefault().CountryName
                    }).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<Country> GetCountriesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Country> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Countrys orderby w.CountryID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Countries.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Countries orderby w.CountryID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Countries.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<Country> GetCountriesAllAsQueryable(bool CountryStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Country> xList;
            if (filter != null)
            {
                xList = (from w in context.Countries where w.CountryStatus == CountryStatus orderby w.CountryID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Countries where w.CountryStatus == CountryStatus orderby w.CountryID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }

    }
}