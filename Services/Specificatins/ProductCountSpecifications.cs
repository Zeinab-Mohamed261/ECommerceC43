using Domain.Models.ProductModule;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specificatins
{
    public class ProductCountSpecifications :BaseSpecfication<Product , int>
    {
        public ProductCountSpecifications(ProductQueryParameters parameters):base(ApplyCriteria(parameters))
        {
            
        }
        private static Expression<Func<Product, bool>> ApplyCriteria(ProductQueryParameters parameters)
        {
            return Product =>
                        (!parameters.BrandId.HasValue || Product.BrandId == parameters.BrandId.Value) &&
                        (!parameters.TypeId.HasValue || Product.TypeId == parameters.TypeId.Value) &&
                        (string.IsNullOrEmpty(parameters.Search) || Product.Name.ToLower().Contains(parameters.Search.ToLower()));
        }
    }
}
