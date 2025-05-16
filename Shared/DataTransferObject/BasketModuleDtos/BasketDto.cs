using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.BasketModuleDtos
{
    public class BasketDto
    {
        public string Id { get; set; } //GUID :Created From Client [Frontend]
        public ICollection<BasketItemDto> Items { get; set; }
    }
}
