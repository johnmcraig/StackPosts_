using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackPosts_.Api.Data;
using StackPosts_.Api.Hubs;
using StackPosts_.Api.Middleware;
using StackPosts_.Core.Interfaces;
using StackPosts_.Infrastructure;
using StackPosts_.Infrastructure.Data;

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
            services.AddDbContext<PostsDbContext>();

            services.AddInfrastructure();
            
            services.AddCors();

            services.AddSignalR();

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV" );

            services.AddSwaggerGen();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseCors(x => 
                x.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:8080")
            );

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StackPosts API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "StackPosts API V2");
            });
 
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            //app.UseSwaggerDocumention();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PostHub>("/post-hub");
            });
        }
    }
}
