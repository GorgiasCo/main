using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface ISocialNetworkRepository
    {
    
    
        SocialNetwork Insert(String SocialNetworkName, Boolean SocialNetworkStatus, String SocialNetworkURL, String SocialNetworkImage, String SocialNetworkDescription);
        bool Update(int SocialNetworkID, String SocialNetworkName, Boolean SocialNetworkStatus, String SocialNetworkURL, String SocialNetworkImage, String SocialNetworkDescription);        
        bool Delete(int SocialNetworkID);

        SocialNetwork GetSocialNetwork(int SocialNetworkID);

        //List
        List<SocialNetwork> GetSocialNetworksAll();
        List<SocialNetwork> GetSocialNetworksAll(bool SocialNetworkStatus);
        List<SocialNetwork> GetSocialNetworksAll(int page = 1, int pageSize = 7, string filter=null);
        List<SocialNetwork> GetSocialNetworksAll(bool SocialNetworkStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<SocialNetwork> GetSocialNetworksAllAsQueryable();
        IQueryable<SocialNetwork> GetSocialNetworksAllAsQueryable(bool SocialNetworkStatus);
        IQueryable<SocialNetwork> GetSocialNetworksAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<SocialNetwork> GetSocialNetworksAllAsQueryable(bool SocialNetworkStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}


