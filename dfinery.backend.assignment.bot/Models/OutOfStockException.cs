using System;
namespace dfinery.backend.assignment.bot.Models
{
    public class OutOfStockException : Exception
    {
        public OutOfStockException(string message) : base(message)
        {
            
        }
    }
}
