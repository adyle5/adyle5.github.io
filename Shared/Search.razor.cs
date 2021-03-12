using AGBlazorComponents;
using AGBlog.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AGBlog.Shared
{
    partial class Search
    {
        [Inject]
        HttpClient Http { get; set; }

        AGModal modal;
        ICollection<PostViewModel> Posts = null;


        string searchTerm = "";

        async Task SearchClicked()
        {
            if (searchTerm != null)
            {
                var response = await Http.PostAsJsonAsync($"/posts/search/", searchTerm);
                if (!response.IsSuccessStatusCode)
                {
                    // return error message
                }
                else
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        PropertyNameCaseInsensitive = true
                    };

                    string responseJson = await response.Content.ReadAsStringAsync();
                    Posts = JsonSerializer.Deserialize<IEnumerable<PostViewModel>>(responseJson, options).ToList();

                    await ShowModal();
                }
            }
        }

        async Task SearchKeyPress(KeyboardEventArgs args)
        {
            if (args.Code == "Enter")
            {
                await SearchClicked();
            }
        }

        async Task ShowModal()
        {
            await modal.OpenModal();
        }

        async Task CloseModal()
        {
            await modal.CloseModal();
        }
    }
}