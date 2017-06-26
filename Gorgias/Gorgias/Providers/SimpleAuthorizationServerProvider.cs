using Gorgias.Business.DataTransferObjects;
using Gorgias.Business.DataTransferObjects.Authentication;
using Gorgias.DataLayer.Authentication;
using Gorgias.Infrastruture.Core;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Gorgias.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            string clientId = string.Empty;
            string clientSecret = string.Empty;
            Client client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                //context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }

            using (AuthRepository _repo = new AuthRepository())
            {
                client = _repo.FindClient(context.ClientId);
            }

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            string userRole = "user";
            using (AuthRepository _repo = new AuthRepository())
            {
                IdentityUser user = await _repo.FindUser(context.UserName, context.Password);
                //IdentityUserRole role = user.Roles.FirstOrDefault();
                //if (role != null)
                //{
                //    userRole = role.RoleId;
                //}
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
                if (!user.EmailConfirmed)
                {
                    context.SetError("invalid_grant", "You need to active your account first.");
                    return;
                }
            }

            //IEnumerable<UserProfileDTO> resultUser = new BusinessLayer.Facades.UserProfileFacade().GetAdministrationUserProfile(context.UserName);
            //IEnumerable<ProfileDTO> resultProfiles = new BusinessLayer.Facades.ProfileFacade().GetProfilesAdministration(context.UserName);
            //ProfileDTO resultProfile = new BusinessLayer.Facades.ProfileFacade().GetProfile(context.UserName);

            UserCustomDTO resultuser = new BusinessLayer.Facades.UserFacade().GetUser(context.UserName);

            //check country if not null => then it is Country Managers

            //if null profiles count zero => then it is Master Administration

            //if has userprofiles that have any user role 1 => then it is owner (Agency can be identified in CMS ;) )

            //if has userprofiles that dont have user role 1, check if has 2 or more => Normal Agency or invite or Content Managers

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            if (resultuser.CountryID == null && resultuser.UserProfiles.Count() == 0)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    }
                });
                identity.AddClaim(new Claim("sub", context.UserName));

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }

            if (resultuser.CountryID != null && resultuser.UserProfiles.Count() == 0)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    },
                    {
                        "userID", resultuser.UserID.ToString()
                    },
                    {
                        "userFullname", resultuser.UserFullname
                    },
                    {
                        "countryID", resultuser.CountryID.ToString()
                    },
                    {
                        "UserRole", "0"
                    },
                    {
                        "userUserID", resultuser.UserID.ToString()
                    },
                });
                identity.AddClaim(new Claim("sub", context.UserName));

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }

            if (resultuser.CountryID != null & resultuser.UserProfiles.Count() > 0 && !resultuser.UserProfiles.Any(m => m.UserRoleID == 1 || m.UserRoleID == 3))
            {
                UserProfileDTO resultProfile = resultuser.UserProfiles.Where(m => m.UserRoleID != 1).First();
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    },
                    {
                        "userFullName", resultuser.UserFullname
                    },
                    {
                        "userID", resultuser.UserID.ToString()
                    },
                    {
                        "countryID", "0"
                    },                    
                    {
                        "userUserID", resultProfile.UserID.ToString()
                    },
                    {
                        "Role", userRole
                    },
                    {
                        "UserRole", resultProfile.UserRoleID.ToString()
                    }
                });
                identity.AddClaim(new Claim("sub", context.UserName));

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }

            if (resultuser.CountryID != null & resultuser.UserProfiles.Count() > 0 && resultuser.UserProfiles.Any(m => m.UserRoleID == 3))
            {
                UserProfileDTO resultProfile = resultuser.UserProfiles.Where(m => m.UserRoleID == 3).First();
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    },
                    {
                        "userFullName", resultuser.UserFullname
                    },
                    {
                        "userID", resultuser.UserID.ToString()
                    }
                    ,
                    {
                        "countryID", "0"
                    },
                    {
                        "userUserID", resultProfile.UserID.ToString()
                    },
                    {
                        "Role", userRole
                    },
                    {
                        "UserRole", resultProfile.UserRoleID.ToString()
                    }
                });
                identity.AddClaim(new Claim("sub", context.UserName));

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }

            if (resultuser.CountryID != null & resultuser.UserProfiles.Count() > 0 && resultuser.UserProfiles.Any(m => m.UserRoleID == 1))
            {
                UserProfileDTO resultProfile = resultuser.UserProfiles.Where(m => m.UserRoleID == 1).First();
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    },
                    {
                        "userFullName", resultProfile.Profile.ProfileFullname
                    },
                    {
                        "userID", resultProfile.ProfileID.ToString()
                    }
                    ,
                    {
                        "countryID", "0"
                    },
                    {
                        "profileURL", resultProfile.Profile.ProfileURL
                    }
                    ,
                    {
                        "profileTypeID", resultProfile.Profile.ProfileTypeID.ToString()
                    }
                    ,
                    {
                        "profileIsConfirmed", resultProfile.Profile.ProfileIsConfirmed.ToString()
                    }
                    ,
                    {
                        "profileIsPeople", resultProfile.Profile.ProfileIsPeople.ToString()
                    },
                    {
                        "userUserID", resultProfile.UserID.ToString()
                    },
                    {
                        "Role", userRole
                    },
                    {
                        "UserRole", resultProfile.UserRoleID.ToString()
                    }
                });
                identity.AddClaim(new Claim("sub", context.UserName));

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }

            //if (resultProfile == null)
            //  if (resultUser.Count() == 0)
            //  {
            //      identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            //      var props = new AuthenticationProperties(new Dictionary<string, string>
            //      {
            //          {
            //              "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
            //          },
            //          {
            //              "userName", context.UserName
            //          }
            //      });
            //      identity.AddClaim(new Claim("sub", context.UserName));

            //      var ticket = new AuthenticationTicket(identity, props);
            //      context.Validated(ticket);
            //  }
            //  else if(resultUser.Count() == 1)
            //  {
            //      ProfileDTO resultProfile = resultUser.First().Profile;
            //      identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            //      var props = new AuthenticationProperties(new Dictionary<string, string>
            //      {
            //          {
            //              "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
            //          },
            //          {
            //              "userName", context.UserName
            //          },
            //          {
            //              "userFullName", resultProfile.ProfileFullname
            //          },
            //          {
            //              "userID", resultProfile.ProfileID.ToString()
            //          }
            //          ,
            //          {
            //              "profileURL", resultProfile.ProfileURL
            //          }
            //          ,
            //          {
            //              "profileTypeID", resultProfile.ProfileTypeID.ToString()
            //          }
            //          ,
            //          {
            //              "profileIsConfirmed", resultProfile.ProfileIsConfirmed.ToString()
            //          }
            //          ,
            //          {
            //              "profileIsPeople", resultProfile.ProfileIsPeople.ToString()
            //          },
            //          {
            //              "Role", userRole
            //          },
            //          {
            //              "UserRole", resultUser.First().UserRoleID.ToString()
            //          }
            //      }
            //);
            //      identity.AddClaim(new Claim("sub", context.UserName));

            //      var ticket = new AuthenticationTicket(identity, props);
            //      context.Validated(ticket);
            //  } else
            //  {
            //      ProfileDTO resultProfile = resultUser.First().Profile;
            //      identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            //      var props = new AuthenticationProperties(new Dictionary<string, string>
            //      {
            //          {
            //              "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
            //          },
            //          {
            //              "userName", context.UserName
            //          },
            //          {
            //              "userID", resultUser.First().UserID.ToString()
            //          },
            //          {
            //              "UserRole", resultUser.First().UserRoleID.ToString()
            //          }
            //      }
            //);
            //      identity.AddClaim(new Claim("sub", context.UserName));

            //      var ticket = new AuthenticationTicket(identity, props);
            //      context.Validated(ticket);
            //  }
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

    }
}