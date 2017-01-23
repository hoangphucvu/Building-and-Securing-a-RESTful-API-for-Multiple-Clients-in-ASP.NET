using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using JWT.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace JWT.Identity
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var user = context.OwinContext.Get<BooksContext>().Users.FirstOrDefault(u => u.UserName == context.UserName);
            if (!context.OwinContext.Get<BookUserManager>().CheckPassword(user, context.Password))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            var ticket = new AuthenticationTicket(SetClaimsIdentity(context, user), new AuthenticationProperties());
            context.Validated(ticket);

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return base.ValidateClientAuthentication(context);
        }

        public static ClaimsIdentity SetClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context,
            IdentityUser user)
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));

            var userRoles = context.OwinContext.Get<BookUserManager>().GetRoles(user.Id);
            foreach (var role in userRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return identity;
        }
    }
}