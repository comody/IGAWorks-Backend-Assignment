using System;
using dfinery.backend.assignment.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dfinery.backend.assignment.Service.messenger;

namespace dfinery.backend.assignment.Controllers
{
    [Route("api/collect")]
    public class EventCollectController : Controller 
    {
        private readonly IEventSQSMessenger eventSQSMessenger;

        public EventCollectController(IEventSQSMessenger eventSQSMessenger)
        {
            this.eventSQSMessenger = eventSQSMessenger;
        }

        [HttpPost]
        public async Task<IActionResult> CollectEventAsync([FromBody] EventCollectReq req)
        {
            bool is_success = true;

            EventCollectModel eventCollectModel = req.ToEventModel();

            try
            {
                await eventSQSMessenger.SendMessage(eventCollectModel);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error pushing message to sqs : {e.Message}");
                is_success = false;
            }
            return Ok(new EventCollectRes { is_success = is_success });

        }
    }
}
