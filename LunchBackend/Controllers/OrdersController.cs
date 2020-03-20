using AutoMapper;
using LunchBackend.DbAccess.Interfaces;
using LunchBackend.DbAccess.Models.Entities;
using LunchBackend.Models.Requests;
using LunchBackend.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LunchBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController: ControllerBase
    {
        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var orderToReturn = await UnitOfWork.Orders.GetSingleFullDataAsync(d => d.Id == id,
                ord => ord.Include(or => or.Deliver));

            if (orderToReturn == null)
            {
                return NotFound();
            }

            var result = Mapper.Map<OrderResponse>(orderToReturn);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequest request)
        {
            var orderToAdd = Mapper.Map<Order>(request);

            if (orderToAdd == null)
            {
                return BadRequest();
            }

            this.UnitOfWork.Orders.Add(orderToAdd);

            await UnitOfWork.CompleteAsync();

            return Ok(orderToAdd);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateOrder(OrderRequest request)
        {
            var orderToUpdate = await UnitOfWork.Orders.GetAsync(request.Id ?? -1);

            if (orderToUpdate != null)
            {
                orderToUpdate.Name = request.Name;
                orderToUpdate.OrderMessage = request.OrderMessage;
                orderToUpdate.Support = request.Support;
                orderToUpdate.Payed = request.Payed;
            }

            UnitOfWork.Orders.Update(orderToUpdate);

            var result = await UnitOfWork.CompleteAsync();

            if (result == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var orderToDelete = await UnitOfWork.Orders.GetAsync(id);

            if (orderToDelete == null)
            {
                return NotFound();
            }

            UnitOfWork.Orders.Remove(orderToDelete);

            return Ok();
        }
    }
}