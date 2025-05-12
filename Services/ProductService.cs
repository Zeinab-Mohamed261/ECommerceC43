using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Specificatins;
using ServicesAbstrations;
using Shared;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandDtos = _mapper.Map<IEnumerable<ProductBrand> , IEnumerable<BrandDto>>(Brands);
            return BrandDtos;
        }

        public async Task<PaginationResponce<ProductDto>> GetAllProductsAsync(ProductQueryParameters Parameters)
        {
            var specifications = new ProductWithTypeAndBrandSpecifications(Parameters);
            var Products = await _unitOfWork.GetRepository<Product , int>().GetAllAsync(specifications);
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            var ProductCount = Products.Count();

            return new PaginationResponce<ProductDto>(Parameters.pageIndex, ProductCount, 0, Data);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType , int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specifications = new ProductWithTypeAndBrandSpecifications(id);
            var Product = await _unitOfWork.GetRepository<Product , int>().GetByIdAsync(specifications);
            return _mapper.Map<Product, ProductDto>(Product);
        }
    }
}
