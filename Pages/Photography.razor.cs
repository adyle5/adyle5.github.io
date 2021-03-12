using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AGBlazorComponents;

namespace AGBlog.Pages
{
    partial class Photography
    {
        [Inject]
        LocalClient Local { get; set; }

        List<Photo> photos;

        protected override async Task OnInitializedAsync()
        {
            photos = await Local.GetFromJsonAsync<List<Photo>>("data/photos.json");
        }
    }
}
