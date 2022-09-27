using System;
using System.Collections.Generic;

namespace TradeAPI
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }
        
        public string ProductArticleNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public string ProductCategory { get; set; } = null!;
        public string ProductManufacturer { get; set; } = null!;
        public double ProductCost { get; set; }
        public int? ProductDiscountAmount { get; set; }
        public int ProductQuantityInStock { get; set; }
        public string ProductProvider { get; set; } = null!;
        public int? ProductMaxDiscountAmount { get; set; }
        public string ProductUnitOfMeasurement { get; set; } = null!;
        public string? ProductPhoto { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
