using ModelBilletterSystem;
namespace BillettetSystemAPI.Interfaces

{
    public interface ITicket
    {
        Task<List<Ticket>> GetAllTickets();
        Task<Ticket> GetTicketById(Guid Id_ticket);
        Task<Ticket> CreateTicket(decimal ticket_price, bool is_used, int eventId);
        Task<Ticket> UpdateTicket(Guid Id_ticket, decimal ticket_price, bool is_used, int eventId);
        Task<bool> RemoveTicket(Guid Id_ticket);
        Task BuyTicket(decimal ticket_Price,bool is_used);
    }
}
