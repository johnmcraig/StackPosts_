using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using StackPosts_.Client.Contracts;
using StackPosts_.Client.Models;
using StackPosts_.Client.Static;

namespace StackPosts_.Client.Pages.Posts
{
    public class PostBase : ComponentBase
    { 
        [Inject] private IPostService PostService { get; set; }
        [Inject] private NavigationManager NavManager { get; set; }

        [Parameter]
        public int Id { get; set; }

        protected PostModel PostModel = new PostModel();

        protected IList<PostModel> PostModelList { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            PostModelList = await PostService.GetAll(Endpoints.PostsEndpoint);
        }

        protected void BackToList()
        {
            NavManager.NavigateTo("/posts/");
        }

        protected void UpVote()
        {
            PostModel.Score++;
        }

        protected void DownVote()
        {
            PostModel.Score--;
        }
    }
}