using Shared.DataTransferObject.OrderModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstrations
{
    public interface IOrderService
    {
        //Create Order
        //Take Basket Id , Shipping Address , Delivery Method Id , Customer Email 
        //Return Order Details (Id , UserName , OrderDate , Items (Product Name - Picture Url - Price - Quantity) ,
        //Address , Delivery Method Name , Order Status Value , Sub Total , Total Price  )

        Task<OrderToReturnDto> CreateOrder(OrderDto orderDto , string email);

        //Get Delivery Methods
        Task<IEnumerable<DeliveryMethodsDto>> GetDeliveryMethodAsync();

        //Get All Orders 
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email);

        //Get Order By Id
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);

    }
}
