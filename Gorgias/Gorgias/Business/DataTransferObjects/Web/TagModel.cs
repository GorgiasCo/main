using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class TagModel
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public bool selected { get; set; }
    }
}