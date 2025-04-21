using Microsoft.AspNetCore.Mvc;
using ModelBilletterSystem;
using ModelLibrary;
namespace BillettetSystemAPI.Interfaces

{
    public interface ITicket
    {
        Task<List<Ticket>> GetAllTickets();
        Task<Ticket> GetTicketById(Guid Id_ticket);
        Task<Ticket> UpdateTicket(Guid Id_ticket, bool is_used, int eventId);
        Task<bool> RemoveTicket(Guid Id_ticket);
        Task<List<buyTicketResponse>> BuyTicket(int eventId, int ticketIntotal);
        Task<IActionResult> MarkAsUsed(Guid id_ticket);
        Task<IActionResult> TicketPDF(Guid id_ticket);
       
    }
}
