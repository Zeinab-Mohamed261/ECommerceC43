using Domain.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specificatins
{
    public class OrderSpecifications :BaseSpecfication<Order , Guid>
    {
        public OrderSpecifications(string email):base(o => o.buyerEmail == email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }
        public OrderSpecifications(Guid id):base(o => o.Id == id) 
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }
    }
}
