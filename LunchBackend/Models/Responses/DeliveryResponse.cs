using System.Collections.Generic;

namespace LunchBackend.Models.Responses
{
    public class DeliveryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Shop { get; set; }
        public string PayPalUrl { get; set; }
        public double Price { get; set; }
        public IEnumerable<OrderResponse> Orders { get; set; }
    }
}
