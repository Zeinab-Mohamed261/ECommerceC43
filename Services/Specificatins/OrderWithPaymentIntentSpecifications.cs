using Domain.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specificatins
{
    public class OrderWithPaymentIntentSpecifications:BaseSpecfication<Order,Guid>
    {
        public OrderWithPaymentIntentSpecifications(string paymentIntentId):base(O => O.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
