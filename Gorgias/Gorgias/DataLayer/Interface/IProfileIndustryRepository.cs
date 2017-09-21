using Gorgias.Business.DataTransferObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IProfileIndustryRepository
    {
        bool Insert(int IndustryID, int ProfileID);
        bool Delete(int IndustryID, int ProfileID);
        bool Delete(int ProfileID);

        //List            
        List<Profile> GetProfileIndustriesByIndustryID(int IndustryID, int page = 1, int pageSize = 7, string filter = null);
        Profile GetProfileIndustriesByProfileID(int ProfileID);

        //IQueryable        
        IQueryable<Profile> GetProfileIndustriesByIndustryIDAsQueryable(int IndustryID);
        IQueryable<Profile> GetProfileIndustriesByIndustryIDAsQueryable(int IndustryID, int page = 1, int pageSize = 7, string filter = null);
    }
}


