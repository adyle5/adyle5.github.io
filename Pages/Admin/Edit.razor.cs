using AGBlazorComponents;
using AGBlog.ViewModels;
using AGMarkdown;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace AGBlog.Pages.Admin
{
    partial class Edit
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        NavigationManager Nav { get; set; }

        [Inject]
        IParser parser { get; set; }

        [Parameter]
        public int Id { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }

        PostViewModel post;
        string errorMessage = "";
        string previewText = "";
        AGModal modal;

        protected override async Task OnParametersSetAsync()
        {
            if (Id > 0)
            {
                var authState = await AuthenticationStateTask;
                var user = authState.User;

                if (user.Identity.IsAuthenticated && user.Claims.Any(c => c.Type == ClaimTypes.Thumbprint))
                {
                    string token = user.Claims.Where(c => c.Type == ClaimTypes.Thumbprint).FirstOrDefault().Value;

                    Http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);

                    post = await Http.GetFromJsonAsync<PostViewModel>($"/Posts/{Id}");
                }
                else
                {
                    Nav.NavigateTo("/admin/login");
                }
            }
            else
            {
                post = new PostViewModel();
            }
        }

        async Task Submit()
        {
            if (post != null)
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true
                };

                var authState = await AuthenticationStateTask;
                var user = authState.User;

                if (user.Identity.IsAuthenticated && user.Claims.Any(c => c.Type == ClaimTypes.Thumbprint))
                {
                    string token = user.Claims.Where(c => c.Type == ClaimTypes.Thumbprint).FirstOrDefault().Value;

                    Http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response;

                    if (Id > 0)
                    {
                        response = await Http.PutAsJsonAsync<PostViewModel>($"/Posts/{post.ID}", post, options);
                    }
                    else
                    {
                        response = await Http.PostAsJsonAsync<PostViewModel>($"/Posts", post, options);
                    }
                

                    if (response.IsSuccessStatusCode)
                    {
                        Nav.NavigateTo("/admin/posts");
                    }
                    else
                    {
                        errorMessage = $"Error updating article. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}";
                    }
                }

                await Task.CompletedTask;
            }
        }

        async Task Preview()
        {
            if (post != null && !string.IsNullOrEmpty(post.Content))
            {
                previewText = parser.ConvertMarkdown(post.Content);
                await modal.OpenModal();
            }
        }
    }
}
