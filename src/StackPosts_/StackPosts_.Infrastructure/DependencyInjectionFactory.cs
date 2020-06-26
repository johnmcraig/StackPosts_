using Microsoft.Extensions.DependencyInjection;
using StackPosts_.Core.Interfaces;
using StackPosts_.Infrastructure.Data;

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
