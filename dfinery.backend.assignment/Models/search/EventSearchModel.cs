using System;
using System.Collections.Generic;

namespace dfinery.backend.assignment.Models.search
{
    public class EventSearchModel
    {
        public string event_id { get; set; }
        public string @event { get; set; }
        public Dictionary<string, object> parameters { get; set; }
        public string event_datetime { get; set; }
    }
}
