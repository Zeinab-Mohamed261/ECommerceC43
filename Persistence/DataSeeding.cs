using Domain.Contracts;
using Domain.Models.IdentityModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext ,
                            UserManager<ApplicationUser> _userManager ,
                            RoleManager<IdentityRole> _roleManager,
                            StoreIdentityDbContext _identityDbContext) : IDataSeeding
    {
        public void SeedData()
        {
			try
			{

                // check migration is applides and updared db [table exist in db]
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }
                //exist data in tables

                //ProductBrand

                if (!_dbContext.ProductBrands.Any()) //no data is exist [empty table]
                {
                    var ProductBrandData = File.ReadAllText(@"..\Persistence\\Data\\DataSeed\\brands.json");
                    // covert json to c# file [deseralise]
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands != null && ProductBrands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(ProductBrands);
                    }
                }

                //ProductType

                if (!_dbContext.ProductTypes.Any()) //no data is exist [empty table]
                {
                    var ProductTypesData = File.ReadAllText(@"..\Persistence\\Data\\DataSeed\\types.json");
                    // covert json to c# file [deseralise]
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);
                    if (ProductTypes != null && ProductTypes.Any())
                    {
                        _dbContext.ProductTypes.AddRange(ProductTypes);
                    }
                }

                //Product

                if (!_dbContext.Products.Any()) //no data is exist [empty table]
                {
                    var ProductsData = File.ReadAllText(@"..\Persistence\\Data\\DataSeed\\products.json");
                    // covert json to c# file [deseralise]
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (Products != null && Products.Any())
                    {
                        _dbContext.Products.AddRange(Products);
                    }
                }


                _dbContext.SaveChanges();
            }
			catch (Exception ex)
			{
                //TODO
			}
        }
        public async void IdentityDataSeed()
        {
            if(!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if(!_userManager.Users.Any())
            {
                try
                {
                    var user01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Ahmed",
                        PhoneNumber = "1234567890",
                        UserName = "MohamedAhmed",

                    };
                    var user02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Ahmed",
                        PhoneNumber = "1234567890",
                        UserName = "SalmaAhmed",

                    };
                    //Create User
                    await _userManager.CreateAsync(user01, "P@$$w0rd");
                    await _userManager.CreateAsync(user02, "P@$$w0rd");
                    //Assign Role To User
                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");
                    //Update DB
                    await _identityDbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}
