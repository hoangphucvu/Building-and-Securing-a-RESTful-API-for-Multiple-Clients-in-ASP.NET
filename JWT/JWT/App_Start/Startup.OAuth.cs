using System;
using System.Configuration;
using JWT.Core;
using JWT.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace JWT
{
    public partial class Startup
    {
        /// <summary>
        /// //Issuer – a unique identifier for the entity that issued the token (not to be confused with Entity Framework’s entities)
        ///Secret – a secret key used to secure the token and prevent tampering
        ///State who is the audience(we’re specifying “Any” for the audience, as this is a required field but we’re not fully implementing it).
        ///State who is responsible for generating the tokens.Here we’re using SymmetricKeyIssuerSecurityTokenProvider and passing it our secret key to prevent tampering.
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = ConfigurationManager.AppSettings["issuer"];
            var secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);
            app.CreatePerOwinContext(() => new BooksContext());
            app.CreatePerOwinContext(() => new BookUserManager());
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions()
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { "Any" },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer,secret)
                }
            });

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJsFormat(issuer)
            });
        }
    }
}