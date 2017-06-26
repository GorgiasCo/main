using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web.List
{
    public class ProfileListModel
    {
        public ProfileModel Profile { get; set; }
        public IEnumerable<AlbumModel> Albums { get; set; }
        public IEnumerable<AboutModel> About { get; set; }
        public IEnumerable<ConnectionModel> Connections { get; set; }
        public IEnumerable<ExternalLinkModel> ExternalLinks { get; set; }
        public IEnumerable<SocialNetworkModel> SocialNetworks { get; set; }
        public IEnumerable<TagModel> Tags { get; set; } 
        public IEnumerable<TagModel> PrimaryTags { get; set; }       
    }
}