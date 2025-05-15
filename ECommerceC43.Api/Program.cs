
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Reposotories;
using Services;
using Services.MappingProfiles;
using ServicesAbstrations;

namespace ECommerceC43.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                var connnectionString = builder.Configuration.GetConnectionString("DefaultConnection");//access appsetting
                options.UseSqlServer(connnectionString);
            });
            #region DI
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); 
            builder.Services.AddScoped<IDataSeeding, DataSeeding>(); // DI for seeding data
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // DI for unit of work
            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly); // DI for automapper
            builder.Services.AddScoped<IServiceManager, ServiceManager>(); // DI for service manager
            #endregion

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var objectOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            objectOfDataSeeding.SeedData(); // call seeding data method


            // Configure the HTTP request pipeline.
            #region MiddleWare

            ////Custom Middleware
            //app.Use(async (RequestContext, NextMiddleware) =>
            //{
            //    Console.WriteLine("Request Under Processing");
            //    await NextMiddleware.Invoke();
            //    Console.WriteLine("Waiting Response");
            //    Console.WriteLine(RequestContext.Response.Body);
            //});
            // use CustomExceptionHandlerMiddlware we Do to handle exception
            app.UseMiddleware<CustomExceptionHandlerMiddlware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
