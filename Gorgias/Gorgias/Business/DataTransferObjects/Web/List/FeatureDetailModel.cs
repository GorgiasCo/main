using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web.List
{
    public class FeatureDetailModel
    {
        public ProfileModel Profile { get; set; }
        public IEnumerable<ExternalLinkModel> ExternalLinks { get; set; }
        public IEnumerable<SocialNetworkModel> SocialNetworks { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
        public IEnumerable<FeatureModel> CurrentFeature { get; set; }
    }
}