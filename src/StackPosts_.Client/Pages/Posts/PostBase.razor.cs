using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using StackPosts_.Client.Models;
using StackPosts_.Client.Services;
using StackPosts_.Client.Static;

namespace StackPosts_.Client.Pages.Posts
{
    public class PostBase : ComponentBase
    {
        [Inject] private HttpClient Http { get; set; }
        // [Inject] private PostService PostService { get; set; }
        
        [Parameter] 
        public int Id { get; set; }
        
        [Parameter]
        public PostModel PostScore { get; set; }

        protected IList<PostModel> PostModelList { get; set; }
        

        protected override async Task OnInitializedAsync()
        {
            // PostModelList = await PostService.GetAll(Endpoints.PostsEndpoint);
            // PostModelDetails = await PostService.GetSingle(Endpoints.PostsEndpoint, Id);
            PostModelList = await Http.GetFromJsonAsync<IList<PostModel>>("api/posts");
            // PostModelDetails = await Http.GetFromJsonAsync<PostModel>($"api/posts/{Id}");
        }

        protected void UpVote()
        {
            PostScore.Score++;
        }

        protected void DownVote()
        {
            PostScore.Score--;
        }
    }
}