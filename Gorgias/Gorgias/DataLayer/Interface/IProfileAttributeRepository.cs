using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IProfileAttributeRepository
    {
    
    
        ProfileAttribute Insert(int AttributeID, int ProfileID, String ProfileAttributeNote);
        bool Update(int AttributeID, int ProfileID, String ProfileAttributeNote);        
        bool Delete(int AttributeID, int ProfileID);

        ProfileAttribute GetProfileAttribute(int AttributeID, int ProfileID);

        //List
        List<ProfileAttribute> GetProfileAttributesAll();
        List<ProfileAttribute> GetProfileAttributesAll(int page = 1, int pageSize = 7, string filter=null);
        
        List<ProfileAttribute> GetProfileAttributesByAttributeID(int AttributeID, int page = 1, int pageSize = 7, string filter=null);       
        List<ProfileAttribute> GetProfileAttributesByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        
        //IQueryable
        IQueryable<ProfileAttribute> GetProfileAttributesAllAsQueryable();
        IQueryable<ProfileAttribute> GetProfileAttributesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<ProfileAttribute> GetProfileAttributesByAttributeIDAsQueryable(int AttributeID);
        IQueryable<ProfileAttribute> GetProfileAttributesByAttributeIDAsQueryable(int AttributeID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<ProfileAttribute> GetProfileAttributesByProfileIDAsQueryable(int ProfileID);
        IQueryable<ProfileAttribute> GetProfileAttributesByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
    }
}


