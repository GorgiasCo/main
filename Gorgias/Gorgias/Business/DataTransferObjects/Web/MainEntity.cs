using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class MainEntities
    {
        public IEnumerable<CountryModel> Country { get; set; }
        public IEnumerable<IndustryModel> Industry { get; set; }
        public IEnumerable<ProfileTypeModel> ProfileType { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
    }
}