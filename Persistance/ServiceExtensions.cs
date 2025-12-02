using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // register services
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
                ));

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }
    }
}
