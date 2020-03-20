using System;
using System.Threading.Tasks;
using LunchBackend.DbAccess.Models.Entities;

namespace LunchBackend.DbAccess.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Delivery> Deliveries { get; }
        IRepository<Order> Orders { get; }

        // Complete task
        Task<bool> CompleteAsync();

        int Complete();
    }
}