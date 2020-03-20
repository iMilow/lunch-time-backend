using AutoMapper;
using LunchBackend.DbAccess.Interfaces;
using LunchBackend.DbAccess.Models.Entities;
using LunchBackend.Models.Requests;
using LunchBackend.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LunchBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveriesController: ControllerBase
    {
        public DeliveriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllDeliveries()
        {
            var deliveriesToReturn =
                await UnitOfWork.Deliveries.GetAllAsQueryable().Include(d => d.Orders).ToListAsync();

            if (deliveriesToReturn == null)
            {
                return NotFound();
            }

            if (deliveriesToReturn.Count > 0)
            {
                var result = Mapper.Map<IEnumerable<DeliveryResponse>>(deliveriesToReturn);
                return Ok(result);    
            }

            return Ok();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var deliveryToReturn = await UnitOfWork.Deliveries.GetSingleFullDataAsync(d => d.Id == id,
                del => del.Include(de => de.Orders));

            if (deliveryToReturn == null)
            {
                return NotFound();
            }

            var result = Mapper.Map<DeliveryResponse>(deliveryToReturn);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDelivery(DeliveryRequest request)
        {
            var deliveryToAdd = Mapper.Map<Delivery>(request);

            if (deliveryToAdd == null)
            {
                return BadRequest();
            }

            this.UnitOfWork.Deliveries.Add(deliveryToAdd);

            await UnitOfWork.CompleteAsync();

            return Ok(deliveryToAdd);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateDelivery(DeliveryRequest request)
        {
            var deliveryToUpdate = await UnitOfWork.Deliveries.GetSingleFullDataAsync(d => d.Id == request.Id, de => 
                de.Include(del => del.Orders));

            if (deliveryToUpdate == null)
            {
                return BadRequest();
            }

            // Remove orders
            var ordersToRemove = this.UnitOfWork.Orders.Find(o => o.DeliverId == request.Id);
            UnitOfWork.Orders.RemoveRange(ordersToRemove);

            await UnitOfWork.CompleteAsync();

            var ordersToUpdate = Mapper.Map<IEnumerable<Order>>(request.Orders);
            
            UnitOfWork.Orders.AddRange(ordersToUpdate);

            var result = await UnitOfWork.CompleteAsync();

            if(result == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            var deliveryToDelete = await UnitOfWork.Deliveries.GetSingleFullDataAsync(d => d.Id == id,
                del => del.Include(de => de.Orders));

            if (deliveryToDelete == null)
            {
                return NotFound();
            }

            UnitOfWork.Deliveries.Remove(deliveryToDelete);

            return Ok();
        }
    }
}