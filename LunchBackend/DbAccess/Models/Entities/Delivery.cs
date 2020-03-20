using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LunchBackend.DbAccess.Models.Entities
{
    public class Delivery
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Shop { get; set; }

        public string PayPalUrl { get; set; }
        
        // Relationships
        public ICollection<Order> Orders { get; set; }
    }
}