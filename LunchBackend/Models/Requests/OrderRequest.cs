namespace LunchBackend.Models.Requests
{
    public class OrderRequest
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string OrderMessage { get; set; }

        public bool Support { get; set; }

        public string Price { get; set; }

        public bool Payed { get; set; }

        public int DeliverId { get; set; }
    }
}
