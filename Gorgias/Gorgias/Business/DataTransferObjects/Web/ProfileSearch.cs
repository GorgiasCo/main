using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class ProfileSearch
    {
        public int? CountryID { get; set; }
        public string[] Industries { get; set; }
        public int? ProfileTypeID { get; set; }
        public int? ProfileID { get; set; }
        public int OrderType { get; set; } 
        public string[] Tags { get; set; }
        public string Location { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? SubscriptionTypeID { get; set; }
    }
}