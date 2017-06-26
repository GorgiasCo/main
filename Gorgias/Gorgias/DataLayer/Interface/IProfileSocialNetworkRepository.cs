using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IProfileSocialNetworkRepository
    {
    
    
        ProfileSocialNetwork Insert(int SocialNetworkID, int ProfileID, String ProfileSocialNetworkURL);
        bool Update(int SocialNetworkID, int ProfileID, String ProfileSocialNetworkURL);        
        bool Delete(int SocialNetworkID, int ProfileID);

        ProfileSocialNetwork GetProfileSocialNetwork(int SocialNetworkID, int ProfileID);

        //List
        List<ProfileSocialNetwork> GetProfileSocialNetworksAll();
        List<ProfileSocialNetwork> GetProfileSocialNetworksAll(int page = 1, int pageSize = 7, string filter=null);
        
        List<ProfileSocialNetwork> GetProfileSocialNetworksBySocialNetworkID(int SocialNetworkID, int page = 1, int pageSize = 7, string filter=null);       
        List<ProfileSocialNetwork> GetProfileSocialNetworksByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        
        //IQueryable
        IQueryable<ProfileSocialNetwork> GetProfileSocialNetworksAllAsQueryable();
        IQueryable<ProfileSocialNetwork> GetProfileSocialNetworksAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<ProfileSocialNetwork> GetProfileSocialNetworksBySocialNetworkIDAsQueryable(int SocialNetworkID);
        IQueryable<ProfileSocialNetwork> GetProfileSocialNetworksBySocialNetworkIDAsQueryable(int SocialNetworkID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<ProfileSocialNetwork> GetProfileSocialNetworksByProfileIDAsQueryable(int ProfileID);
        IQueryable<ProfileSocialNetwork> GetProfileSocialNetworksByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
    }
}


