using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IExternalLinkRepository
    {
    
    
        ExternalLink Insert(int LinkTypeID, int ProfileID, String ExternalLinkURL);
        bool Update(int LinkTypeID, int ProfileID, String ExternalLinkURL);        
        bool Delete(int LinkTypeID, int ProfileID);

        ExternalLink GetExternalLink(int LinkTypeID, int ProfileID);

        //List
        List<ExternalLink> GetExternalLinksAll();
        List<ExternalLink> GetExternalLinksAll(int page = 1, int pageSize = 7, string filter=null);
        
        List<ExternalLink> GetExternalLinksByLinkTypeID(int LinkTypeID, int page = 1, int pageSize = 7, string filter=null);       
        List<ExternalLink> GetExternalLinksByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        
        //IQueryable
        IQueryable<ExternalLink> GetExternalLinksAllAsQueryable();
        IQueryable<ExternalLink> GetExternalLinksAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<ExternalLink> GetExternalLinksByLinkTypeIDAsQueryable(int LinkTypeID);
        IQueryable<ExternalLink> GetExternalLinksByLinkTypeIDAsQueryable(int LinkTypeID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<ExternalLink> GetExternalLinksByProfileIDAsQueryable(int ProfileID);
        IQueryable<ExternalLink> GetExternalLinksByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
    }
}


