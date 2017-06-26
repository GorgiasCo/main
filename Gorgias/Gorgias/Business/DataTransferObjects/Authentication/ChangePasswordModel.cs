using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Authentication
{
    public class ChangePasswordModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string ConfirmedPassword { get; set; }
    }
}