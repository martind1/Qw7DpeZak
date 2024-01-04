using Microsoft.AspNetCore.Components.Authorization;
using DpeZak.Database.Authentification.Models;
using Serilog;
using System.Security.Claims;

namespace DpeZak.Services.Security
{
    public class ApplicationAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly SecurityService securityService;
        private ApplicationAuthenticationState authenticationState;

        public ApplicationAuthenticationStateProvider(SecurityService securityService)
        {
            this.securityService = securityService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            try
            {
                var state = await GetApplicationAuthenticationStateAsync();

                if (state.IsAuthenticated)
                {
                    Log.Information($"IsAuthenticated: ");
                    identity = new ClaimsIdentity(state.Claims.Select(c => new Claim(c.Type, c.Value)), "DpeZak");
                }
                else Log.Information($"NOT IsAuthenticated: ");
            }
            catch (HttpRequestException ex)
            {
                //MD
                Log.Error(ex.Message);
                throw;
            }

            var result = new AuthenticationState(new ClaimsPrincipal(identity));

            await securityService.InitializeAsync(result);

            return result;
        }

        private async Task<ApplicationAuthenticationState> GetApplicationAuthenticationStateAsync()
        {
            authenticationState ??= await securityService.GetAuthenticationStateAsync();

            return authenticationState;
        }
    }
}