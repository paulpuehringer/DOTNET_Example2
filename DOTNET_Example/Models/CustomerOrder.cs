using System.Collections.Generic;

namespace DOTNET_Example.Models
{
    public class CustomerOrder
    {
        public int OrderId { get; set; }
        public string OrderPlaced { get; set; }
        public string OrderFulfilled { get; set; }
        public string CustomerName { get; set; }
        public IEnumerable<Pizza> Pizzas { get; set; }
    }
}