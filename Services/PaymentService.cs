using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Services.Specificatins;
using ServicesAbstrations;
using Shared.DataTransferObject.BasketModuleDtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentService(IConfiguration _configuration,
        IBasketRepository _basketRepository,
        IUnitOfWork _unitOfWork,
        IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            //Configure stripe  :install package Stripe.net
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            //get basket by basketId
            var Basket = await _basketRepository.GetBasketAsync(basketId) ?? throw new BasketNotFoundException(basketId);

            //Get amount=> Get Products + Delivery Method Cost
            var ProductRepo = _unitOfWork.GetRepository<Domain.Models.ProductModule.Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                item.Price = Product.Price; //Update price from product
            }
            ArgumentNullException.ThrowIfNull(Basket.deliveryMethodId);
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.deliveryMethodId.Value)
                    ?? throw new DeliveryMethodNotFoundException(Basket.deliveryMethodId.Value);

            Basket.shippingPrice = DeliveryMethod.Price;
            var BasketAmount = (long)(Basket.Items.Sum(item => item.Price * item.Quantity) + DeliveryMethod.Price) * 100;

            //Create PaymentIntent with [create-update]
            var PaymentService = new PaymentIntentService();
            if (Basket.paymentIntentId is null) //Create
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = BasketAmount, //in cents
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var PaymentIntent = await PaymentService.CreateAsync(options);
                Basket.paymentIntentId = PaymentIntent.Id;
                Basket.clientSecret = PaymentIntent.ClientSecret;
            }
            else//Update
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = BasketAmount, //in cents
                };
                await PaymentService.UpdateAsync(Basket.paymentIntentId, options);

            }

            //Update Basket in DB
            await _basketRepository.CreateOrUpdateBasketAsync(Basket);
            return _mapper.Map<BasketDto>(Basket);
        }

        public async Task UpdateOrderPaymentStatus(string JsonRequest, string StripeHeader)
        {
            var endPointSecret = _configuration["StripeSettings:EndPointSecret"];
            var StripeEvent = EventUtility.ConstructEvent(JsonRequest, StripeHeader, endPointSecret);

            var PaymentIntent = StripeEvent.Data.Object as PaymentIntent;
            switch (StripeEvent.Type)
            {
                case EventTypes.PaymentIntentPaymentFailed:
                    await UpdatePaymentFailedAsync(PaymentIntent.Id);
                    break;
                case EventTypes.PaymentIntentSucceeded:
                    await UpdatePaymentSuccesAsync(PaymentIntent.Id);
                    break;
                default:
                    Console.WriteLine("UnHandled Event ");
                    break;
            }
        }

        private async Task UpdatePaymentSuccesAsync(string id)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new OrderWithPaymentIntentSpecifications(id));

            order.status = OrderStatus.PaymentReceived;

            _unitOfWork.GetRepository<Order, Guid>().Update(order);

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task UpdatePaymentFailedAsync(string id)
        {
            var order =await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new OrderWithPaymentIntentSpecifications(id));

            order.status =OrderStatus.PaymentFailed;

            _unitOfWork.GetRepository<Order, Guid>().Update(order);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

