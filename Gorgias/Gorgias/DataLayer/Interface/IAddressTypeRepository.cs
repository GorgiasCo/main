using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IAddressTypeRepository
    {
    
    
        AddressType Insert(String AddressTypeName, String AddressTypeImage, Boolean AddressTypeStatus);
        bool Update(int AddressTypeID, String AddressTypeName, String AddressTypeImage, Boolean AddressTypeStatus);        
        bool Delete(int AddressTypeID);

        AddressType GetAddressType(int AddressTypeID);

        //List
        List<AddressType> GetAddressTypesAll();
        List<AddressType> GetAddressTypesAll(bool AddressTypeStatus);
        List<AddressType> GetAddressTypesAll(int page = 1, int pageSize = 7, string filter=null);
        List<AddressType> GetAddressTypesAll(bool AddressTypeStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<AddressType> GetAddressTypesAllAsQueryable();
        IQueryable<AddressType> GetAddressTypesAllAsQueryable(bool AddressTypeStatus);
        IQueryable<AddressType> GetAddressTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<AddressType> GetAddressTypesAllAsQueryable(bool AddressTypeStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}


