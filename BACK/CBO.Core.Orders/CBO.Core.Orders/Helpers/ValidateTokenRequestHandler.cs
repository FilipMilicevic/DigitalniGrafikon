using CBO.Core.Orders.DataAccess;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using OpenIddict.Abstractions;
using OpenIddict.Server;
using OpenIddict.Validation.AspNetCore;
using System.Security.Claims;

namespace CBO.Core.Orders.API.Helpers
{
    public class ValidateTokenRequestHandler : IOpenIddictServerHandler<OpenIddictServerEvents.ValidateTokenRequestContext>
    {
        public ValueTask HandleAsync(OpenIddictServerEvents.ValidateTokenRequestContext context)
        {
            // reject token requests that don't use grant_type=client_credentials.
            if (!context.Request.IsClientCredentialsGrantType())
            {
                context.Reject
                (
                    error: OpenIddictConstants.Errors.UnsupportedGrantType,
                    description: "Auth_InvalidGrantType"
                );
            }

            return default;
        }
    }

    public class HandleTokenRequestHandler : IOpenIddictServerHandler<OpenIddictServerEvents.HandleTokenRequestContext>
    {
        private readonly IOrdersDataAccess service;

        public HandleTokenRequestHandler(IOrdersDataAccess service)
        {
            this.service = service;
        }
        public ValueTask HandleAsync(OpenIddictServerEvents.HandleTokenRequestContext context)
        {
            string sessionNonce = Guid.NewGuid().ToString("N");
            if (context.Request.ClientId == null || context.Request.ClientSecret == null)
            {
                context.Reject
                (
                    error: OpenIddictConstants.Errors.InvalidRequest
                );
                return default;
            }
            var login = service.Login(context.Request.ClientId, context.Request.ClientSecret);

            login.Wait();

            SetPrincipal(context, login.Result, sessionNonce);

            return default;

        }

        internal void SetPrincipal(OpenIddictServerEvents.HandleTokenRequestContext context, string userId, string sessionNonce)
        {
            var identity = new ClaimsIdentity
            (
            new[]
            {
                    new Claim(OpenIddictConstants.Claims.Subject, userId),
                    new Claim(OpenIddictConstants.Claims.Role, userId),
                    new Claim("SessionNonce", sessionNonce)
            },
                OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme
            );


            foreach (var claim in identity.Claims)
            {
                if (claim.Type == "SessionNonce")
                {
                    claim.SetDestinations(OpenIdConnectParameterNames.RefreshToken);
                }
                else
                {
                    claim.SetDestinations(OpenIdConnectParameterNames.AccessToken);
                }
            }

            context.Principal = new ClaimsPrincipal(identity);
            context.Principal.SetScopes
            (
                new[]
                {
                    OpenIddictConstants.Scopes.OpenId
                }
            );
        }
    }

    public class ApplyTokenResponseHandler : IOpenIddictServerHandler<OpenIddictServerEvents.ApplyTokenResponseContext>
    {
        public ValueTask HandleAsync(OpenIddictServerEvents.ApplyTokenResponseContext context)
        {
            return default;
        }
    }
}

