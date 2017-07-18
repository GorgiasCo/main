using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Report
{
    public class ProfileReport
    {
        public int ProfileID { get; set; }
        public int? ProfileView { get; set; }
        public int? AlbumView { get; set; }
        public int? AlbumLikes { get; set; }
        public int? AlbumComments { get; set; }
        public int? StayOnConnection { get; set; }
        public int? Subscription { get; set; }
    }
}