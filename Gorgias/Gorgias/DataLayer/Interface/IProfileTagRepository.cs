using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IProfileTagRepository
    {
    
    
        ProfileTag Insert(int TagID, int ProfileID, Boolean ProfileTagStatus);
        bool Update(int TagID, int ProfileID, Boolean ProfileTagStatus);        
        bool Delete(int TagID, int ProfileID);

        ProfileTag GetProfileTag(int TagID, int ProfileID);

        //List
        List<ProfileTag> GetProfileTagsAll();
        List<ProfileTag> GetProfileTagsAll(bool ProfileTagStatus);
        List<ProfileTag> GetProfileTagsAll(int page = 1, int pageSize = 7, string filter=null);
        List<ProfileTag> GetProfileTagsAll(bool ProfileTagStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        List<ProfileTag> GetProfileTagsByTagID(int TagID, bool ProfileTagStatus);
        List<ProfileTag> GetProfileTagsByTagID(int TagID, int page = 1, int pageSize = 7, string filter=null);       
        List<ProfileTag> GetProfileTagsByTagID(int TagID, bool ProfileTagStatus, int page = 1, int pageSize = 7, string filter=null);               
        List<ProfileTag> GetProfileTagsByProfileID(int ProfileID, bool ProfileTagStatus);
        List<ProfileTag> GetProfileTagsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        List<ProfileTag> GetProfileTagsByProfileID(int ProfileID, bool ProfileTagStatus, int page = 1, int pageSize = 7, string filter=null);               
        
        //IQueryable
        IQueryable<ProfileTag> GetProfileTagsAllAsQueryable();
        IQueryable<ProfileTag> GetProfileTagsAllAsQueryable(bool ProfileTagStatus);
        IQueryable<ProfileTag> GetProfileTagsAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<ProfileTag> GetProfileTagsAllAsQueryable(bool ProfileTagStatus, int page = 1, int pageSize = 7, string filter=null);   
        IQueryable<ProfileTag> GetProfileTagsByTagIDAsQueryable(int TagID);
        IQueryable<ProfileTag> GetProfileTagsByTagIDAsQueryable(int TagID, bool ProfileTagStatus);
        IQueryable<ProfileTag> GetProfileTagsByTagIDAsQueryable(int TagID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<ProfileTag> GetProfileTagsByTagIDAsQueryable(int TagID, bool ProfileTagStatus, int page = 1, int pageSize = 7, string filter=null);               
        IQueryable<ProfileTag> GetProfileTagsByProfileIDAsQueryable(int ProfileID);
        IQueryable<ProfileTag> GetProfileTagsByProfileIDAsQueryable(int ProfileID, bool ProfileTagStatus);
        IQueryable<ProfileTag> GetProfileTagsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<ProfileTag> GetProfileTagsByProfileIDAsQueryable(int ProfileID, bool ProfileTagStatus, int page = 1, int pageSize = 7, string filter=null);               
    }
}


