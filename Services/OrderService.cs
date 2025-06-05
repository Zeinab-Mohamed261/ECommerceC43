using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using ServicesAbstrations;
using Shared.DataTransferObject.IdentityDtos;
using Shared.DataTransferObject.OrderModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IMapper _mapper , IBasketRepository _basketRepository , IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string email)
        {
            //Map address to order Address
            var OrderAddress = _mapper.Map<AddressDto , OrderAddress>(orderDto.Address);

            // Get Basket
            var Basket =await _basketRepository.GetBasketAsync(orderDto.BasketId) 
                ?? throw new BasketNotFoundException(orderDto.BasketId);


            //Create order items list
            List<OrderItem> OrderItems = []; 
            var  ProductRepo = _unitOfWork.GetRepository<Product ,int>();

            foreach(var item in Basket.Items)
            {
                //Get Product
                var Product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                //Create Order Item
                var OrderItem = new OrderItem()
                {
                    Product = new ProductItemOrderd()
                    {
                        ProductId = Product.Id,
                        PictureUrl = Product.PictureUrl,
                        ProductName = Product.Name,
                    },
                    Price = Product.Price,
                    Quantity = item.Quantity
                };
                OrderItems.Add(OrderItem);
            }


            //Get Delivery Method

            var DeliveryMethod =await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            // Calculation sub total
            var SubTotal = OrderItems.Sum(item => item.Quantity * item.Price);

            var Order = new Order(email, OrderAddress, DeliveryMethod, OrderItems, SubTotal);

            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
            //Save Changes
            await _unitOfWork.SaveChangesAsync();

            //Map Order to OrderToReturnDto
            return _mapper.Map<Order, OrderToReturnDto>(Order);

        }
    }
}
