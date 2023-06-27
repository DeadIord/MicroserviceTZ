using System.Collections.Generic;
using System;

namespace UserService.ViewModel
{
    public class OrderHistoryItemVM
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalCost { get; set; }
        public int TotalQuantity { get; set; }
        public List<OrderItemVM> OrderItems { get; set; }

    }
}
