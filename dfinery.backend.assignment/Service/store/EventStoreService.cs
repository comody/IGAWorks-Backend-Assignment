using System;
using System.Collections.Generic;
using System.Text;
using dfinery.backend.assignment.Models.search;
using dfinery.backend.assignment.store.IEventStoreService;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace dfinery.backend.assignment.Service.store
{
    public class EventStoreService : IEventStoreService
    {
        public string connectionString;

        public EventStoreService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public List<EventSearchModel> GetEventModels(string user_id)
        {
            var eventModels = new List<EventSearchModel>();
            string tableName = "event_tb";
            string sql = $"select * from {tableName} where user_id = '{user_id}' order by event_datetime desc ";

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var parametersByte = (byte[])(reader["parameters"]);
                        var parametersStr = Encoding.UTF8.GetString(parametersByte);
                        var parameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(parametersStr);
                        eventModels.Add(new EventSearchModel
                        {
                            event_id = reader["event_id"].ToString(),
                            @event = reader["event"].ToString(),
                            event_datetime = Convert.ToDateTime(reader["event_datetime"]),
                            parameters = parameters 

                        });
                    }
                }
            }
            return eventModels;
        }
        
    }
}
