using System;
using System.Collections.Generic;
using dfinery.backend.assignment.bot.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace dfinery.backend.assignment.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EVENT_TYPE
    {
        LOG_IN,
        READY_PURCHASE,
        COMPLETE_PURCHASE,
        OUT_OF_STOCK,
        GO_BANKRUPT,
        LOG_OUT,
    }

    public class EventModel
    {
        public string event_id { get; set; }
        public string user_id { get; set; }
        public string @event { get; set; }
        public Dictionary<string, object> parameters { get; set; }

        public EventModel(User user, EVENT_TYPE @event, Dictionary<string, object> parameters)
        {
            event_id = Guid.NewGuid().ToString();
            user_id = user.user_id;
            this.@event = @event.ToString();
            this.parameters = parameters;
        }
    }
}