using Gorgias.Business.DataTransferObjects.Authentication;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;
using Gorgias.Infrastruture.Email;
using System.Configuration;
using Gorgias.Infrastruture.Core.Email;
using System.Net;
using Gorgias.Infrastruture.Core.Encoder;

namespace Gorgias.DataLayer.Authentication
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            var provider = new DpapiDataProtectionProvider("Sample");

            var dataProtectionProvider = Startup.DataProtectionProvider;

            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
            _userManager.UserTokenProvider = new DataProtectorTokenProvider<IdentityUser>(dataProtectionProvider.Create("EmailConfirmation"));
            _userManager.EmailService = new EmailService();
        }

        public async Task<IdentityUser> GetUser(string Username)
        {
            return await _userManager.FindByIdAsync(Username);
        }

        public async Task<bool> ConfirmEmail(string UserID, string Code)
        {
            IdentityUser user = await _userManager.FindByIdAsync(UserID);
            IdentityResult result;
            try
            {
                result = await _userManager.ConfirmEmailAsync(user.Id, EncodingHelper.Base64ForUrlDecode(Code));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ChangePassword(ChangePasswordModel model)
        {
            IdentityUser user = await _userManager.FindByNameAsync(model.Username);
            IdentityResult result = await _userManager.ChangePasswordAsync(user.Id, model.OldPassword, model.Password);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ResetPassword(ResetPasswordModel model)
        {
            try
            {
                IdentityUser user = await _userManager.FindByIdAsync(model.Username);
                IdentityResult result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Forget(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                // Send an email with this link
                string code;
                try
                {
                    code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                Dictionary<string, string> values = new Dictionary<string, string>();
                code = HttpUtility.UrlEncode(code);//WebUtility.UrlEncode(code);
                var callbackUrl = string.Format(ConfigurationManager.AppSettings["WebFrontURL"] + "account/reset/{0}?code={1}", user.Id, code);
                string messageBody = "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>";
                values.Add("##Body##", messageBody);
                await _userManager.SendEmailAsync(user.Id, "Reset Password", EmailHelper.PrepareBody(values, "mailtemplate.html"));
                return true;
            }
            return false;
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName,
                Email = userModel.UserName,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                string code;
                try
                {
                    code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    Dictionary<string, string> values = new Dictionary<string, string>();
                    code = EncodingHelper.Base64ForUrlEncode(code);//WebUtility.UrlEncode(code);
                    var callbackUrl = string.Format(ConfigurationManager.AppSettings["WebFrontURL"] + "account/confirmation/{0}?code={1}", user.Id, code);
                    string messageBody = "Please activate your account by clicking <a href=\"" + callbackUrl + "\">here</a>";
                    values.Add("##Body##", messageBody);
                    await _userManager.SendEmailAsync(user.Id, "Account Confirmation", EmailHelper.PrepareBody(values, "mailtemplate.html"));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return result;
        }

        public async Task<IdentityResult> RegisterUserAdmin(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName,
                Email = userModel.UserName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                string code;
                try
                {
                    //code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //Dictionary<string, string> values = new Dictionary<string, string>();
                    //code = EncodingHelper.Base64ForUrlEncode(code);//WebUtility.UrlEncode(code);
                    //var callbackUrl = string.Format(ConfigurationManager.AppSettings["WebFrontURL"] + "account/confirmation/{0}?code={1}", user.Id, code);
                    //string messageBody = "Please activate your account by clicking <a href=\"" + callbackUrl + "\">here</a>";
                    //values.Add("##Body##", messageBody);
                    //await _userManager.SendEmailAsync(user.Id, "Account Confirmation", EmailHelper.PrepareBody(values, "mailtemplate.html"));

                    code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                    Dictionary<string, string> values = new Dictionary<string, string>();
                    code = HttpUtility.UrlEncode(code);//WebUtility.UrlEncode(code);
                    var callbackUrl = string.Format(ConfigurationManager.AppSettings["WebFrontURL"] + "account/reset/{0}?code={1}", user.Id, code);
                    string messageBody = "Your Profile is created. You are now invited to Gorgias Platform.<br/>Please active your account by clicking <a href=\"" + callbackUrl + "\">here</a>";
                    values.Add("##Body##", messageBody);
                    await _userManager.SendEmailAsync(user.Id, "Reset Password", EmailHelper.PrepareBody(values, "mailtemplate.html"));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return result;
        }

        public async Task<IdentityUser> DeleteUser(string userName)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(userName);
            await _userManager.DeleteAsync(user);
            return user;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _ctx.RefreshTokens.ToList();
        }

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}