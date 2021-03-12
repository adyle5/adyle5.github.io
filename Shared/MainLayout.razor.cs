using AGBlog.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace AGBlog.Shared
{
    partial class MainLayout : IDisposable
    {
        [Inject]
        NavigationManager nav { get; set; }

        [Inject]
        IJSRuntime js { get; set; }

        [Inject]
        public AuthenticationStateProvider authProvider { get; set; }

        string homeSelected, stackSelected, photographySelected, aboutSelected, logoutSelected;
        string linkColor = "background-color: lightgreen; color: black;";

        protected override void OnInitialized()
        {
            homeSelected = linkColor;
            nav.LocationChanged += LocationChanged;
        }

        void LocationChanged(object sender, LocationChangedEventArgs e)
        {
            //string navigationMethod = e.IsNavigationIntercepted ? "HTML" : "code";
            //System.Diagnostics.Debug.WriteLine($"Notified of navigation via {navigationMethod} to {e.Location}");

            Uri uri = new Uri(e.Location);
            var lastSegment = uri.Segments[uri.Segments.Length - 1];

            switch (lastSegment)
            {
                case "/":
                    homeSelected = linkColor;
                    stackSelected = photographySelected = aboutSelected = logoutSelected = "";
                    break;
                case "stack":
                    stackSelected = linkColor;
                    homeSelected = photographySelected = aboutSelected = logoutSelected = "";
                    break;
                case "photography":
                    photographySelected = linkColor;
                    homeSelected = stackSelected = aboutSelected = logoutSelected = "";
                    break;
                case "about":
                    aboutSelected = linkColor;
                    homeSelected = stackSelected = photographySelected = logoutSelected = "";
                    break;
                case "logout":
                    logoutSelected = linkColor;
                    aboutSelected = homeSelected = stackSelected = photographySelected = "";
                    break;
                default:
                    homeSelected = stackSelected = photographySelected = aboutSelected = "";
                    break;
            }

            StateHasChanged();
        }
        void IDisposable.Dispose()
        {
            nav.LocationChanged -= LocationChanged;
        }

        async Task Logout()
        {
            await js.InvokeVoidAsync("agblog.DeleteLocalStorage", "agToken");
            ((AGBlogAuthenticationStateProvider)authProvider).NotifyAuthenticationStateChanged();
        }
    }
}
