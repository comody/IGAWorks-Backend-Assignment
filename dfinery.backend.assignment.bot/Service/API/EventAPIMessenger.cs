using System;
using System.Threading.Tasks;
using dfinery.backend.assignment.bot.Models;
using dfinery.backend.assignment.Models;
using dfinery.backend.assignment.bot.Utils;
using RestSharp;

namespace dfinery.backend.assignment.bot.API
{
    public class EventAPIMessenger : IEventAPIMessenger
    {
        private readonly RestAPIClientUtil client;
        private readonly string path;

        public EventAPIMessenger(RestAPIClientUtil client, string path)
        {
            this.client = client;
            this.path = path;
        }

        public async Task SendEventAsync(EventModel eventModel)
        {
            var request = new RestRequest(path);
            request.AddJsonBody(eventModel);
            await client.PostAsync<EventCollectRes>(request);
        }

    }
}
