﻿using AutoMapper;
using Domain.Models.ProductModule;
using Shared.DataTransferObject.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.productBrand, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.productType, options => options.MapFrom(src => src.ProductType.Name));

            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();

        }
    }
}
