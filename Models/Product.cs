using System;
using System.Collections.Generic;

namespace EMARKET_MVC.Models
{
    public partial class Product
    {
        public Product()
        {
            Baskets = new HashSet<Basket>();
            Orders = new HashSet<Order>();
        }

        public string ProductId { get; set; } = null!;
        public string? ProductName { get; set; }
        public string? ProductProperty { get; set; }
        public string? Price { get; set; }

        public virtual ICollection<Basket> Baskets { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
