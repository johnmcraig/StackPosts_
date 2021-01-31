using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackPosts_.Api.Hubs;
using StackPosts_.Api.Middleware;
using StackPosts_.Infrastructure;
using StackPosts_.Api.Extensions;

namespace StackPosts_.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure();

            services.AddApplicationServices();

            services.AddSwaggerDocumentation();
            
            services.AddCors(opt =>
            {
               opt.AddPolicy("CorsPolicy", 
                   builder =>
                   {
                       builder
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .WithOrigins("http://localhost:8080",
                           "http://localhost:4200", 
                           "https://localhost:5002", 
                           "http://localhost:5003", "*");
                   });
            });

            services.AddControllers().AddNewtonsoftJson(opt => {
               opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            

            //services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseSwaggerDocumentation();

            app.UseBlazorFrameworkFiles();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                // endpoints.MapFallbackToController("Index", "Fallback");
                // endpoints.MapHub<PostHub>("/post-hub");
            });
        }
    }
}
