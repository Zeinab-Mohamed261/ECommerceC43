﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.ProductModule
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
    }
}
