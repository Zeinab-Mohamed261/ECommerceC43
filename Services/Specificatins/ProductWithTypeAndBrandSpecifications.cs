using Domain.Models.ProductModule;
using Shared;
using Shared.DataTransferObject.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            : base(ApplyCriteria(parameters))  //Search
        {
            //Add Includes
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            ApplySorting(parameters);
            ApplyPagination(parameters.PageSize, parameters.pageIndex);
        }

        private static Expression<Func<Product, bool>> ApplyCriteria(ProductQueryParameters parameters)
        {
            return Product =>
                        (!parameters.BrandId.HasValue || Product.BrandId == parameters.BrandId.Value) &&
                        (!parameters.TypeId.HasValue || Product.TypeId == parameters.TypeId.Value) &&
                        (string.IsNullOrEmpty(parameters.Search) || Product.Name.ToLower().Contains(parameters.Search.ToLower()));
        }

        private void ApplySorting(ProductQueryParameters parameters)
        {
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
