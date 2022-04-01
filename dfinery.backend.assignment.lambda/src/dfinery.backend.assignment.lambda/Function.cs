using System;
using System.Collections.Generic;
using System.Text;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using dfinery.backend.assignment.lambda.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using static Amazon.Lambda.SQSEvents.SQSBatchResponse;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace dfinery.backend.assignment.lambda
{
    public class Function
    {
        private string connectionString;
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="sqsEvent"></param>
        /// <param name="context"></param>
        /// <returns></returns>

        public SQSBatchResponse FunctionHandler(SQSEvent sqsEvent, ILambdaContext context)
        {
            // Set Environment Variable
            connectionString = Environment.GetEnvironmentVariable("connectionString");

            var batchItemFailures = new List<BatchItemFailure>();

            // Consume SQS Event
            foreach (var record in sqsEvent.Records)
            {
                var messageBody = record.Body;

                // Store Log
                context.Logger.LogLine($"#1. sqs message: {messageBody}");

                var eventModel = JsonConvert.DeserializeObject<EventModel>(messageBody);

                var is_success = true;

                try
                {
                    // Update DB
                    is_success = UpdateEventModels(eventModel);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to update event model. e.message = {e.Message}");

                    is_success = false;
                }

                // Handle Exception case
                if (!is_success)
                {
                    batchItemFailures.Add(new BatchItemFailure { ItemIdentifier = record.MessageId });
                }
            }

            return new SQSBatchResponse { BatchItemFailures = batchItemFailures };
        }

        private bool UpdateEventModels(EventModel eventModel)
        {
            var parametersStr = JsonConvert.SerializeObject(eventModel.parameters);
            var parametersByte = Encoding.UTF8.GetBytes(parametersStr);

            string tableName = "event_tb";
            string sql = $"INSERT INTO {tableName} VALUES(@event_id, @user_id, @event, @parameters, @event_datetime)";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@event_id", eventModel.event_id);
                cmd.Parameters.AddWithValue("@user_id", eventModel.user_id);
                cmd.Parameters.AddWithValue("@event", eventModel.@event);
                cmd.Parameters.AddWithValue("@parameters", parametersByte);
                cmd.Parameters.AddWithValue("@event_datetime", eventModel.event_datetime.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to update DB. message: {e.Message}");
                    return false;
                }
            }

            return true;
        }
    }
}