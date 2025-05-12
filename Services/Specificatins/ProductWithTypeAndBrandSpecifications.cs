using Domain.Models;
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
        public ProductWithTypeAndBrandSpecifications()
            :base(null) // no filter
        {
            //Add Includes
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
