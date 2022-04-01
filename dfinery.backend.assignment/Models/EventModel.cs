using System;
namespace dfinery.backend.assignment.Models
{
    public class EventModel : EventCollectReq
    {
        public DateTime event_datetime { get; set; }

        public EventModel() { }
    }
}
