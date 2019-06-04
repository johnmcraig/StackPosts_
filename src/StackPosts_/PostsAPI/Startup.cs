using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PostsAPI.Data;
using PostsAPI.Hubs;
using Swashbuckle.AspNetCore.Swagger;

namespace PostsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PostsDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("sqlConString"));
            });

            services.AddScoped<IPostRepository, PostRepository>();

            services.AddCors();
            services.AddSignalR();

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV" );

            services.AddApiVersioning(config => {
                    config.ReportApiVersions = true;
                    config.AssumeDefaultVersionWhenUnspecified = true;
                    config.DefaultApiVersion = new ApiVersion(1, 0);
                    config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddSwaggerGen(options => {
                
                var provider = services.BuildServiceProvider()
                    .GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName, new Info
                        { 
                            Title = $"Posts API {description.ApiVersion}", 
                            Version = description.ApiVersion.ToString() 
                        });
                    }
            });

            services.AddTransient<DataSeed>();

            services.AddMvc().
                SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataSeed seedPosts) //
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x => 
                x.WithOrigins("http://localhost:8080")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.UseSignalR(route =>
            {
                route.MapHub<PostHub>("/post-hub");
            });
            
            seedPosts.SeedData().Wait();
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
