using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IFeaturedSponsorRepository
    {
    
    
        FeaturedSponsor Insert(int FeatureID, int ProfileID, int FeaturedSponsorMode, int FeaturedRole);
        bool Update(int FeatureID, int ProfileID, int FeaturedSponsorMode, int FeaturedRole);        
        bool Delete(int FeatureID, int ProfileID);

        FeaturedSponsor GetFeaturedSponsor(int FeatureID, int ProfileID);

        //List
        List<FeaturedSponsor> GetFeaturedSponsorsAll();
        List<FeaturedSponsor> GetFeaturedSponsorsAll(int page = 1, int pageSize = 7, string filter=null);
        
        List<FeaturedSponsor> GetFeaturedSponsorsByFeatureID(int FeatureID, int page = 1, int pageSize = 7, string filter=null);       
        List<FeaturedSponsor> GetFeaturedSponsorsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        
        //IQueryable
        IQueryable<FeaturedSponsor> GetFeaturedSponsorsAllAsQueryable();
        IQueryable<FeaturedSponsor> GetFeaturedSponsorsAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<FeaturedSponsor> GetFeaturedSponsorsByFeatureIDAsQueryable(int FeatureID);
        IQueryable<FeaturedSponsor> GetFeaturedSponsorsByFeatureIDAsQueryable(int FeatureID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<FeaturedSponsor> GetFeaturedSponsorsByProfileIDAsQueryable(int ProfileID);
        IQueryable<FeaturedSponsor> GetFeaturedSponsorsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
    }
}


