using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using ServicesAbstrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(ProductProfile).Assembly); // DI for automapper
            Services.AddScoped<IServiceManager, ServiceManager>(); // DI for service manager
            return Services;
        }
    }
}
