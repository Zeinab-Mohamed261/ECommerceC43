using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.ProductModule
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public ProductBrand ProductBrand { get; set; } //Navigatio Property [one]
        public int BrandId { get; set; } //FK
        public ProductType ProductType { get; set; } //Navigation Property [one]
        public int TypeId { get; set; } //FK
    }
}
