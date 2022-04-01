using System;
using System.Collections.Generic;

namespace dfinery.backend.assignment.Models
{
    public class EventCollectReq
    {
        public string event_id { get; set; }
        public string user_id { get; set; }
        public string @event { get; set; }
        public Dictionary<string, object> parameters { get; set; }

        public EventModel ToEventModel()
        {
            return new EventModel
            {
                event_id = this.event_id,
                user_id = this.user_id,
                @event = this.@event,
                parameters = this.parameters,
                event_datetime = DateTime.UtcNow
            };
        }
    }
}
