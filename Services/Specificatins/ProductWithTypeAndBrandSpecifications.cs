using Domain.Models;
using Shared;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specificatins
{
    internal class ProductWithTypeAndBrandSpecifications : BaseSpecfication<Product,int>
    {
        //use this ctor to create query to get product by id
        public ProductWithTypeAndBrandSpecifications(int id) 
            :base(product => product.Id == id)
        {
            //Add Includes
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        //use this ctor to create query to get all products
        public ProductWithTypeAndBrandSpecifications(ProductQueryParameters parameters)
            :base(Product =>
            (!parameters.BrandId.HasValue || Product.BrandId == parameters.BrandId.Value)&&
            (!parameters.TypeId.HasValue  || Product.TypeId  == parameters.TypeId.Value)
            ) 
        {
            //Add Includes
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (parameters.Options)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
        }
    }
}
