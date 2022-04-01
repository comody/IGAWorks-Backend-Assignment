using System;
using System.Collections.Generic;

namespace dfinery.backend.assignment.lambda.Models
{
    public class EventModel
    {
        public string event_id { get; set; }
        public string user_id { get; set; }
        public string @event { get; set; }
        public Dictionary<string, object> parameters { get; set; }
        public DateTime event_datetime { get; set; }
    }
}
