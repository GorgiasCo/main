using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IAddressRepository
    {

        Address Insert(String AddressName, Boolean AddressStatus, String AddressTel, String AddressFax, String AddressZipCode, String AddressAddress, String AddressEmail, String AddressImage, int CityID, int ProfileID, int AddressTypeID, DbGeography AddressLocation);
        bool Update(int AddressID, String AddressName, Boolean AddressStatus, String AddressTel, String AddressFax, String AddressZipCode, String AddressAddress, String AddressEmail, String AddressImage, int CityID, int ProfileID, int AddressTypeID, DbGeography AddressLocation);
        bool Delete(int AddressID);
        bool DeleteByProfileID(int ProfileID);

        Address GetAddress(int AddressID);

        //List
        List<Address> GetAddressesAll();
        List<Address> GetAddressesAll(bool AddressStatus);
        List<Address> GetAddressesAll(int page = 1, int pageSize = 7, string filter = null);
        List<Address> GetAddressesAll(bool AddressStatus, int page = 1, int pageSize = 7, string filter = null);

        List<Address> GetAddressesByCityID(int CityID, bool AddressStatus);
        List<Address> GetAddressesByCityID(int CityID, int page = 1, int pageSize = 7, string filter = null);
        List<Address> GetAddressesByCityID(int CityID, bool AddressStatus, int page = 1, int pageSize = 7, string filter = null);
        List<Address> GetAddressesByProfileID(int ProfileID, bool AddressStatus);
        List<Address> GetAddressesByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null);
        List<Address> GetAddressesByProfileID(int ProfileID, bool AddressStatus, int page = 1, int pageSize = 7, string filter = null);
        List<Address> GetAddressesByAddressTypeID(int AddressTypeID, bool AddressStatus);
        List<Address> GetAddressesByAddressTypeID(int AddressTypeID, int page = 1, int pageSize = 7, string filter = null);
        List<Address> GetAddressesByAddressTypeID(int AddressTypeID, bool AddressStatus, int page = 1, int pageSize = 7, string filter = null);

        //IQueryable
        IQueryable<Business.DataTransferObjects.Mobile.AddressModelV2> GetAddressesByProfileID(int ProfileID);
        IQueryable<Address> GetAddressesAllAsQueryable();
        IQueryable<Address> GetAddressesAllAsQueryable(bool AddressStatus);
        IQueryable<Address> GetAddressesAllAsQueryable(int ProfileID, int AddressTypeID);
        IQueryable<Address> GetAddressesAllAsQueryable(string ProfileURL, int AddressTypeID);
        IQueryable<Address> GetAddressesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Address> GetAddressesAllAsQueryable(bool AddressStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Address> GetAddressesByCityIDAsQueryable(int CityID);
        IQueryable<Address> GetAddressesByCityIDAsQueryable(int CityID, bool AddressStatus);
        IQueryable<Address> GetAddressesByCityIDAsQueryable(int CityID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Address> GetAddressesByCityIDAsQueryable(int CityID, bool AddressStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Address> GetAddressesByProfileIDAsQueryable(int ProfileID);
        IQueryable<Address> GetAddressesByProfileIDAsQueryable(int ProfileID, bool AddressStatus);
        IQueryable<Address> GetAddressesByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Address> GetAddressesByProfileIDAsQueryable(int ProfileID, bool AddressStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Address> GetAddressesByProfileURLAsQueryable(string ProfileURL);
        IQueryable<Address> GetAddressesByAddressTypeIDAsQueryable(int AddressTypeID);
        IQueryable<Address> GetAddressesByAddressTypeIDAsQueryable(int AddressTypeID, bool AddressStatus);
        IQueryable<Address> GetAddressesByAddressTypeIDAsQueryable(int AddressTypeID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Address> GetAddressesByAddressTypeIDAsQueryable(int AddressTypeID, bool AddressStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


