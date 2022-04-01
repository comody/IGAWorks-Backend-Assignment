using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace dfinery.backend.assignment.bot.Utils
{
    public class RestAPIClientUtil
    {
        private readonly RestClient client;

        public RestAPIClientUtil(string url)
        {
            client = new RestClient(url);
        }

        public async Task<T> PostAsync<T>(RestRequest request)
        {
            var response = await client.ExecutePostAsync(request);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
