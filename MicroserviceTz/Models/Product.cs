
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace MicroserviceTz.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
      
        
    }

}
