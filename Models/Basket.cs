using System;
using System.Collections.Generic;

namespace EMARKET_MVC.Models
{
    public partial class Basket
    {
        public string BasketId { get; set; } = null!;
        public string? CustomerId { get; set; }
        public string? ProductId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Product? Product { get; set; }
    }
}
