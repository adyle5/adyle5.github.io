using AGBlog.Pages.Admin;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace AGBlog.Auth
{
    public class AGBlogAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime js;
        private readonly ILogger<AGBlogAuthenticationStateProvider> logger;

        public AGBlogAuthenticationStateProvider(IJSRuntime js, ILogger<AGBlogAuthenticationStateProvider> logger)
        {
            this.js = js;
            this.logger = logger;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            try
            {
                var json = await js.InvokeAsync<string>("agblog.GetLocalStorage", "agToken");

                if (string.IsNullOrWhiteSpace(json))
                {
                    return await AnonymousUser();
                }

                var userToken = JsonSerializer.Deserialize<Token>(json, options);
                if (userToken == null)
                {
                    return await AnonymousUser();
                }
                else
                {
                    // Check if the token is expired
                    if (DateTime.TryParse(userToken.expiration, out DateTime dt))
                    {
                        if (dt <= DateTime.Now)
                        {
                            await js.InvokeVoidAsync("agblog.DeleteLocalStorage", "agToken");
                            return await AnonymousUser();
                        }
                    }
                    else
                    {
                        await js.InvokeVoidAsync("agblog.DeleteLocalStorage", "agToken");
                        return await AnonymousUser();
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Thumbprint, userToken.token),
                        new Claim(ClaimTypes.Expiration, userToken.expiration),
                        new Claim(ClaimTypes.Role, "Administrator")
                    };


                    var user = new ClaimsIdentity(claims, "AGBlogAuthType");
                    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(user)));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return await AnonymousUser();
            }
        }

        public void NotifyAuthenticationStateChanged()
        {
            base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task<AuthenticationState> AnonymousUser()
        {
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        }
    }
}
