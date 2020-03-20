namespace LunchBackend.Models.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OrderMessage { get; set; }
        public double Price { get; set; }
        public bool Support { get; set; }
        public bool Payed { get; set; }
        public int DeliverId { get; set; }
    }
}
