using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using Services;
using ServicesAbstrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Persistence.Reposotories;
using Persistence.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Persistence.Identity;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public static class InfrastructureServiceRegistiration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services , IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(options =>
            {
                var connnectionString = Configuration.GetConnectionString("DefaultConnection");//access appsetting
                options.UseSqlServer(connnectionString);
            });
            Services.AddScoped<IDataSeeding, DataSeeding>(); // DI for seeding data
            Services.AddScoped<IUnitOfWork, UnitOfWork>(); // DI for unit of work
            Services.AddScoped<IBasketRepository, BasketRepository>(); // DI for basket repository
            Services.AddScoped<ICacheRepository, CacheRepository>();
            Services.AddSingleton<IConnectionMultiplexer>((_)=>
            {
                return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnection"));
            });
            Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                var connsctionString = Configuration.GetConnectionString("IdentityConnection");
                options.UseSqlServer(connsctionString);
            });
            Services.AddIdentityCore<ApplicationUser>(options =>
            {

            })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();
            return Services;

            
        }
    }
}
