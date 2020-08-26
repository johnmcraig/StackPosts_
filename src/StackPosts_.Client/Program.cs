using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

            builder.Services.AddTransient(typeof(IRepositoryService<>), typeof(RepositoryService<>));
            builder.Services.AddTransient<IPostService, PostService>();
            builder.Services.AddTransient<IReplyService, ReplyService>();
            builder.Services.AddOptions();

            await builder.Build().RunAsync();
        }
    }
}
