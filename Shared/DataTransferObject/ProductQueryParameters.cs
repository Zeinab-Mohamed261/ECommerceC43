using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public class ProductQueryParameters
    {
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;
        private int pageSize = DefaultPageSize;

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions Options { get; set; }
        public string? Search { get; set; }
        public int pageIndex { get; set; } = 1; // default value
        public int PageSize 
        {
            get => pageSize;
            set => pageSize = value> 0 && value <MaxPageSize ? value : DefaultPageSize; 
        }
    }
}
