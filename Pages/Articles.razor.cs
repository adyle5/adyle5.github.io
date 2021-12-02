using AGBlog.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AGBlog.Pages
{
    partial class Articles
    {
        [Inject]
        HttpClient http { get; set; }

        public IEnumerable<PostViewModel> Posts { get; set; }
        int pageOn = 1;
        string previous = "";
        string next = "";
        int pagingRows = 5; // This should come from AppSettings.

        protected async override Task OnInitializedAsync()
        {
            await GetPosts();
        }

        async Task GetPosts(Direction direction = Direction.NA)
        {
            switch (direction)
            {
                case Direction.Forward:
                    pageOn += 1;
                    break;
                case Direction.Backward:
                    pageOn -= 1;
                    break;
                case Direction.NA:
                    pageOn = 1;
                    break;
            }

            Posts = await http.GetFromJsonAsync<IEnumerable<PostViewModel>>($"/posts/page/{pageOn}");
            int rowCount = await http.GetFromJsonAsync<int>("/posts/count");
            int numberOfPages = CalculatePageCount(rowCount, pagingRows);

            if (pageOn == 1 && numberOfPages == 1)
            {
                previous = "display: none;";
                next = "display: none;";
            }
            else if (pageOn == 1 && numberOfPages != 1)
            {
                previous = "display: none;";
                next = "";
            }
            else if (pageOn == numberOfPages)
            {
                previous = "";
                next = "display: none;";
            }
            else
            {
                previous = "";
                next = "";
            }

            StateHasChanged();
        }

        int CalculatePageCount(int rowCount, int pagingRows)
        {
            if (rowCount <= pagingRows)
            {
                return 1;
            }

            if (rowCount == 0)
            {
                return 0;
            }

            double rawPages = (double)rowCount / pagingRows;

            return Convert.ToInt32(Math.Ceiling(rawPages));
        }
    }
}
