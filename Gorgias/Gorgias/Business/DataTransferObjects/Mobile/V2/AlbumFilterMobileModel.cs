using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class AlbumFilterMobileModel
    {
        public int CategoryID { get; set; }
        public int CategoryTypeID { get; set; }
        public int ProfileID { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}