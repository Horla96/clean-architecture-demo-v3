using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.IdentityModels;
using Persistence.Seeds;
using Persistence.SharedServices;

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

            services.AddDataProtection();
            services.AddIdentityCore<ApplicationUser>()
             .AddRoles<ApplicationRole>()
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(30);
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddTransient<IAccountService, AccountService>();
            //seeds roles and users
            DefaultRoles.SeedRolesAsync(services.BuildServiceProvider()).Wait();
            DefaultUsers.SeedUsersAsync(services.BuildServiceProvider()).Wait();
        }
    }
}
