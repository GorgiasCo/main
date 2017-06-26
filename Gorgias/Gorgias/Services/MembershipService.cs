using Gorgias.DataLayer.Repository.SQL.Authentication;
using Gorgias.Services.Abstract;
using Gorgias.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Gorgias.Services
{
    public class MembershipService : IMembershipService
    {
        private AuthenticationRepository _repository;
        private EncryptionService _encryptionService;

        #region IMembershipService Implementation

        public MembershipService() {
            _repository = new AuthenticationRepository();
            _encryptionService = new EncryptionService();
        }

        public MembershipContext ValidateUser(string username, string password)
        {
            var membershipCtx = new MembershipContext();

            var user = _repository.GetUser(username);//
            if (user != null && isUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.UserEmail);
                membershipCtx.User = user;

                var identity = new GenericIdentity(user.UserEmail);
                membershipCtx.Principal = new GenericPrincipal(
                    identity,
                    userRoles.Select(x => x.UserRoleName).ToArray());
            }

            return membershipCtx;
        }
        public User CreateUser(string username, string email, string password, int[] roles)
        {
            var existingUser = _repository.GetUser(username);

            if (existingUser != null)
            {
                throw new Exception("Username is already in use");
            }

            var passwordSalt = _encryptionService.CreateSalt();

            var user = new User()
            {
                UserEmail = username,
                //UserSalt = passwordSalt,                
                UserStatus = false,
                //UserHashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
                UserDateCreated = DateTime.Now
            };

            _repository.InsertUser(user);

            if (roles != null || roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    addUserToRole(user, role);
                }
            }

            return user;
        }

        public User GetUser(int userId)
        {
            return _repository.GetUser(userId);
        }

        public List<UserRole> GetUserRoles(string username)
        {
            List<UserRole> _result = new List<UserRole>();

            var existingUserRoles = _repository.GetUserRoles(username);

            if (existingUserRoles != null)
            {
                foreach (var userRole in existingUserRoles)
                {
                    _result.Add(userRole);
                }
            }

            return _result.Distinct().ToList();
        }
        #endregion

        #region Helper methods
        private void addUserToRole(User user, int roleId)
        {
            var role = _repository.GetRole(roleId);
            if (role == null)
                throw new ApplicationException("Role doesn't exist.");

            //var userRole = new UserRole()
            //{
            //    RoleId = role.ID,
            //    UserId = user.ID
            //};
            //_userRoleRepository.Add(userRole);
        }

        private bool isPasswordValid(User user, string password)
        {
            return true;//string.Equals(_encryptionService.EncryptPassword(password, user.UserSalt), user.UserHashedPassword);
        }

        private bool isUserValid(User user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.UserStatus;
            }

            return false;
        }
        #endregion
    }
}