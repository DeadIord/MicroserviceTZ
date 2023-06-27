using System.Collections.Generic;
using System;

namespace ProducService.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int OrdersId { get; set; }
        
        public virtual Product Product { get; set; }
   
    }
   
   
}
