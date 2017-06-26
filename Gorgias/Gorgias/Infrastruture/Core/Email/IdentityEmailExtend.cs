using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Infrastruture.Core.Email
{
    public class IdentityEmailExtend : IdentityMessage
    {
        public Dictionary<string, string> bodyValues { get; set; }
        public string emailTemplateFile { get; set; }
    }
}