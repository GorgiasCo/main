using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class IndustryModel
    {
        public int IndustryID { get; set; }
        public string IndustryName { get; set; }
        public bool selected { get; set; }
    }
}