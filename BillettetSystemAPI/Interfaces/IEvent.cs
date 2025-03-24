using ModelBilletterSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillettetSystemAPI.Interfaces
{
    public interface IEvent
    {
        Task<List<Event>> GetAllEvents();
        Task<Event> GetEventById(int id);
        Task<Event> CreateEvent(string event_title, string event_description, DateTime create_at, List<int> categoryIds);
        Task<Event> UpdateEvent(int id, string event_title, string event_description, DateTime create_at, List<int> categoryId);
        Task<bool> RemoveEvent(int id);
    }
}
