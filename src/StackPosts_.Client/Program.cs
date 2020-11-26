using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StackPosts_.Client.Contracts;
using StackPosts_.Client.Services;

namespace StackPosts_.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IReplyService, ReplyService>();
            builder.Services.AddOptions();

            await builder.Build().RunAsync();
        }
    }
}
