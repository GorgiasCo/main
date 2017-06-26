using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class AlbumImageSetModel
    {
        public int AlbumID { get; set; }
        public string AlbumName { get; set; }
        public string ContentURL { get; set; }
        public bool ContentStatus { get; set; }
        public long RowN { get; set; }
    }
}