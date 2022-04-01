using System;
using System.Threading.Tasks;
using Amazon.SQS;
using dfinery.backend.assignment.Models;
using Newtonsoft.Json;

namespace dfinery.backend.assignment.Service.messenger
{ 
    public struct EventSQSMessenger : IEventSQSMessenger
    {
        private AmazonSQSClient amazonSQSClient;
        private string queueUrl;

        public EventSQSMessenger(string queueUrl)
        {
            amazonSQSClient = new AmazonSQSClient(Amazon.RegionEndpoint.APNortheast2);
            this.queueUrl = queueUrl;
        }

        public async Task SendMessage(EventModel req)
        {
            var messageBody = JsonConvert.SerializeObject(req);
            await amazonSQSClient.SendMessageAsync(queueUrl, messageBody);
        }

    }
}
