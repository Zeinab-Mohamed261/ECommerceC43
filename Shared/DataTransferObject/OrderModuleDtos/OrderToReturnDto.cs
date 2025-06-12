using Shared.DataTransferObject.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.OrderModuleDtos
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public string buyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; }
        public AddressDto shipToAddress { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public string status { get; set; } = default!;
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal deliveryCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = default!;
    }
}
