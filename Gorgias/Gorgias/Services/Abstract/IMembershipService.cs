using Gorgias.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Services.Abstract
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        User CreateUser(string username, string email, string password, int[] roles);
        User GetUser(int userId);
        List<UserRole> GetUserRoles(string username);
    }
}