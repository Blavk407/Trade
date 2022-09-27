using System;
using System.Collections.Generic;

namespace TradeAPI
{
    public partial class Order
    {
        public Order()
        {
            ProductArticleNumbers = new HashSet<Product>();
        }

        public int OrderId { get; set; }
        public string OrderStatus { get; set; } = null!;
        public DateTime OrderDeliveryDate { get; set; }
        public int PointId { get; set; }
        public int? UserId { get; set; }
        public int ReceiptCode { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual PointsOfIssue Point { get; set; } = null!;
        public virtual User? User { get; set; }

        public virtual ICollection<Product> ProductArticleNumbers { get; set; }
    }
}
