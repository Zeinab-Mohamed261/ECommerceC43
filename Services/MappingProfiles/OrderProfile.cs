using AutoMapper;
using Domain.Models.OrderModule;
using Shared.DataTransferObject.IdentityDtos;
using Shared.DataTransferObject.OrderModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class OrderProfile :Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                     .ForMember(D => D.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.PictureUrl, O => O.MapFrom(S => S.Product.PictureUrl));

            CreateMap<DeliveryMethod, DeliveryMethodsDto>();

        }
    }
}
