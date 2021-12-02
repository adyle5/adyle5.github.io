using AGBlog.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AGBlog.Pages
{
    partial class Index
    {
        [Inject]
        HttpClient http { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await http.GetFromJsonAsync<IEnumerable<PostViewModel>>($"/posts/page/1");
        }
    }
}
