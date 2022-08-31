using System;
using System.Collections.Generic;

namespace EMARKET_MVC.Models
{
    public partial class Order
    {
        public string OrderId { get; set; } = null!;
        public string? ProductId { get; set; }
        public string? CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Product? Product { get; set; }
    }
}
