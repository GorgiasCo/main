using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class ExternalLinkModel
    {
        public int ProfileID { get; set; }
        public string LinkTypeImage { get; set; }
        public string ExternalLinkURL { get; set; }
    }
}