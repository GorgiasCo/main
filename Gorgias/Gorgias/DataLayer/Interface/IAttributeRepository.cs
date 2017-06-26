using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IAttributeRepository
    {
    
    
        Attribute Insert(String AttributeName, String AttributeCaption, Boolean AttributeStatus, int AttributeMode, String AttributeRule, String AttributeType, String AttributeImage, String AttributeDescription);
        bool Update(int AttributeID, String AttributeName, String AttributeCaption, Boolean AttributeStatus, int AttributeMode, String AttributeRule, String AttributeType, String AttributeImage, String AttributeDescription);        
        bool Delete(int AttributeID);

        Attribute GetAttribute(int AttributeID);

        //List
        List<Attribute> GetAttributesAll();
        List<Attribute> GetAttributesAll(bool AttributeStatus);
        List<Attribute> GetAttributesAll(int page = 1, int pageSize = 7, string filter=null);
        List<Attribute> GetAttributesAll(bool AttributeStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<Attribute> GetAttributesAllAsQueryable();
        IQueryable<Attribute> GetAttributesAllAsQueryable(bool AttributeStatus);
        IQueryable<Attribute> GetAttributesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<Attribute> GetAttributesAllAsQueryable(bool AttributeStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}


