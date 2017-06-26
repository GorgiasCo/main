using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface ISubscriptionTypeRepository
    {
    
    
        SubscriptionType Insert(String SubscriptionTypeName, Decimal SubscriptionTypeFee, Boolean SubscriptionTypeStatus, String SubscriptionTypeImage, String SubscriptionTypeDescription);
        bool Update(int SubscriptionTypeID, String SubscriptionTypeName, Decimal SubscriptionTypeFee, Boolean SubscriptionTypeStatus, String SubscriptionTypeImage, String SubscriptionTypeDescription);        
        bool Delete(int SubscriptionTypeID);

        SubscriptionType GetSubscriptionType(int SubscriptionTypeID);

        //List
        List<SubscriptionType> GetSubscriptionTypesAll();
        List<SubscriptionType> GetSubscriptionTypesAll(bool SubscriptionTypeStatus);
        List<SubscriptionType> GetSubscriptionTypesAll(int page = 1, int pageSize = 7, string filter=null);
        List<SubscriptionType> GetSubscriptionTypesAll(bool SubscriptionTypeStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<SubscriptionType> GetSubscriptionTypesAllAsQueryable();
        IQueryable<SubscriptionType> GetSubscriptionTypesAllAsQueryable(bool SubscriptionTypeStatus);
        IQueryable<SubscriptionType> GetSubscriptionTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<SubscriptionType> GetSubscriptionTypesAllAsQueryable(bool SubscriptionTypeStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}


