using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class FeatureModel
    {
        public string FeatureTitle { get; set; }
        public int FeatureID { get; set; }
        public string FeatureImage { get; set; }
        public string FeatureDescription { get; set; }
        public int FeaturedSponsorMode { get; set; }
        public int FeaturedRole { get; set; }
        public int ProfileID { get; set; }
        public string ProfileFullname { get; set; }
    }
}