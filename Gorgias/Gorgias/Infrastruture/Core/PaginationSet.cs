using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Infrastruture.Core
{
    public class PaginationSet<T>
    {
        public int Page { get; set; }

        public int Count
        {
            get
            {
                return (null != this.Items) ? this.Items.Count() : 0;
            }
        }

        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool hasMore {
            get {
                return TotalPages == Page ? false : true;
            }
        }

        public IEnumerable<T> Items { get; set; }
    }
}