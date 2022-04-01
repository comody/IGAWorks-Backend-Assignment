using System;
namespace dfinery.backend.assignment.bot.Models
{
    public class GoBankruptException : Exception
    {
        public GoBankruptException(string message) : base(message)
        {
        }
    }
}
