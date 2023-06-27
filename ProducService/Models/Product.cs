
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace ProducService.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
     
        public virtual ICollection<OrderItem> OrderItems { get; set; }
      
        
    }

}
