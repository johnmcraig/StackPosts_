using Microsoft.Extensions.DependencyInjection;
using StackPosts_.Core.Interfaces;
using StackPosts_.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackPosts_.Infrastructure
{
    public static class DependencyInjectionFactory
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddDbContext<StoreContext>();

            return services;
        }
    }
}
