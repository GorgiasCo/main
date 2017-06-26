using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface ILinkTypeRepository
    {
    
    
        LinkType Insert(String LinkTypeName, Boolean LinkTypeStatus, String LinkTypeImage, String LinkTypeDescription);
        bool Update(int LinkTypeID, String LinkTypeName, Boolean LinkTypeStatus, String LinkTypeImage, String LinkTypeDescription);        
        bool Delete(int LinkTypeID);

        LinkType GetLinkType(int LinkTypeID);

        //List
        List<LinkType> GetLinkTypesAll();
        List<LinkType> GetLinkTypesAll(bool LinkTypeStatus);
        List<LinkType> GetLinkTypesAll(int page = 1, int pageSize = 7, string filter=null);
        List<LinkType> GetLinkTypesAll(bool LinkTypeStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<LinkType> GetLinkTypesAllAsQueryable();
        IQueryable<LinkType> GetLinkTypesAllAsQueryable(bool LinkTypeStatus);
        IQueryable<LinkType> GetLinkTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<LinkType> GetLinkTypesAllAsQueryable(bool LinkTypeStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}


