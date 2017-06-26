using Gorgias.Business.DataTransferObjects.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Mobile
{
    public class ProfileSliderModel
    {
        public ProfileModel Profile { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
    }
}