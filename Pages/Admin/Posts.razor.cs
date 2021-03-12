using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text;
using AGBlog.ViewModels;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http.Headers;
using AGBlazorComponents;

namespace AGBlog.Pages.Admin
{
    partial class Posts : IDisposable
    {
        [Inject]
        NavigationManager Nav { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        ICollection<PostViewModel> PostsVM { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }

        int idToDelete;
        AGModal modal;
        string deleteErrorMessage = "";

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            if (user.Identity.IsAuthenticated && user.Claims.Any(c => c.Type == ClaimTypes.Thumbprint))
            {
                string token = user.Claims.Where(c => c.Type == ClaimTypes.Thumbprint).FirstOrDefault().Value;

                Http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                PostsVM = await Http.GetFromJsonAsync<List<PostViewModel>>("/Posts");
            }
            else
            {
                Nav.NavigateTo("/admin/login");
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                modal.EventTrigger += DeletePost;
            }
        }

        async Task ShowDeleteModal(int id)
        {
            idToDelete = id;
            await modal.OpenModal();
        }

        async Task NavigateToPost(int id)
        {
            Nav.NavigateTo($"/admin/edit/{id}");
            await Task.CompletedTask;
        }

        async Task DeleteClicked()
        {
            if (idToDelete > 0)
            {
                await modal.Trigger(idToDelete);
            }

            await Task.CompletedTask;
        }

        async void DeletePost(object sender, EventArgs e)
        {
            if (e != EventArgs.Empty)
            {
                ModalTriggerEventArgs eventArgs = (ModalTriggerEventArgs)e;
                int id = Convert.ToInt32(eventArgs.Obj);

                var authState = await AuthenticationStateTask;
                var user = authState.User;

                if (user.Identity.IsAuthenticated && user.Claims.Any(c => c.Type == ClaimTypes.Thumbprint))
                {
                    string token = user.Claims.Where(c => c.Type == ClaimTypes.Thumbprint).FirstOrDefault().Value;

                    Http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                    var response = await Http.DeleteAsync($"/Posts/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        PostsVM = await Http.GetFromJsonAsync<List<PostViewModel>>("/Posts");
                        await modal.CloseModal();
                    }
                    else
                    {
                        deleteErrorMessage = $"Unable to delete post. Reason: {response.StatusCode} - {response.ReasonPhrase}.";
                    }
                } 
            }

            await modal.CloseModal();
            StateHasChanged();
        }

        public void Dispose()
        {
            if (modal != null)
            {
                modal.EventTrigger -= DeletePost;
            }    
        }
    }
}
