using System.Collections.Generic;
using System;

namespace MicroserviceTz.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int OrdersId { get; set; }
        
        public virtual Product Product { get; set; }
        public virtual Order Orders { get; set; }
    }
    public class OrderDetailsVM
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalCost { get; set; }
        public List<OrderItemVM> OrderItems { get; set; }
    }

    public class OrderHistoryItemVM
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalCost { get; set; }
        public int TotalQuantity { get;  set; }
        public List<OrderItemVM> OrderItems { get; set; }
       
    }
    public class OrderItemVM
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
