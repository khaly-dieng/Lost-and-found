using Application.Abstractions;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<ILostAndFoundDbContext>(provider => provider.GetRequiredService<LostAndFoundDbContext>());
            return services;
        }
    }
}
