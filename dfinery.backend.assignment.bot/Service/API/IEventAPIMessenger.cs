using System.Threading.Tasks;
using dfinery.backend.assignment.Models;

namespace dfinery.backend.assignment.bot.API
{
    public interface IEventAPIMessenger
    {
        Task SendEventAsync(EventModel eventModel);
    }
}