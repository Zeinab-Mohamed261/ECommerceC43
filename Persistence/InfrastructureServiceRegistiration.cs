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
            return Services;
        }
    }
}
