using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BasketModule
{
    public class CustomerBasket //مش عايزاه يتخزن في DB
    {
        public string Id { get; set; } //GUID :Created From Client [Frontend]
        public ICollection<BasketItems> Items { get; set; }
        public string? clientSecret { get; set; }
        public string? paymentIntentId { get; set; }
        public int? deliveryMethodId { get; set; }
        public decimal? shippingPrice { get; set; }
    }
}
