
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Reposotories;
using Services;
using Services.MappingProfiles;
using ServicesAbstrations;
using Shared.ErrorModels;

namespace ECommerceC43.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            #region DI
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices(); // DI for swagger

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddAplicationServices(); // DI for application services

            builder.Services.AddWebApplicationServices(); // DI for web application services
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
