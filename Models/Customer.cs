using System;
using System.Collections.Generic;

namespace EMARKET_MVC.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Baskets = new HashSet<Basket>();
            Orders = new HashSet<Order>();
        }

        public string CustomerId { get; set; } = null!;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Basket> Baskets { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
