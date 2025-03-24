using ModelBilletterSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillettetSystemAPI.Interfaces

{
    public interface ITicket
    {
        Task<List<Ticket>> GetAllTickets();
        Task<Ticket> GetTicketById(int id);
        Task<Ticket> CreateTicket(int ticket_amount, int ticket_price, bool ability, bool is_used, int eventId);
        Task<Ticket> UpdateTicket(int id, int ticket_amount, int ticket_price, bool ability, bool is_used, int eventId);
        Task<bool> RemoveTicket(int id);

    }
}
