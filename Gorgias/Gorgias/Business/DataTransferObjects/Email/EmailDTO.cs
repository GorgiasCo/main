using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Email
{
    public class EmailDTO
    {
        public string Subject { get; set; }
        public string TO { get; set; }
        public string Body { get; set; }
    }
}