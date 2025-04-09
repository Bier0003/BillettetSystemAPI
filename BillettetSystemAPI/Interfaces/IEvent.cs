using ModelBilletterSystem;
namespace BillettetSystemAPI.Interfaces
{
    public interface IEvent
    {
        Task<List<Event>> GetAllEvents();
        Task<Event> GetEventById(int Id_Event);
        Task<Event> CreateEvent(string Event_Title, string Event_Description, DateTime Create_at,int Ticket_amount, string? Image, int CategoryId);
        Task<Event> UpdateEvent(int Id_Event, string Event_Title, string Event_Description, DateTime Create_at, int Ticket_amount, string? Image, int CategoryId);
        Task<bool> RemoveEvent(int Id_Event);
        
    }
}
