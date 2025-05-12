using Shared;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstrations
{
    public interface IProductService
    {
        // Get All Products
        Task<PaginationResponce<ProductDto>> GetAllProductsAsync(ProductQueryParameters queryParameters);
        //Get Product By Id
        Task<ProductDto> GetProductByIdAsync(int id);
        //Get All Brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        //Get All Types
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    }
}
