using System.Threading.Tasks;
using LunchBackend.DbAccess.Interfaces;
using LunchBackend.DbAccess.Models.Entities;

namespace LunchBackend.DbAccess
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ItIsLunchTimeContext _context;

        public UnitOfWork(ItIsLunchTimeContext context)
        {
            this._context = context;
            
            this.Deliveries = new Repository<Delivery>(this._context);
            this.Orders = new Repository<Order>(this._context);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        public IRepository<Delivery> Deliveries { get; }
        public IRepository<Order> Orders { get; }
        
        public async Task<bool> CompleteAsync()
        {
            return await this._context.SaveChangesAsync() > 0;
        }

        public int Complete()
        {
            return this._context.SaveChanges();
        }
    }
}