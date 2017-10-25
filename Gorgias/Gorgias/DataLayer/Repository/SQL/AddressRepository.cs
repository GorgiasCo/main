using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Gorgias;
using Gorgias.DataLayer.Interface;
using Gorgias.Infrastruture.EntityFramework;
using System.Linq;
using System.Data.Entity.Spatial;

namespace Gorgias.DataLayer.Repository.SQL
{
    public class AddressRepository : IAddressRepository, IDisposable
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
        public Address Insert(String AddressName, Boolean AddressStatus, String AddressTel, String AddressFax, String AddressZipCode, String AddressAddress, String AddressEmail, String AddressImage, int CityID, int ProfileID, int AddressTypeID, DbGeography AddressLocation)
        {
            try
            {
                Address obj = new Address();
                obj.AddressName = AddressName;
                obj.AddressStatus = AddressStatus;
                obj.AddressTel = AddressTel;
                obj.AddressFax = AddressFax;
                obj.AddressZipCode = AddressZipCode;
                obj.AddressAddress = AddressAddress;
                obj.AddressEmail = AddressEmail;
                obj.CityID = CityID;
                obj.ProfileID = ProfileID;
                obj.AddressTypeID = AddressTypeID;
                obj.AddressLocation = AddressLocation;
                context.Addresses.Add(obj);
                context.SaveChanges();

                obj.AddressImage = obj.AddressID + ".jpg";
                context.SaveChanges();

                return obj;
            }
            catch (Exception ex)
            {
                return new Address();
            }
        }

        public bool Update(int AddressID, String AddressName, Boolean AddressStatus, String AddressTel, String AddressFax, String AddressZipCode, String AddressAddress, String AddressEmail, String AddressImage, int CityID, int ProfileID, int AddressTypeID, DbGeography AddressLocation)
        {
            Address obj = new Address();
            obj = (from w in context.Addresses where w.AddressID == AddressID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Addresses.Attach(obj);

                obj.AddressName = AddressName;
                obj.AddressStatus = AddressStatus;
                obj.AddressTel = AddressTel;
                obj.AddressFax = AddressFax;
                obj.AddressZipCode = AddressZipCode;
                obj.AddressAddress = AddressAddress;
                obj.AddressEmail = AddressEmail;
                obj.AddressImage = AddressImage;
                obj.CityID = CityID;
                obj.ProfileID = ProfileID;
                obj.AddressTypeID = AddressTypeID;
                obj.AddressLocation = AddressLocation;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int AddressID)
        {
            Address obj = new Address();
            obj = (from w in context.Addresses where w.AddressID == AddressID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Addresses.Attach(obj);
                context.Addresses.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteByProfileID(int ProfileID)
        {
            Profile obj = new Profile();
            obj = (from w in context.Profiles.Include("Addresses") where w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Profiles.Attach(obj);

                obj.Addresses.Clear();

                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Address GetAddress(int AddressID)
        {
            return (from w in context.Addresses where w.AddressID == AddressID select w).FirstOrDefault();
        }

        //Lists
        public List<Address> GetAddressesAll()
        {
            return (from w in context.Addresses orderby w.AddressID descending select w).ToList();
        }
        public List<Address> GetAddressesAll(bool AddressStatus)
        {
            return (from w in context.Addresses where w.AddressStatus == AddressStatus orderby w.AddressID descending select w).ToList();
        }
        //List Pagings
        public List<Address> GetAddressesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Address>();
            if (filter != null)
            {
                xList = (from w in context.Addresses orderby w.AddressID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Addresses orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<Address> GetAddressesAll(bool AddressStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Address>();
            if (filter != null)
            {
                xList = (from w in context.Addresses where w.AddressStatus == AddressStatus orderby w.AddressID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Addresses where w.AddressStatus == AddressStatus orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<Address> GetAddressesAllAsQueryable()
        {
            return (from w in context.Addresses.Include("Profile").Include("AddressType") orderby w.AddressID descending select w).AsQueryable();
        }

        public IQueryable<Business.DataTransferObjects.Mobile.AddressModelV2> GetAddressesByProfileID(int ProfileID)
        {
            return (from w in context.Addresses.Include("City.Country").Include("AddressType") where w.ProfileID == ProfileID orderby w.AddressType.AddressTypeName ascending select new Business.DataTransferObjects.Mobile.AddressModelV2 { AddressAddress = w.AddressAddress, AddressImage = w.AddressImage, AddressLocation = w.AddressLocation, AddressName = w.AddressName, AddressTel = w.AddressTel, AddressTypeName = w.AddressType.AddressTypeName, AddressZipCode = w.AddressZipCode, CityName = w.City.CityName, CountryName = w.City.Country.CountryName }).AsQueryable();
        }

        public IQueryable<Address> GetAddressesAllAsQueryable(bool AddressStatus)
        {
            return (from w in context.Addresses where w.AddressStatus == AddressStatus orderby w.AddressID descending select w).AsQueryable();
        }

        public IQueryable<Address> GetAddressesAllAsQueryable(int ProfileID, int AddressTypeID)
        {
            return (from w in context.Addresses.Include("City.Country") where w.AddressStatus == true && w.ProfileID == ProfileID && w.AddressTypeID == AddressTypeID orderby w.AddressID descending select w).AsQueryable();
        }

        public IQueryable<Address> GetAddressesAllAsQueryable(string ProfileURL, int AddressTypeID)
        {
            return (from w in context.Addresses.Include("City.Country") where w.AddressStatus == true && w.Profile.ProfileURL == ProfileURL && w.AddressTypeID == AddressTypeID orderby w.AddressID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<Address> GetAddressesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Address> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Addresss orderby w.AddressID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Addresses.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Addresses orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Addresses.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<Address> GetAddressesAllAsQueryable(bool AddressStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Address> xList;
            if (filter != null)
            {
                xList = (from w in context.Addresses where w.AddressStatus == AddressStatus orderby w.AddressID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Addresses where w.AddressStatus == AddressStatus orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<Address> GetAddressesByCityID(int CityID, bool AddressStatus)
        {
            return (from w in context.Addresses where w.CityID == CityID && w.AddressStatus == AddressStatus orderby w.AddressID descending select w).ToList();
        }
        public List<Address> GetAddressesByCityID(int CityID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Addresses where w.CityID == CityID orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Address> GetAddressesByCityID(int CityID, bool AddressStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Addresses where w.CityID == CityID && w.AddressStatus == AddressStatus orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Address> GetAddressesByProfileID(int ProfileID, bool AddressStatus)
        {
            return (from w in context.Addresses where w.ProfileID == ProfileID && w.AddressStatus == AddressStatus orderby w.AddressID descending select w).ToList();
        }
        public List<Address> GetAddressesByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Addresses where w.ProfileID == ProfileID orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Address> GetAddressesByProfileID(int ProfileID, bool AddressStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Addresses where w.ProfileID == ProfileID && w.AddressStatus == AddressStatus orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Address> GetAddressesByAddressTypeID(int AddressTypeID, bool AddressStatus)
        {
            return (from w in context.Addresses where w.AddressTypeID == AddressTypeID && w.AddressStatus == AddressStatus orderby w.AddressID descending select w).ToList();
        }
        public List<Address> GetAddressesByAddressTypeID(int AddressTypeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Addresses where w.AddressTypeID == AddressTypeID orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Address> GetAddressesByAddressTypeID(int AddressTypeID, bool AddressStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Addresses where w.AddressTypeID == AddressTypeID && w.AddressStatus == AddressStatus orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<Address> GetAddressesByCityIDAsQueryable(int CityID)
        {
            return (from w in context.Addresses where w.CityID == CityID orderby w.AddressID descending select w).AsQueryable();
        }
        public IQueryable<Address> GetAddressesByCityIDAsQueryable(int CityID, bool AddressStatus)
        {
            return (from w in context.Addresses where w.CityID == CityID && w.AddressStatus == AddressStatus orderby w.AddressID descending select w).AsQueryable();
        }
        public IQueryable<Address> GetAddressesByCityIDAsQueryable(int CityID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Addresses where w.CityID == CityID orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Address> GetAddressesByCityIDAsQueryable(int CityID, bool AddressStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Addresses where w.CityID == CityID && w.AddressStatus == AddressStatus orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Address> GetAddressesByProfileURLAsQueryable(string ProfileURL)
        {
            //return (from w in context.Addresses  where w.Profile.ProfileURL == ProfileURL && w.AddressStatus == true orderby w.AddressID descending select w).AsQueryable();
            return (from w in context.Addresses.Include("City.Country") where w.AddressStatus == true && w.Profile.ProfileURL == ProfileURL orderby w.AddressID descending select w).AsQueryable();
        }
        public IQueryable<Address> GetAddressesByProfileIDAsQueryable(int ProfileID)
        {
            return (from w in context.Addresses where w.ProfileID == ProfileID orderby w.AddressID descending select w).AsQueryable();
        }
        public IQueryable<Address> GetAddressesByProfileIDAsQueryable(int ProfileID, bool AddressStatus)
        {
            return (from w in context.Addresses.Include("City.Country") where w.ProfileID == ProfileID && w.AddressStatus == AddressStatus orderby w.AddressID descending select w).AsQueryable();
        }
        public IQueryable<Address> GetAddressesByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Addresses where w.ProfileID == ProfileID orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Address> GetAddressesByProfileIDAsQueryable(int ProfileID, bool AddressStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Addresses where w.ProfileID == ProfileID && w.AddressStatus == AddressStatus orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Address> GetAddressesByAddressTypeIDAsQueryable(int AddressTypeID)
        {
            return (from w in context.Addresses where w.AddressTypeID == AddressTypeID orderby w.AddressID descending select w).AsQueryable();
        }
        public IQueryable<Address> GetAddressesByAddressTypeIDAsQueryable(int AddressTypeID, bool AddressStatus)
        {
            return (from w in context.Addresses where w.AddressTypeID == AddressTypeID && w.AddressStatus == AddressStatus orderby w.AddressID descending select w).AsQueryable();
        }
        public IQueryable<Address> GetAddressesByAddressTypeIDAsQueryable(int AddressTypeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Addresses where w.AddressTypeID == AddressTypeID orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Address> GetAddressesByAddressTypeIDAsQueryable(int AddressTypeID, bool AddressStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Addresses where w.AddressTypeID == AddressTypeID && w.AddressStatus == AddressStatus orderby w.AddressID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}