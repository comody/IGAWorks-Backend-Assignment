using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dfinery.backend.assignment.Models;
using dfinery.backend.assignment.Models.search;
using dfinery.backend.assignment.store.IEventStoreService;
using Microsoft.AspNetCore.Mvc;

namespace dfinery.backend.assignment.Controllers
{
    [Route("api/search")]
    public class EventSearchController : Controller
    {
        private readonly IEventStoreService eventStoreService;

        public EventSearchController(IEventStoreService eventStoreService)
        {
            this.eventStoreService = eventStoreService;
        }

        [HttpPost] 
        public IActionResult SearchEventAsync([FromBody] EventSearchReq req)
        {
            bool is_success = true;

            List<EventSearchModel> results = new List<EventSearchModel>();

            try
            {
                results = eventStoreService.GetEventModels(req.user_id);   
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error searching events : {e.Message}"); 
                is_success = false;
            }
            return Ok(new EventSearchRes { is_success = is_success, results = results });
        }
        
    }
}
