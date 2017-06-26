using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IIndustryRepository
    {
    
    
        Industry Insert(String IndustryName, Boolean IndustryStatus, int IndustryParentID, String IndustryImage, String IndustryDescription);
        Industry Insert(Industry objIndustry);
        bool Update(int IndustryID, String IndustryName, Boolean IndustryStatus, int IndustryParentID, String IndustryImage, String IndustryDescription);        
        bool Delete(int IndustryID);

        Industry GetIndustry(int IndustryID);

        //List
        List<Industry> GetIndustriesAll();
        List<Industry> GetIndustriesAll(bool IndustryStatus);
        List<Industry> GetIndustriesAll(int page = 1, int pageSize = 7, string filter=null);
        List<Industry> GetIndustriesAll(bool IndustryStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<Industry> GetIndustriesAllAsQueryable();
        IQueryable<Industry> GetIndustriesAllAsQueryable(bool IndustryStatus);
        IQueryable<Industry> GetIndustriesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<Industry> GetIndustriesAllAsQueryable(bool IndustryStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}


