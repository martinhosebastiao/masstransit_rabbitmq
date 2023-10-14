using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Messaging.RabbitMq.Domain.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Messaging.RabbitMq.Publisher.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IBus bus;
        public OrderController(IBus bus) => this.bus = bus;

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> CreateTicketAsync([FromBody]TicketModel ticket)
        {
            try
            {
                if (ticket == null)
                    return BadRequest();

                ticket.Booked = DateTime.Now;

                Uri uri = new("rabbitmq://localhost/orderTicketQueue");
                ISendEndpoint? endPoint = await bus.GetSendEndpoint(uri);

                await endPoint.Send(ticket);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

