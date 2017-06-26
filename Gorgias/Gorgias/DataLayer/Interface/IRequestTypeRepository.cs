using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IRequestTypeRepository
    {
    
    
        RequestType Insert(String RequestTypeName, Boolean RequestTypeStatus, Boolean RequestIsRestricted);
        bool Update(int RequestTypeID, String RequestTypeName, Boolean RequestTypeStatus, Boolean RequestIsRestricted);        
        bool Delete(int RequestTypeID);

        RequestType GetRequestType(int RequestTypeID);

        //List
        List<RequestType> GetRequestTypesAll();
        List<RequestType> GetRequestTypesAll(bool RequestTypeStatus);
        List<RequestType> GetRequestTypesAll(int page = 1, int pageSize = 7, string filter=null);
        List<RequestType> GetRequestTypesAll(bool RequestTypeStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<RequestType> GetRequestTypesAllAsQueryable();
        IQueryable<RequestType> GetRequestTypesAllAsQueryable(bool RequestTypeStatus);
        IQueryable<RequestType> GetRequestTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<RequestType> GetRequestTypesAllAsQueryable(bool RequestTypeStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}


