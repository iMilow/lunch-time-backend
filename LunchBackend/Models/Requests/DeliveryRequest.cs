using System.Collections.Generic;

namespace LunchBackend.Models.Requests
{
    public class DeliveryRequest
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Shop { get; set; }

        public string PayPalUrl { get; set; }

        public double Price { get; set; }
        public IEnumerable<OrderRequest> Orders { get; set; }
    }
}
