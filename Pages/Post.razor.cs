using AGBlog.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text;
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

        protected override async Task OnInitializedAsync()
        {
            post = await Http.GetFromJsonAsync<PostViewModel>($"/posts/{postlink}");

            // Move to DI pipeline.
            post.Content = parser.ConvertMarkdown(post.Content);

            StateHasChanged();
        }
    }
}
