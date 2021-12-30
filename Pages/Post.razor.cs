using AGBlog.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AGMarkdown;

namespace AGBlog.Pages
{
    partial class Post
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        IParser parser { get; set; }

        [Parameter]
        public string postlink { get; set; }

        PostViewModel post;
        string message = "Searching for article...";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                post = await Http.GetFromJsonAsync<PostViewModel>($"/posts/{postlink}");
                post.Content = parser.ConvertMarkdown(post.Content);
            }
            catch (HttpRequestException)
            {
                message = "Content not available";
            }

            StateHasChanged();
        }
    }
}
