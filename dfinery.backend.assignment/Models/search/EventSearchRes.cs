using System;
using System.Collections.Generic;
using dfinery.backend.assignment.Models.search;

namespace dfinery.backend.assignment.Models
{
    public class EventSearchRes
    {
        public bool is_success { get; set; }
        public List<EventSearchModel> results { get; set; }
    }
}
