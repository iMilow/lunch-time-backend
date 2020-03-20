namespace LunchBackend.DbAccess.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OrderMessage { get; set; }
        public bool Support { get; set; }
        public bool Payed { get; set; }

        public double Price { get; set; }
        
        // Relationships
        public int DeliverId { get; set; }
        public Delivery Deliver { get; set; }
    }
}