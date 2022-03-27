using System;
namespace dfinery.backend.assignment.Models
{
    public class EventCollectModel : EventCollectReq
    {
        public DateTime event_datetime { get; set; }

        public EventCollectModel() { }
    }
}
