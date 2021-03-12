using AGBlog.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace AGBlog.Pages.Admin
{
    partial class Login
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }

        [Inject]
        public NavigationManager nav { get; set; }

        [Inject]
        public AuthenticationStateProvider authProvider {get; set;}

        LoginModel loginModel = new LoginModel();

        string loginError = "";

        private async Task Submit()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            string jsonString = JsonSerializer.Serialize(loginModel, options);
            var response = await Http.PostAsync("/api/Authentication/login", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string tokenJson = await response.Content.ReadAsStringAsync();
                Token token = JsonSerializer.Deserialize<Token>(tokenJson);

                // Save the token to local storage
                await JS.InvokeVoidAsync("agblog.DeleteLocalStorage", "agToken");
                await JS.InvokeVoidAsync("agblog.SetLocalStorage", "agToken", token);

                loginError = "";

                ((AGBlogAuthenticationStateProvider)authProvider).NotifyAuthenticationStateChanged();

                nav.NavigateTo("/admin/posts");
            }
            else
            {
                loginError = "Either the username or password you entered is incorrect!";
            }
        }
    }
}
