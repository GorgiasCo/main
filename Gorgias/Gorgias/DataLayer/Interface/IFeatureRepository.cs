using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IFeatureRepository
    {
    
    
        Feature Insert(String FeatureTitle, DateTime FeatureDateCreated, DateTime FeatureDateExpired, Boolean FeatureStatus, Boolean FeatureIsDeleted, String FeatureImage, String FeatureDescription, int ProfileID);
        bool Update(int FeatureID, String FeatureTitle, DateTime FeatureDateCreated, DateTime FeatureDateExpired, Boolean FeatureStatus, Boolean FeatureIsDeleted, String FeatureImage, String FeatureDescription, int ProfileID);        
        bool Delete(int FeatureID);

        Feature GetFeature(int FeatureID);

        //List
        List<Feature> GetFeaturesAll();
        List<Feature> GetFeaturesAll(bool FeatureStatus);
        List<Feature> GetFeaturesAll(int page = 1, int pageSize = 7, string filter=null);
        List<Feature> GetFeaturesAll(bool FeatureStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<Feature> GetFeaturesAllAsQueryable();
        IQueryable<Feature> GetFeaturesAllAsQueryable(bool FeatureStatus);
        IQueryable<Feature> GetFeaturesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<Feature> GetFeaturesAllAsQueryable(bool FeatureStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}


