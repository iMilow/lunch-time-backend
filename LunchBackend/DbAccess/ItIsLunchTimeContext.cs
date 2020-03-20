using System;
using LunchBackend.DbAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LunchBackend.DbAccess
{
    public class ItIsLunchTimeContext: DbContext
    {
        public ItIsLunchTimeContext(DbContextOptions<ItIsLunchTimeContext> options): base(options)
        { }

        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
