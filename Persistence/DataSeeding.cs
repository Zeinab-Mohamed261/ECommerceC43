using Domain.Contracts;
using Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
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
    }
}
