using System;
using System.Collections.Generic;

namespace TradeAPI
{
    public partial class PointsOfIssue
    {
        public PointsOfIssue()
        {
            Orders = new HashSet<Order>();
        }

        public int PointId { get; set; }
        public string Address { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
