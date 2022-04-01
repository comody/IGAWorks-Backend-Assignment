using System;
using System.Threading.Tasks;
using dfinery.backend.assignment.bot.API;
using dfinery.backend.assignment.bot.Utils;
using Microsoft.Extensions.Configuration;

namespace dfinery.backend.assignment.bot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration config = GetConfiguration();

            var url = config["API:Event:Endpoint"];
            var client = new RestAPIClientUtil(url);
            var eventAPI = new EventAPIMessenger(client, "api/collect");
            var userScenario = new UserScenario(eventAPI);

            try
            {
                await userScenario.Run();
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private static IConfiguration GetConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", false);

            return configurationBuilder.Build();
        }
    }
}
    