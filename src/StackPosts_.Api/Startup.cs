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

            services.AddControllers();

            services.AddApplicationServices();

            services.AddSwaggerDocumentation();
            
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", opt =>
                {
                    opt.AllowAnyHeader().AllowAnyMethod()
                    .WithOrigins( "http://localhost:8080", "http://localhost:4200");
                });
            });

            //services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            
            //app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapFallbackToController("Index", "Fallback");
                //endpoints.MapHub<PostHub>("/post-hub");
            });
        }
    }
}
